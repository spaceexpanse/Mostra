// See https://aka.ms/new-console-template for more information


using NostrLib;

RelayConnection relayConnection = new RelayConnection("wss://ws.postman-echo.com/raw");

await relayConnection.ConnectAsync();
Console.Write("Enter a message: ");
var message = Console.ReadLine();
await relayConnection.SendMessageAsync(!string.IsNullOrEmpty(message) ? message : "Hello World!");
var response = await relayConnection.ReceiveMessageAsync();
Console.WriteLine(response);
