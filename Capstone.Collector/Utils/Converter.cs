using Capstone.Collector.Models;
using Capstone.Common.Entities;

namespace Capstone.Collector.Utils;

public class Converter
{
    public static Congress ConvertCongressDtoToEntity(CongressDto dto)
    {
        var timeStamp = DateTime.UtcNow;
        return new Congress
        {
            CreatedDate = timeStamp,
            CreatedBy = Constants.API_NAME,
            StartYear = dto.StartYear,
            EndYear = dto.EndYear,
            Name = dto.Name,
            Number = dto.Number,
            ModifiedBy = Constants.API_NAME,
            ModifiedDate = timeStamp,
        };
    }

    public static  CongressMember ConvertMemberEntityFromDto(CongressMemberDto dto)
    {
        var timeStamp = DateTime.UtcNow;

        return new CongressMember
        {
            BioGuideId = dto.BioguideId,
            Name = dto?.Name,
            ImageUrl = dto.Depiction?.ImageUrl,
            Attribution = dto.Depiction?.Attribution,
            State = dto?.State,
            Url = dto?.Url,
            CreatedBy = Constants.API_NAME,
            CreatedDate = timeStamp,
            ModifiedBy = Constants.API_NAME,
            ModifiedDate = timeStamp
        };
    }
}