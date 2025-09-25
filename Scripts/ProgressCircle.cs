using Godot;

namespace BabbleCalibration.Scripts;

public partial class ProgressCircle : PanelContainer
{
    [Export] public float Time = 5;
    [Export] public bool AutoStart;
    
    [ExportGroup("Internal")]
    [Export] private ShaderMaterial _shaderMaterial;
    [Export] public Timer Timer { get; private set; }

    private static readonly StringName ProgressParam = "Progress";

    public override void _Ready()
    {
        base._Ready();
        Timer.WaitTime = Time;
        if (AutoStart) Timer.Start();
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
        _shaderMaterial.SetShaderParameter(ProgressParam, (float)(Timer.TimeLeft / Time));
    }
}