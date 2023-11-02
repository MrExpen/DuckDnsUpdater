using DuckDnsUpdater;

#region SetUp

var token = Environment.GetEnvironmentVariable("TOKEN");
var domains = Environment.GetEnvironmentVariable("DOMAINS");

if (string.IsNullOrWhiteSpace(token))
    throw new ArgumentException(nameof(token));

if (string.IsNullOrWhiteSpace(domains))
    throw new ArgumentException(nameof(domains));

var client = new DuckDnsClient(token);

#endregion

while (true)
{
    var success = await client.UpdateDnsRecordsAsync(domains);

    if (success)
        Console.WriteLine("duckdns update ok");

    else
        Console.WriteLine("duckdns update failed");


    await Task.Delay(TimeSpan.FromMinutes(5));
}