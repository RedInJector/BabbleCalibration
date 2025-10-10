using System.Collections.Generic;
using BabbleCalibration.Scripts.Elements;
using Godot;
using Godot.Collections;

namespace BabbleCalibration.Scripts.Backends;

public partial class OpenVRBackend : Node, IBackend
{
    [Export] public Node ElementRoot;
    [Export] public Node3D Head;
    private XRInterface _interface;
    public Node Self => this;
    public bool IsOverlay => true;

    [Export] public Array<OpenVRElement> StartingElements = new();
    public static IBackend Create() => ResourceLoader.Load<PackedScene>("res://Scenes/Backends/OpenVRBackend.tscn").Instantiate<OpenVRBackend>();
    private Stack<OpenVRElement> _storedPool = new();
    private List<OpenVRElement> _usedPool = new();
    public void Initialize()
    {
        var global = GetNode("/root/OpenVRInterface");

        var xrInt = global.Call("get_interface").As<XRInterface>();
        xrInt.Call("set_application_type", 2);
        xrInt.Call("set_tracking_universe", 1);

        xrInt.Call("initialize");

        _interface = xrInt;

        if (StartingElements is not null)
            foreach (var elem in StartingElements) 
                _storedPool.Push(elem);
    }

    public ElementBase CreateHeadElement()
    {
        var elem = CreateElement();
        elem.HeadMode = true;
        return elem;
    }
    public ElementBase CreateWorldElement()
    {
        var elem = CreateElement();
        return elem;
    }

    public void ClearElements()
    {
        foreach (var used in _usedPool)
        {
            used.Visible = false;
            _storedPool.Push(used);
            used.Reset();
        }
        _usedPool.Clear();
    }
    public Transform3D HeadTransform() => Head.GlobalTransform;

    public Transform3D EyeTransform(bool left)
    {
        var head = Head.GlobalTransform;
        var eyeBall = _interface.GetTransformForView(left ? 1u : 0u, Transform3D.Identity);

        return new Transform3D(head.Basis, eyeBall.Origin);
    }

    private OpenVRElement CreateElement()
    {
        if (_storedPool.TryPop(out var result))
        {
            _usedPool.Add(result);
            result.Visible = true;
            return result;
        }
        result = ResourceLoader.Load<PackedScene>("res://Scenes/Elements/OpenVRElement.tscn")
            .Instantiate<OpenVRElement>();
        ElementRoot.AddChild(result);
        _usedPool.Add(result);
        return result;
    }
}