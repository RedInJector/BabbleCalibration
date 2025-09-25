using Godot;
using Godot.Collections;

namespace BabbleCalibration.Scripts;

public abstract class RoutineBase
{
    public IBackend Backend { get; private set; }

    public virtual void Initialize(IBackend backend, Dictionary args = null) => Backend = backend;
    public virtual void Update(float delta){}
    public virtual void End(){}

    public static T LoadScene<T>(string path) where T : Node => ResourceLoader.Load<PackedScene>(path).Instantiate<T>();
}