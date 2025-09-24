using Godot;

namespace BabbleCalibration.Scripts.Elements;

public abstract partial class ElementBase : Node
{
    [Export] public Control Root { get; private set; }
    public abstract Transform3D ElementTransform { get; set; }
    public abstract Vector2I ElementResolution { get; set; }
    public abstract float ElementWidth { get; set; }
}