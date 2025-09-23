using Godot;

namespace BabbleCalibration.Scripts.Backends;

public partial class OpenVRBackend : Node, IBackend
{
    public Node Self => this;
    public bool IsOverlay => true;
    public static IBackend Create()
    {
        throw new System.NotImplementedException();
    }
    public void Initialize()
    {
        throw new System.NotImplementedException();
    }
}