using BabbleCalibration.Scripts.Routines;
using Godot;
using OverlaySDK;
using OverlaySDK.Packets;

namespace BabbleCalibration.Scripts;

public class GodotPacketHandler : PacketHandlerAdapter
{
    public OverlayMessageDispatcher Dispatcher { get; private set; }

    public GodotPacketHandler(OverlayMessageDispatcher dispatcher)
    {
        Dispatcher = dispatcher;
        Dispatcher.RegisterHandler(this);
    }

    public override void OnStartRoutine(RunFixedLenghtRoutinePacket routine)
    {
        base.OnStartRoutine(routine);

        var name = routine.RoutineName;
        
        Callable.From(() =>
        {
            var routineToDo = name.ToLower().Trim();
            MainScene.Instance.StartRoutine(routineToDo);
        }).CallDeferred();
    }
}