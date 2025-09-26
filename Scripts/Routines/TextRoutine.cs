using System;
using BabbleCalibration.Scripts.RoutineInterfaces;
using Godot;
using Godot.Collections;

namespace BabbleCalibration.Scripts.Routines;

public class TextRoutine : RoutineBase
{
    public override void Initialize(IBackend backend, Dictionary args = null)
    {
        base.Initialize(backend, args);
        
        if (args is not null)
        {
            var text = "";
            var head = false;
            var transform = Transform3D.Identity.TranslatedLocal(Vector3.Forward + Vector3.Up);
            
            if (args.TryGetValue("text", out var value) && value.VariantType is Variant.Type.String) 
                text = value.AsString();
            if (args.TryGetValue("head", out value) && value.VariantType is Variant.Type.Bool) 
                head = value.AsBool();
            if (args.TryGetValue("transform", out value) && value.VariantType is Variant.Type.Transform3D) 
                transform = value.AsTransform3D();
            
            var (tutorial, interf) = this.Load<LabelRoutineInterface>("res://Scenes/Routines/TextRoutine.tscn", head);
            interf.Label.Text = text;
            tutorial.ElementTransform = transform;
        }
    }
}