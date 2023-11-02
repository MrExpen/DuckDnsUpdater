namespace DuckDnsUpdater;

public class DuckDnsClient : IDisposable
{
    private const string BaseAddress = "https://www.duckdns.org";
    private const string OkString = "OK";
    
    private readonly HttpClientCacher _httpClientCacher = new();

    private readonly string _token;

    /// <summary>
    /// конструктор класса
    /// </summary>
    /// <param name="token">токен авторизации</param>
    public DuckDnsClient(string token)
    {
        _token = token;
    }

    /// <summary>
    /// Обновляет DNS записи доменов текущим внешним ip адресом
    /// </summary>
    /// <param name="domains">имена доменов через запятую</param>
    /// <returns></returns>
    public Task<bool> UpdateDnsRecordsAsync(string domains)
    {
        return UpdateDnsRecordsAsync(_httpClientCacher.HttpClient, domains, _token);
    }

    /// <summary>
    /// Обновляет DNS записи доменов текущим внешним ip адресом
    /// </summary>
    /// <param name="client">Http клиент</param>
    /// <param name="domains">имена доменов через запятую</param>
    /// <param name="token">токен пользователя</param>
    /// <returns><b>true</b> если запрос завершился успешно, иначе <b>false</b></returns>
    private static async Task<bool> UpdateDnsRecordsAsync(HttpClient client, string domains, string token)
    {
        try
        {
            var result = await client.GetStringAsync($"{BaseAddress}/update?domains={domains}&token={token}");

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