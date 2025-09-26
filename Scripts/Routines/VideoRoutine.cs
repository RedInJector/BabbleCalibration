using BabbleCalibration.Scripts.RoutineInterfaces;
using Godot;
using Godot.Collections;

namespace BabbleCalibration.Scripts.Routines;

public class VideoRoutine : RoutineBase
{
    public override void Initialize(IBackend backend, Dictionary args = null)
    {
        base.Initialize(backend, args);
        
        if (args is not null)
        {
            var text = "";
            var head = false;
            var transform = Transform3D.Identity.TranslatedLocal(Vector3.Forward + Vector3.Up);
            var videoPath = "res://Assets/BabbleCalibration.ogv";
            
            if (args.TryGetValue("text", out var value) && value.VariantType is Variant.Type.String) 
                text = value.AsString();
            if (args.TryGetValue("path", out value) && value.VariantType is Variant.Type.String) 
                videoPath = value.AsString();
            if (args.TryGetValue("head", out value) && value.VariantType is Variant.Type.Bool) 
                head = value.AsBool();
            if (args.TryGetValue("transform", out value) && value.VariantType is Variant.Type.Transform3D) 
                transform = value.AsTransform3D();
            
            var (tutorial, interf) = this.Load<VideoRoutineInterface>("res://Scenes/Routines/VideoRoutine.tscn", head);
            interf.Video.Stream = ResourceLoader.Load<VideoStream>(videoPath);
            interf.Video.Play();
            interf.Label.Text = text;
            tutorial.ElementTransform = transform;
        }
    }
}