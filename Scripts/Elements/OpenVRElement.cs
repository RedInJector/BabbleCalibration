using Godot;

namespace BabbleCalibration.Scripts.Elements;

public partial class OpenVRElement : ElementBase
{
    [Export] private SubViewportContainer _overlayContainer;
    [Export] private SubViewport _viewport;

    [Export]
    public bool HeadMode
    {
        get => _headMode;
        set
        {
            if (value == _headMode) return;
            _headMode = value;
            UpdateTransforms();
        }
    }
    public bool Visible
    {
        get => _overlayContainer.Call("is_overlay_visible").AsBool();
        set => _overlayContainer.Call("set_overlay_visible", true);
    }

    private bool _headMode = false;

    private Transform3D _transformValue = Transform3D.Identity;

    public override Transform3D ElementTransform
    {
        get => _transformValue;
        set
        {
            _transformValue = value;
            UpdateTransforms();
        }
    }

    public override Vector2I ElementResolution
    {
        get => _viewport.Size;
        set => _viewport.Size = value;
    }

    public override float ElementWidth
    {
        get => _overlayContainer.Call("get_overlay_width_in_meters").AsSingle();
        set => _overlayContainer.Call("set_overlay_width_in_meters", value);
    }

    public override void _Ready()
    {
        base._Ready();
        UpdateTransforms();
    }

    public void Reset()
    {
        BackendHelpers.ClearAllChildren(Root);
        ElementTransform = Transform3D.Identity;
        ElementResolution = Vector2I.One * 512;
        ElementWidth = 1;
        HeadMode = false;
    }

    /*
    public override void _Process(double delta)
    {
        base._Process(delta);
        
        var transform = ElementTransform;
        
        if (!_headMode) transform = MainScene.Instance.Backend.HeadTransform().AffineInverse() * transform;
        
        _overlayContainer.Call("set_tracked_device_relative_position", transform);
    }
    */

    private void UpdateTransforms()
    {
        if (!IsInsideTree()) return;

        var transform = ElementTransform;
        var quaternion = transform.Basis.GetRotationQuaternion(); 
        var newQuat = new Quaternion(-quaternion.X, -quaternion.Y, quaternion.Z, quaternion.W);

        var newTransform = new Transform3D(new Basis(newQuat).Scaled(transform.Basis.Scale), transform.Origin);
        
        _overlayContainer.Call("set_absolute_position", newTransform);
        _overlayContainer.Call("set_tracked_device_relative_position", newTransform);
        _overlayContainer.Call("set_tracked_device_name", _headMode ? "hmd" : "");
    }
}