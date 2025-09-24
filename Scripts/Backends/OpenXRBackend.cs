using BabbleCalibration.Scripts.Elements;
using Godot;

namespace BabbleCalibration.Scripts.Backends;

public partial class OpenXRBackend : Node, IBackend
{
    public Node Self => this;
    public bool IsOverlay => false;
    public static IBackend Create() => ResourceLoader.Load<PackedScene>("res://Backends/OpenXRBackend.tscn").Instantiate<OpenXRBackend>();

    public void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public ElementBase CreateHeadElement()
    {
        throw new System.NotImplementedException();
    }

    public ElementBase CreateWorldElement()
    {
        throw new System.NotImplementedException();
    }
}