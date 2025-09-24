namespace WsDemo; 
    
static class ConnectionInfo
{
    public static int Count = 0;
    public static List<string> ConnectionsIds = new List<string>();

    public static string GetCurrentConnections()
    {
        string info = "";
        foreach (var i in ConnectionsIds)
        {
            info += i + "\n";
        }

        return info;
    }

}