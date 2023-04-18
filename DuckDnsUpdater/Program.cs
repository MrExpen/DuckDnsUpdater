string domain = Environment.GetEnvironmentVariable("DOMAIN");
if (string.IsNullOrWhiteSpace(domain))
{
    throw new ArgumentException(nameof(domain));
}

string token = Environment.GetEnvironmentVariable("TOKEN");
if (string.IsNullOrWhiteSpace(token))
{
    throw new ArgumentException(nameof(token));
}

while (true)
{
    var response =
        await new HttpClient()
            .GetStringAsync($"https://www.duckdns.org/update?domains={domain}&token={token}");

    if (response == "OK")
    {
        Console.WriteLine("duckdns update ok");
    }
    else
    {
        Console.WriteLine("duckdns update failed");
    }

    await Task.Delay(TimeSpan.FromMinutes(5));
}