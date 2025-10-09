using BabbleCalibration.Scripts.Elements;
using Godot;

namespace BabbleCalibration.Scripts.Backends;

public partial class OpenVRBackend : Node, IBackend
{
    [Export] public Node ElementRoot;
    [Export] public XRController3D Head;
    private XRInterface _interface;
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

        _interface = xrInt;
    }

    public ElementBase CreateHeadElement()
    {
        GD.Print("Creating world element");
        var elem = CreateElement();
        GD.Print("Adding child");
        ElementRoot.AddChild(elem);
        elem.HeadMode = true;
        return elem;
    }
    public ElementBase CreateWorldElement()
    {
        GD.Print("Creating world element");
        var elem = CreateElement();
        GD.Print("Adding child");
        ElementRoot.AddChild(elem);
        return elem;
    }

    public void ClearElements() => BackendHelpers.ClearAllChildren(ElementRoot);
    public Transform3D HeadTransform() => Head.GlobalTransform;

    public Transform3D EyeTransform(bool left)
    {
        var head = Head.GlobalTransform;
        var eyeBall = _interface.GetTransformForView(left ? 1u : 0u, Transform3D.Identity);

        return new Transform3D(head.Basis, eyeBall.Origin);
    }

    private OpenVRElement CreateElement()
    {
        GD.Print("Creating element");
        return ResourceLoader.Load<PackedScene>("res://Scenes/Elements/OpenVRElement.tscn")
            .Instantiate<OpenVRElement>();
    }
}