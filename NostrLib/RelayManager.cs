namespace NostrLib;

public class RelayManager
{
    private readonly Dictionary<string, NNostr.Client.NostrClient> _clients = new();

    public bool AddRelay(string uri)
    {
        try
        {
            var relay = new NNostr.Client.NostrClient(new Uri(uri));
            _clients.Add(uri, relay);
            return true;
        }
        catch (Exception e)
        {
            System.Diagnostics.Trace.TraceError("Could not add " + uri + " to the relay list.");
            return false;
        }
    }

    public bool ConnectAll()
    {
        var didOneFail = false;
        foreach (var relay in _clients)
        {
            try
            {
                relay.Value.Connect();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError("Failed to connect to " + relay.Value);
                didOneFail = true;
            }
        }

        return didOneFail;
    }

    public bool Connect(string uri)
    {
        var didFail = false;

        try
        {
            _clients[uri].Connect();
            
        }
        catch (Exception e)
        {
            System.Diagnostics.Trace.TraceError("Failed to connect to " + uri);
            didFail = true;
        }

        return didFail;
    }
}