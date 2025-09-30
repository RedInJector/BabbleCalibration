using BabbleCalibration.Scripts.RoutineInterfaces;
using Godot;
using Godot.Collections;

namespace BabbleCalibration.Scripts.Routines;

public class TextTimerRoutine : RoutineBase
{
    public string Text = "{0}";
    public LabelTimerRoutineInterface Interface;
    public override void Initialize(IBackend backend, Dictionary args = null)
    {
        base.Initialize(backend, args);
        
        if (args is not null)
        {
            var head = false;
            var transform = Transform3D.Identity.TranslatedLocal(Vector3.Forward + Vector3.Up);
            var time = 10f;
            
            if (args.TryGetValue("text", out var value) && value.VariantType is Variant.Type.String) 
                Text = value.AsString();
            if (args.TryGetValue("head", out value) && value.VariantType is Variant.Type.Bool) 
                head = value.AsBool();
            if (args.TryGetValue("transform", out value) && value.VariantType is Variant.Type.Transform3D) 
                transform = value.AsTransform3D();
            if (args.TryGetValue("time", out value) && value.VariantType is Variant.Type.Float)
                time = value.AsSingle();
            
            
            (var element, Interface) = this.Load<LabelTimerRoutineInterface>("res://Scenes/Routines/TextTimerRoutine.tscn", head);
            element.ElementTransform = transform;

            Interface.Timer.WaitTime = time;
            Interface.Timer.Start();
            
            UpdateText();
        }
    }

    public override void Update(float delta)
    {
        base.Update(delta);
        UpdateText();
    }

    private void UpdateText()
    {
        var timerTime = Mathf.FloorToInt(Interface.Timer.TimeLeft).ToString();
        var text = string.Format(Text, timerTime);
        Interface.Label.Text = text;
    }
}