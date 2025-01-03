using System.Text.Json;
using Capstone.Collector.Models;

namespace Capstone.Collector.ApiClient;

public class CongressApiClient
{
   private readonly System.Net.Http.HttpClient _httpClient;
   private const string GetMembersEndpoint = "/v3/member/congress";
   private const string GetCongressEndpoint = "/v3/congress/current";
   private const string GetBillsEndpoint = "/v3/bill";
   private readonly string _apiKey;
   private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
   private readonly ILogger _logger;
   
   public CongressApiClient(string baseUrl, string apiKey,ILogger logger)
   {
      _httpClient = new System.Net.Http.HttpClient();
      _httpClient.BaseAddress = new Uri(baseUrl);
      this._apiKey = apiKey;
      _logger = logger;
   }

   public async Task<CongressMemberResponseDto?> GetMembersAsync(int? skip, int? take, int congressNumber)
   {
      var path = $"{GetMembersEndpoint}/{congressNumber}";
      if(!string.IsNullOrWhiteSpace(_apiKey))path += $"?api_key={_apiKey}";
      
      
      if (skip is not null)
      {
         var separator = path.Contains("?") ? "&" : "?";
         path += $"{separator}offset={skip}";   
      }

      if (take is not null)
      {
         var separator = path.Contains("?") ? "&" : "?";
         path += $"{separator}limit={take}";
      }
      
      _logger.LogInformation($"GET {path}");
      var uri = new Uri(_httpClient.BaseAddress.ToString()+path);
      
      var response = await _httpClient.GetAsync(uri);
      var data = await HandleJsonResponse<CongressMemberResponseDto>(response);
      return data;
   }

   public async Task<CongressResponseDto?> GetCongressAsync()
   {
      var path = $"{GetCongressEndpoint}";
      if(!string.IsNullOrWhiteSpace(_apiKey))path += $"?api_key={_apiKey}";
      var uri = new Uri(_httpClient.BaseAddress.ToString()+path);
     
      var response = await _httpClient.GetAsync(uri);
      var data = await HandleJsonResponse<CongressResponseDto>(response);
      _logger.LogInformation($"{data.ToString()}");
      return data;
   }

   public async Task<BillResponseDto> GetBillAsync(int congress)
   {
      var path = $"{GetBillsEndpoint}/{congress}";
      if(!string.IsNullOrWhiteSpace(_apiKey))path += $"?api_key={_apiKey}";
      var uri = new Uri(_httpClient.BaseAddress.ToString()+path);
      
      var response = await _httpClient.GetAsync(uri);
      var data = await HandleJsonResponse<BillResponseDto>(response);
      _logger.LogInformation($"{data.ToString()}");
      return data;
   }

   public async Task<BillResponseDto?> GetMemberBillsAsync(string bioguideId)
   {
      throw new NotImplementedException();
   }

   private async Task<T?> HandleJsonResponse<T>(HttpResponseMessage response)
   {
      if (!response.IsSuccessStatusCode) return default(T);
      var json = await response.Content.ReadAsStringAsync();
      var result = JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
      return result;
   }
}