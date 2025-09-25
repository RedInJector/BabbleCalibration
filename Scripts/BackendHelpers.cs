using BabbleCalibration.Scripts.Elements;
using Godot;

namespace BabbleCalibration.Scripts;

public static class BackendHelpers
{
    public static ElementBase CreateElementWithObject(this IBackend backend, Control obj, bool head = false)
    {
        var elem = head ? backend.CreateHeadElement() : backend.CreateWorldElement();
        elem.Root.AddChild(obj);
        return elem;
    }

    public static void ClearAllChildren(Node node)
    {
        foreach (var c in node.GetChildren())
        {
            node.RemoveChild(c);
            c.QueueFree();
        }
    }
}