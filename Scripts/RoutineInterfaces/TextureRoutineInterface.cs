using Godot;

namespace BabbleCalibration.Scripts.RoutineInterfaces;

public partial class TextureRoutineInterface : PanelContainer
{
    [Export] public Label Label;
    [Export] public TextureRect TextureRect;
}