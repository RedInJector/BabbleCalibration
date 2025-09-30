using Godot;
using Godot.Collections;

namespace BabbleCalibration.Scripts;

public static class RoutineHelpers
{
    public static Dictionary LabelRoutineArgs(string text, bool head = false, Transform3D? transform = null)
    {
        return new Dictionary
        {
            {"text", text },
            {"head", head },
            {"transform", transform ?? Transform3D.Identity.TranslatedLocal(Vector3.Forward + Vector3.Up) }
        };
    }
    
    public static Dictionary FilePathRoutineArgs(string filePath, string text, bool head = false, Transform3D? transform = null)
    {
        return new Dictionary
        {
            {"path", filePath },
            {"text", text },
            {"head", head },
            {"transform", transform ?? Transform3D.Identity.TranslatedLocal(Vector3.Forward + Vector3.Up) }
        };
    }
    
    public static Dictionary LabelTimerRoutineArgs(string text, float time, bool head = false, Transform3D? transform = null)
    {
        return new Dictionary
        {
            {"text", text },
            {"head", head },
            {"transform", transform ?? Transform3D.Identity.TranslatedLocal(Vector3.Forward + Vector3.Up) },
            {"time", time }
        };
    }
    
    public static Dictionary ReticleRoutineArgs(Transform3D transform, float time)
    {
        return new Dictionary
        {
            {"time", time },
            {"transform", transform },
        };
    }
}