using BabbleCalibration.Scripts.Elements;
using Godot;

namespace BabbleCalibration.Scripts.Backends;

public partial class OpenVRBackend : Node, IBackend
{
    [Export] public Node ElementRoot;
    public Node Self => this;
    public bool IsOverlay => true;
    public static IBackend Create() => ResourceLoader.Load<PackedScene>("res://Scenes/Backends/OpenVRBackend.tscn").Instantiate<OpenVRBackend>();
    public void Initialize()
    {
        var global = GetNode("/root/OpenVRInterface");

        var xrInt = global.Call("get_interface").As<XRInterface>();
        xrInt.Call("set_application_type", 2);
        xrInt.Call("set_tracking_universe", 1);

        xrInt.Call("initialize");
    }

    public ElementBase CreateHeadElement()
    {
        var elem = CreateElement();
        ElementRoot.AddChild(elem);
        return elem;
    }
    public ElementBase CreateWorldElement()
    {
        var elem = CreateElement();
        ElementRoot.AddChild(elem);
        elem.HeadMode = true;
        return elem;
    }

    public void ClearElements() => BackendHelpers.ClearAllChildren(ElementRoot);

    private OpenVRElement CreateElement() => ResourceLoader.Load<PackedScene>("res://Scenes/Elements/OpenVRElement.tscn").Instantiate<OpenVRElement>();
}