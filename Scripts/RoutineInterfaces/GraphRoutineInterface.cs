using Godot;

namespace BabbleCalibration.Scripts.RoutineInterfaces;

public partial class GraphRoutineInterface : PanelContainer
{
    [Export] public GraphControl Graph;
    [Export] public ProgressBar EpochBar;
    [Export] public Label EpochText;
    [Export] public ProgressBar BatchBar;
    [Export] public Label BatchText;
    
    [Export] public Label LossText; //,̶'̶ ̶ ̶,̶ ̶|̶ ̶,̶'̶ ̶_̶'̶
    [Export] public Label TimeText;
}