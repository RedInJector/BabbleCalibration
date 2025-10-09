using System;
using System.Text.Json;
using OverlaySDK;
using OverlaySDK.Packets;

namespace BabbleCalibration.Scripts;

public class TestClient : IEventDrivenConnection<object, JsonDocument>, IDisposable
{
    public void Dispose()
    {
        
    }

    public bool Connected() => true;

    public void Send(object obj)
    {
        
    }

    public void SendPacket<T>(T packet) where T : IPacket
    {
        var obj = JsonDocument.Parse(JsonSerializer.Serialize(new Packet<T>(packet)));
        DataReceived(obj);
    }
    public event Action<JsonDocument> DataReceived = _ => {};
    public event Action OnDisconnect = () => {};
}