using Godot;

namespace BabbleCalibration.Scripts;

public interface IBackend
{
    /// <summary>
    /// Returns the node of this backend
    /// </summary>
    public abstract Node Self { get; }
    /// <summary>
    /// Returns whether or not the backend is an overlay, or a standalone app.<br/>
    /// If a backend is an overlay, a transparent background should be used
    /// </summary>
    public abstract bool IsOverlay { get; }
    /// <summary>
    /// Creates an instance of the backend
    /// </summary>
    /// <returns>The created backend</returns>
    public static abstract IBackend Create();
    /// <summary>
    /// Initializes the backend
    /// </summary>
    public abstract void Initialize();
}