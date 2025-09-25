using Godot;
using System;
using System.Linq;
using BabbleCalibration.Scripts;
using BabbleCalibration.Scripts.Backends;
using BabbleCalibration.Scripts.Routines;
using Godot.Collections;

public partial class MainScene : Node
{
    public static MainScene Instance { get; private set; }
    public IBackend Backend { get; private set; }
    public RoutineBase CurrentRoutine { get; private set; }
    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        var args = OS.GetCmdlineArgs();
        var argsLower = args.Select(i => i.ToLowerInvariant().Trim()).ToArray();
        
        var enableXr = false;
        var enableXrOverlay = false;
        var enableOpenVr = false;
        
        var xrInterface = XRServer.FindInterface("OpenXR");
        if (xrInterface != null && xrInterface.IsInitialized()) enableXr = true;

        foreach (var item in argsLower)
        {
            if (item == "--use-openvr") enableOpenVr = true;
            else if (item == "--use-openxr-overlay") enableXrOverlay = true;
        }

        if (!enableOpenVr && !enableXr) throw new Exception("Invalid configuration, no backend provided");
        if (enableOpenVr && (enableXr || enableXrOverlay)) throw new Exception("Invalid configuration, OpenXR cannot be enabled at the same time as OpenVR");
        if (enableXrOverlay && !enableXr) throw new Exception("Invalid configuration, OpenXR must be enabled to use OpenXR Overlay");

        if (enableOpenVr) Backend = OpenVRBackend.Create();
        else Backend = enableXrOverlay ? OpenXROverlayBackend.Create() : OpenXRBackend.Create();
        
        AddChild(Backend.Self);
        
        Backend.Initialize();
        
        StartRoutine<TutorialRoutine>();
    }

    public void StartRoutine<T>(Dictionary args = null) where T : RoutineBase, new()
    {
        CurrentRoutine?.End();
        Backend.ClearElements();
        CurrentRoutine = new T();
        CurrentRoutine.Initialize(Backend, args);
    }
}
