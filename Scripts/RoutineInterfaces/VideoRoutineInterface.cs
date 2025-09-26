using Godot;

namespace BabbleCalibration.Scripts.RoutineInterfaces;

public partial class VideoRoutineInterface : PanelContainer
{
    [Export] public Label Label;
    [Export] public VideoStreamPlayer Video;
}