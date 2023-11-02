namespace DuckDnsUpdater;

public class HttpClientCacher : IDisposable
{
    private HttpClient? _httpClient;

    private readonly TimeSpan _validInterval;

    private DateTime ValidBefore { get; set; } = DateTime.MinValue;

    public HttpClient HttpClient
    {
        get
        {
            if (DateTime.Now > ValidBefore || _httpClient is null)
            {
                _httpClient?.Dispose();
                _httpClient = new HttpClient();
                ValidBefore = DateTime.Now + _validInterval;
            }

            return _httpClient;
        }
    }

    public HttpClientCacher(TimeSpan validInterval)
    {
        _validInterval = validInterval;
    }

    public HttpClientCacher() : this(TimeSpan.FromHours(1))
    {
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}