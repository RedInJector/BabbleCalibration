using Godot;
using Godot.Collections;

namespace BabbleCalibration.Scripts.Routines;

public class DebugRoutine : RoutineBase
{
    public override void Initialize(IBackend backend, Dictionary args = null)
    {
        base.Initialize(backend, args);
        
        var up = Vector3.Up * backend.HeadTransform().Origin.Y;
        CreateAxis(new Vector3(-1,0,0), "-X", Colors.DarkRed);
        CreateAxis(new Vector3(1,0,0), "+X", Colors.Red);
        CreateAxis(new Vector3(0,-1,0), "-Y", Colors.DarkGreen);
        CreateAxis(new Vector3(0,1,0), "+Y", Colors.LawnGreen);
        CreateAxis(new Vector3(0,0,-1), "-Z", Colors.DarkBlue);
        CreateAxis(new Vector3(0,0,1), "+Z", Colors.Blue);
        
        return;
        
        void CreateAxis(Vector3 direction, string text, Color color)
        {
            var label = new Label();
            label.Text = text;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.AddThemeFontSizeOverride("font_size", 64);
            label.AddThemeColorOverride("font_color", color);
            var elem = backend.CreateElementWithObject(label);

            var rotation = new Quaternion(Vector3.Forward, direction);
            elem.ElementTransform = new Transform3D(new Basis(rotation), direction + up);
        }
    }
}