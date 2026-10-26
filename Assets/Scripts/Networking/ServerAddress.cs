/// <summary>
/// Turns what a player typed in the main menu into the WebSocket URL of a server.
///
/// A full URL (ws:// or wss://) is used as it is. A bare "host:port" is the old form and
/// becomes ws://host:port/. A host without a port means the default server port on a
/// desktop build, and wss:// on the standard port in a browser: a page served over HTTPS
/// may only open secure WebSockets.
/// </summary>
public static class ServerAddress
{
    public const string PreferenceKey = "server";

    public const int DefaultPort = 25565;

    // The public server on the cluster. What a browser build connects to unless the player types another.
    public const string HostedServer = "wss://radioactive-server.spraxs.nl/";

    private const string LocalHost = "localhost";
    private const string SecureScheme = "wss://";
    private const string PlainScheme = "ws://";
    private const char PortSeparator = ':';
    private const char PathSeparator = '/';

#if UNITY_WEBGL && !UNITY_EDITOR
    public static readonly string DefaultInput = HostedServer;
#else
    public static readonly string DefaultInput = LocalHost + PortSeparator + DefaultPort;
#endif

    public static string ToUrl(string input)
    {
        string address = string.IsNullOrWhiteSpace(input) ? DefaultInput : input.Trim();

        if (address.StartsWith(SecureScheme) || address.StartsWith(PlainScheme))
        {
            return address.EndsWith(PathSeparator.ToString()) ? address : address + PathSeparator;
        }

        if (address.IndexOf(PortSeparator) >= 0)
        {
            return PlainScheme + address + PathSeparator;
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        return SecureScheme + address + PathSeparator;
#else
        return PlainScheme + address + PortSeparator + DefaultPort + PathSeparator;
#endif
    }
}
