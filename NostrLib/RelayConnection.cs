namespace NostrLib;

using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class RelayConnection
{
    private readonly Uri _serverUri;
    private readonly ClientWebSocket _clientWebSocket;

    public RelayConnection(string serverUri)
    {
        _serverUri = new Uri(serverUri);
        _clientWebSocket = new ClientWebSocket();
    }

    public async Task ConnectAsync()
    {
        await _clientWebSocket.ConnectAsync(_serverUri, CancellationToken.None);
    }

    public async Task SendMessageAsync(string message)
    {
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        await _clientWebSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    public async Task<string> ReceiveMessageAsync()
    {
        ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
        WebSocketReceiveResult result = await _clientWebSocket.ReceiveAsync(buffer, CancellationToken.None);
        return Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
    }
}