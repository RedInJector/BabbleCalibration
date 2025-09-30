using System;
using BabbleCalibration.Scripts.RoutineInterfaces;
using Godot;
using Godot.Collections;

namespace BabbleCalibration.Scripts.Routines;

[Obsolete("Use VideoRoutine")]
public class TutorialRoutine : RoutineBase
{
    public LabelRoutineInterface Interface;
    public override void Initialize(IBackend backend, Dictionary args = null)
    {
        base.Initialize(backend, args);
        (var tutorial, Interface) = this.Load<LabelRoutineInterface>("res://Scenes/Routines/TutorialRoutine.tscn");
        tutorial.ElementTransform = Transform3D.Identity.TranslatedLocal((Vector3.Forward * 2) + Vector3.Up);
    }

    public override void Update(float delta)
    {
        base.Update(delta);
        Interface.Label.Text = Random.Shared.Next().ToString();
    }
}