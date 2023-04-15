namespace NostrLib;

public class RelayManager
{
    public bool AddRelay(string uri)
    {
        try
        {
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


        return didOneFail;
    }

    public bool Connect(string uri)
    {
        var didFail = false;

        try
        {
   
        }
        catch (Exception e)
        {
            System.Diagnostics.Trace.TraceError("Failed to connect to " + uri);
            didFail = true;
        }

        return didFail;
    }
}