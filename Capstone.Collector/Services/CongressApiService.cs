using Capstone.Collector.ApiClient;
using Capstone.Collector.Models;
using Capstone.Collector.Utils;
using Capstone.Common.Entities;
using Capstone.Common.Repository;
using Congress = Capstone.Common.Entities.Congress;
using Constants = Capstone.Collector.Utils.Constants;

namespace Capstone.Collector.Services;

public class CongressApiService
{
    private readonly CongressApiClient _congressApiClient;
    private readonly CongressMemberRepository _congressMemberRepository;
    private readonly CongressRepository _congressRepository;
    private readonly ILogger<CongressApiService> _logger;

    public CongressApiService(IConfiguration configuration, CongressMemberRepository congressMemberRepository,
        CongressRepository congressRepository, ILogger<CongressApiService> logger)
    {
        var connectionConfig = configuration.GetSection("ConnectionStrings").Get<Config>();
        _congressApiClient =
            new CongressApiClient(connectionConfig!.CongressApiUrl, connectionConfig.CongressApiKey, logger);
        _congressMemberRepository = congressMemberRepository;
        _logger = logger;
        _congressRepository = congressRepository;
    }

    // public async Task<List<CongressMember>> GetAllCongressMembers(int congressNumber)
    // {
    //     var memberDtos = new List<CongressMemberDto>();
    //     var membersResponseDto = await _congressApiClient.GetMembersAsync(null, batchSize, congressNumber);
    // }

    public async Task<List<CongressMemberDto>> GetMembers(int? skip, int? batchSize, int congressNumber)
    {
        var memberDtos = new List<CongressMemberDto>();
        var membersResponseDto = await _congressApiClient.GetMembersAsync(skip, batchSize, congressNumber);

        if (membersResponseDto is null || !membersResponseDto.Members.Any()) return memberDtos.ToList();

        memberDtos.AddRange(membersResponseDto.Members);
        while (membersResponseDto != null && membersResponseDto.Pagination.Next.Length > 0)
        {
            var batchSkip = memberDtos.Count;
            _logger.LogInformation("Fetching members, skip={},take={} ", batchSkip, batchSize);
            membersResponseDto = await _congressApiClient.GetMembersAsync(batchSkip, batchSize, congressNumber);
            if (membersResponseDto?.Members != null) memberDtos.AddRange(membersResponseDto.Members);
        }

        _logger.LogInformation("Fetching members finished. Total members : {}", memberDtos.Count);
        return memberDtos;
    }

    public async Task<CongressResponseDto?> GetCongress()
    {
        return await _congressApiClient.GetCongressAsync();
    }

    public async Task<SynchronizationSummaryDto> SynchronizeCongress()
    {
        var congressResponse = await _congressApiClient.GetCongressAsync();
        if (congressResponse == null) return new SynchronizationSummaryDto();

        var congressEntity = Converter.ConvertCongressDtoToEntity(congressResponse.Congress);
        _logger.LogInformation("Received congress information for Congress {}", congressResponse.Congress.Number);
        await _congressRepository.CreateAsync(congressEntity);

       return new SynchronizationSummaryDto { Congress = congressResponse.Congress.Number };
    }

    public async Task<SynchronizationSummaryDto> SynchronizeCongressMembers(int congressNumber)
    {
        var members = await GetMembers(0, 200, congressNumber);

        _logger.LogInformation("Received members for congress {}. Members found: {}", congressNumber,
            members.Count);
        var memberEntities = members.Where(member => member.BioguideId is not null).Select(Converter.ConvertMemberEntityFromDto)
            .ToList();
        var existingMemberEntities = _congressMemberRepository.GetAll();
        var existingBioGuideIds = existingMemberEntities.Select(x => x.BioGuideId);
        try
        {
            var newEntries = memberEntities.Where(x => !existingBioGuideIds.Contains(x.BioGuideId)).ToList();
            if (newEntries.Any())
            {
                _logger.LogInformation("Adding {} entries", newEntries.Count);
                await _congressMemberRepository.CreateAsync(newEntries);
            }

            var existingEntitiesToUpdate =
                memberEntities.Where(x => existingBioGuideIds.Contains(x.BioGuideId)).ToList();
            var updatedEntities = UpdateMembers(existingEntitiesToUpdate, members);

            _logger.LogInformation("Updating {} entries", updatedEntities.Count);
            await _congressMemberRepository.Update(updatedEntities);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to synchronize congress");
        }

        return new SynchronizationSummaryDto { Congress = congressNumber, CongressMemberCount = members.Count };
    }

    private List<CongressMember> UpdateMembers(List<CongressMember> existingMemberEntities,
        List<CongressMemberDto> memberDtos)
    {
        var existingIds = existingMemberEntities.Select(x => x.BioGuideId).ToList();
        var updates = new List<CongressMember>();
        foreach (var bioGuideId in existingIds)
        {
            var existingMemberEntity = existingMemberEntities.SingleOrDefault(x => x.BioGuideId.Equals(bioGuideId));
            var memberDto = memberDtos.SingleOrDefault(x => x.BioguideId.Equals(bioGuideId));

            //todo : check if existibg entity modified date > today
            var wasModifiedInDb = existingMemberEntity.ModifiedDate != null;
            var updateHasOccured = false;

            if (existingMemberEntity != null && memberDto != null)
            {
                updateHasOccured = DateTime.Compare(memberDto.UpdatedDate, existingMemberEntity.CreatedDate) > 0 ||
                                   (wasModifiedInDb && DateTime.Compare(memberDto.UpdatedDate,
                                       (DateTime)existingMemberEntity.ModifiedDate!) > 0);
            }

            if (existingMemberEntity != null && memberDto != null && updateHasOccured)
            {
                existingMemberEntity.Name = memberDto.Name;
                existingMemberEntity.Url = memberDto.Url;
                existingMemberEntity.Attribution = memberDto.Depiction.Attribution;
                existingMemberEntity.ImageUrl = memberDto.Depiction.ImageUrl;
                existingMemberEntity.ModifiedDate = DateTime.Now;
                existingMemberEntity.ModifiedBy = Constants.API_NAME;
                //existingMemberEntity.Email = memberDto.Email;
                updates.Add(existingMemberEntity);
            }
        }

        return updates;
    }

}