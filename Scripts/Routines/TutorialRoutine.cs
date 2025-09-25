using Godot;
using Godot.Collections;

namespace BabbleCalibration.Scripts.Routines;

public class TutorialRoutine : RoutineBase
{
    public override void Initialize(IBackend backend, Dictionary args = null)
    {
        base.Initialize(backend);
        var tutorial = Backend.CreateElementWithObject(LoadScene<Control>("res://Scenes/Routines/TutorialRoutine.tscn"));
        tutorial.ElementTransform = Transform3D.Identity.TranslatedLocal((Vector3.Forward * 2) + Vector3.Up);
    }
}