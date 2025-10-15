using Godot;
using Godot.Collections;

namespace BabbleCalibration.Scripts;

public abstract class RoutineBase
{
    public virtual bool PlaySounds => false;
    protected Transform3D OriginOffset => MainScene.Instance.OriginOffset;
    public IBackend Backend { get; private set; }

    public virtual void Initialize(IBackend backend, Dictionary args = null)
    {
        Backend = backend;
        if (PlaySounds) MainScene.Instance.PlayStartSound();
    }

    public virtual void Update(float delta){}

    public virtual void End()
    {
        if (PlaySounds) MainScene.Instance.PlayEndSound();
    }

    public static T LoadScene<T>(string path) where T : Node => ResourceLoader.Load<PackedScene>(path).Instantiate<T>();
}