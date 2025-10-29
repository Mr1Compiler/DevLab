using System.Net.NetworkInformation;

namespace NetworkUtility.Ping;

public class NetworkService
{
    public string SendPing()
    {
        return "Success : Ping Sent";
    }

    public int PingTimeout(int a, int b)
    {
        return a + b;
    }

    public DateTime LastPingData()
    {
        return DateTime.Now;
    }

    public PingOptions GetPingOptions()
    {
        return new PingOptions
        {
            DontFragment = true,
            Ttl = 1
        };
    }

    public IEnumerable<PingOptions> MostRecentPings()
    {
        IEnumerable<PingOptions> pingOptions = new List<PingOptions>()
        {
            new PingOptions
            {
                DontFragment = true,
                Ttl = 1
            },
            new PingOptions
            {
                DontFragment = true,
                Ttl = 1
            },
            new PingOptions
            {
                DontFragment = true,
                Ttl = 1
            },
        };

        return pingOptions;
    }
}