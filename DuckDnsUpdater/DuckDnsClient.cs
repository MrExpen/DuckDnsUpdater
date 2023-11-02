namespace DuckDnsUpdater;

public class DuckDnsClient : IDisposable
{
    private const string BaseAddress = "https://www.duckdns.org";
    private const string OkString = "OK";
    
    private readonly HttpClientCacher _httpClientCacher = new();

    private readonly string _url;

    /// <summary>
    /// конструктор класса
    /// </summary>
    /// <param name="token">токен авторизации</param>
    /// <param name="domains">домены на обновление</param>
    public DuckDnsClient(string token, string domains)
    {
        _url = $"{BaseAddress}/update?domains={domains}&token={token}";
    }

    /// <summary>
    /// Обновляет DNS записи доменов текущим внешним ip адресом
    /// </summary>
    /// <returns></returns>
    public async Task<bool> UpdateDnsRecordsAsync()
    {
        try
        {
            var result = await _httpClientCacher.HttpClient.GetStringAsync(_url);

            return string.Equals(result, OkString, StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
    
    public void Dispose()
    {
        _httpClientCacher.Dispose();
    }
}