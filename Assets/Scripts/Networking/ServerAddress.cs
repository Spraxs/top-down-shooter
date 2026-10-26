/// <summary>
/// Turns what a player typed in the main menu into the WebSocket URL of a server.
///
/// A ws:// or wss:// URL is used as it is, with the WebSocket path added when it names no
/// path. A bare "host:port" is the old form and becomes ws://host:port/websocket. A host
/// without a port means the default server port on a desktop build, and wss:// on the
/// standard port in a browser: a page served over HTTPS may only open secure WebSockets.
///
/// The path matters: the server answers "/" with an HTML test page, and only upgrades
/// other paths to a WebSocket.
/// </summary>
public static class ServerAddress
{
    public const string PreferenceKey = "server";

    public const int DefaultPort = 25565;

    // The public server on the cluster. What a browser build connects to unless the player types another.
    public const string HostedServer = "wss://radioactive-server.spraxs.nl/websocket";

    private const string WebSocketPath = "/websocket";
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

        if (address.StartsWith(SecureScheme))
        {
            return SecureScheme + WithPath(address.Substring(SecureScheme.Length));
        }

        if (address.StartsWith(PlainScheme))
        {
            return PlainScheme + WithPath(address.Substring(PlainScheme.Length));
        }

        if (address.IndexOf(PortSeparator) >= 0)
        {
            return PlainScheme + WithPath(address);
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        return SecureScheme + WithPath(address);
#else
        return PlainScheme + WithPath(address + PortSeparator + DefaultPort);
#endif
    }

    // "host[:port]" or "host[:port]/path" without the scheme; a missing or bare "/" path becomes the WebSocket path.
    private static string WithPath(string hostAndPath)
    {
        int pathStart = hostAndPath.IndexOf(PathSeparator);

        if (pathStart < 0)
        {
            return hostAndPath + WebSocketPath;
        }

        string path = hostAndPath.Substring(pathStart);

        if (path == PathSeparator.ToString())
        {
            return hostAndPath.Substring(0, pathStart) + WebSocketPath;
        }

        return hostAndPath;
    }
}
