using BabbleCalibration.Scripts.Elements;
using Godot;

namespace BabbleCalibration.Scripts.Backends;

public partial class DebugBackend : Node, IBackend
{
    [Export] public Camera3D Camera;
    [Export] public Node ElementRoot;
    public Node Self => this;
    public bool IsOverlay => false;
    public static IBackend Create() => ResourceLoader.Load<PackedScene>("res://Scenes/Backends/DebugBackend.tscn").Instantiate<DebugBackend>();

    public void Initialize()
    {
        
    }

    public ElementBase CreateHeadElement()
    {
        var elem = OpenXRElement.CreateElement();
        elem.Head = Camera;
        ElementRoot.AddChild(elem);
        return elem;
    }

    public ElementBase CreateWorldElement()
    {
        var elem = OpenXRElement.CreateElement();
        ElementRoot.AddChild(elem);
        return elem;
    }

    public void ClearElements() => BackendHelpers.ClearAllChildren(ElementRoot);
}