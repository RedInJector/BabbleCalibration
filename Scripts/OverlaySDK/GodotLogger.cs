using System;
using Godot;
using OverlaySDK;

namespace BabbleCalibration.Scripts;

public class GodotLogger : ILogger
{
    public void Debug(string message) => GD.Print($"[Debug] {message}");

    public void Info(string message) => GD.Print($"[Info] {message}");

    public void Warn(string message) => GD.PushWarning(message);

    public void Error(string message, Exception ex = null) => GD.PushError($"{message}: {ex}");
}