using Godot;
using System;
using System.Linq;
using BabbleCalibration.Scripts;
using BabbleCalibration.Scripts.Backends;
using BabbleCalibration.Scripts.Routines;
using Godot.Collections;
using OverlaySDK;
using OverlaySDK.Packets;

public partial class MainScene : Node
{
    public static MainScene Instance { get; private set; }
    public IBackend Backend { get; private set; }
    public RoutineBase CurrentRoutine { get; private set; }

    public GodotPacketHandler PacketHandler { get; private set; }

    private bool _sendPackets = true;
    
    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        try
        {
            PacketHandler = new GodotPacketHandler
            (
                new OverlayMessageDispatcher
                (
                    new GodotLogger(), 
                    new EventDrivenJsonClient
                    (
                        new EventDrivenTcpClient(new SocketFactory().CreateClient("127.0.0.1", 2425)) //TODO put the correct port here
                    )
                )
            );
        }
        catch
        {
            _sendPackets = false;
            GD.Print("blame red");
        }
        
        var args = OS.GetCmdlineArgs();
        var argsLower = args.Select(i => i.ToLowerInvariant().Trim()).ToArray();
        
        var enableXr = false;
        var enableXrOverlay = false;
        var enableOpenVr = false;
        var enableDebug = false;
        
        var xrInterface = XRServer.FindInterface("OpenXR");
        if (xrInterface != null && xrInterface.IsInitialized()) enableXr = true;

        foreach (var item in argsLower)
        {
            if (item == "--use-openvr") enableOpenVr = true;
            else if (item == "--use-debug") enableDebug = true;
            else if (item == "--use-openxr-overlay") enableXrOverlay = true;
        }

        if (!enableOpenVr && !enableXr && !enableDebug) throw new Exception("Invalid configuration, no backend provided");
        if (enableOpenVr && (enableXr || enableXrOverlay)) throw new Exception("Invalid configuration, OpenXR cannot be enabled at the same time as OpenVR");
        if (enableXrOverlay && !enableXr) throw new Exception("Invalid configuration, OpenXR must be enabled to use OpenXR Overlay");

        if (enableOpenVr) Backend = OpenVRBackend.Create();
        else if (enableDebug) Backend = DebugBackend.Create();
        else Backend = enableXrOverlay ? OpenXROverlayBackend.Create() : OpenXRBackend.Create();
        
        AddChild(Backend.Self);
        
        Backend.Initialize();
        
        StartRoutine<TextRoutine>(RoutineHelpers.LabelRoutineArgs("Connecting to Baballonia...", true, Transform3D.Identity.TranslatedLocal(Vector3.Forward)));
    }

    public void SendPacket<T>(T packet) where T : IPacket
    {
        if (!_sendPackets) return;
        PacketHandler.Dispatcher.Dispatch(packet);
    }

    public void StartRoutine(string name, float time = 0)
    {
        switch (name)
        {
            case "gazetutorial":
                StartRoutine<VideoRoutine>(RoutineHelpers.FilePathRoutineArgs("res://Assets/BabbleCalibration.ogv",
                    "TODO i dont know what text is supposed to go here {0}", true,
                    Transform3D.Identity.TranslatedLocal(Vector3.Forward)));
                break;
            case "gaze":
                StartRoutine<ReticleRoutine>(RoutineHelpers.ReticleRoutineArgs(
                    Transform3D.Identity.TranslatedLocal((Vector3.Forward * 3) + Vector3.Up), time));
                break;
            case "blinktutorial":
                StartRoutine<TextTimerRoutine>(RoutineHelpers.LabelTimerRoutineArgs("in {0} seconds you close eyes okay",
                    time, true, Transform3D.Identity.TranslatedLocal(Vector3.Forward)));
                break;
            case "blink":
                StartRoutine<TextTimerRoutine>(RoutineHelpers.LabelTimerRoutineArgs("KEEP EYES CLOSED {0} MORE SECONDS",
                    time, true, Transform3D.Identity.TranslatedLocal(Vector3.Forward)));
                break;
            case "close":
                GetTree().Quit();
                break;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        var deltaf = (float)delta;
        CurrentRoutine?.Update(deltaf);
    }

    public void StartRoutine<T>(Dictionary args = null) where T : RoutineBase, new()
    {
        CurrentRoutine?.End();
        Backend.ClearElements();
        CurrentRoutine = new T();
        CurrentRoutine.Initialize(Backend, args);

        var elem = Backend.CreateElementWithObject(ResourceLoader.Load<PackedScene>("res://Scenes/Routines/FloorIndicator.tscn").Instantiate<PanelContainer>());
        elem.ElementTransform = new Transform3D(new Basis(new Quaternion(Vector3.Forward, Vector3.Down)), Vector3.Up * 0.001f);
        elem.ElementWidth = 1.5f;
    }
}
