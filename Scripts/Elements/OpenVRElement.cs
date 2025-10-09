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
    public override float ElementWidth { get; set; }

    public override void _Ready()
    {
        base._Ready();
        UpdateTransforms();
    }

    private void UpdateTransforms()
    {
        if (!IsInsideTree()) return;
        
        var absolute = _headMode ? Transform3D.Identity : ElementTransform;
        var relative = _headMode ? ElementTransform : Transform3D.Identity;

        _overlayContainer.Call("set_absolute_position", absolute);
        _overlayContainer.Call("set_tracked_device_relative_position", relative);
        _overlayContainer.Call("set_tracked_device_name", _headMode ? "hmd" : "");
    }
}