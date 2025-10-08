using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using BabbleCalibration.Scripts.RoutineInterfaces;
using Godot;
using Godot.Collections;
using OverlaySDK.Packets;

namespace BabbleCalibration.Scripts.Routines;

public partial class GraphRoutine : RoutineBase
{
    private GraphRoutineInterface _interface;
    private Stopwatch _stopwatch = new();
    private List<Vector2> _points = new();

    public int EpochCount = -1;
    public int BatchCount = -1;
    
    public int EpochCurrent = -1;
    public int BatchCurrent = -1;
    public override void Initialize(IBackend backend, Dictionary args = null)
    {
        base.Initialize(backend, args);
        
        _stopwatch.Start();
        (var element, _interface) = this.Load<GraphRoutineInterface>("res://Scenes/Routines/GraphRoutine.tscn", true);
        element.ElementTransform = Transform3D.Identity.TranslatedLocal(Vector3.Forward + (Vector3.Down * 0.25f));
    }

    public void Handle(TrainerProgressReportPacket packet)
    {
        var loss = (float)packet.Loss;
        _interface.LossText.Text = $"Current Loss: {loss:F6}";

        var isEpoch = packet.ProgressName is "Epoch";
        var targetLabel = isEpoch ? _interface.EpochText : _interface.BatchText;
        var targetBar = isEpoch ? _interface.EpochBar : _interface.BatchBar;

        targetLabel.Text = $"{packet.ProgressName}: {packet.CurrentProgress}/{packet.TargetProgress}";
        targetBar.MaxValue = packet.TargetProgress;
        targetBar.Value = packet.CurrentProgress;

        if (isEpoch)
        {
            EpochCount = packet.TargetProgress;
            EpochCurrent = packet.CurrentProgress;
        }
        else
        {
            BatchCount = packet.TargetProgress;
            BatchCurrent = packet.CurrentProgress;
        }
        var currentTime = (float)_stopwatch.Elapsed.TotalSeconds;

        _points.Add(new Vector2(currentTime, loss));
        _interface.Graph.Points = _points.Count > 32 ? _points.TakeLast(32).ToArray() : _points.ToArray();
        _interface.QueueRedraw();

        if (EpochCount >= 0 && BatchCount >= 0 && EpochCurrent + BatchCurrent > 0)
        {
            var totalBatchCount = EpochCount * BatchCount;
            var completedBatches = (EpochCurrent * BatchCount) + BatchCurrent;

            var progress = (float)completedBatches / totalBatchCount;
            var remaining = 1 - progress;
            var multiplier = remaining / progress;

            var eta = currentTime * multiplier;
            _interface.TimeText.Text = $"ETA: {TimeSpan.FromSeconds(eta):g}";
        }
    }
}