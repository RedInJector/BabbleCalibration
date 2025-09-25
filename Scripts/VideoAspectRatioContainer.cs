using Godot;

namespace BabbleCalibration.Scripts;

public partial class VideoAspectRatioContainer : AspectRatioContainer
{
    [Export] public VideoStreamPlayer Video;

    private string _videoFile;

    public override void _Process(double delta)
    {
        base._Process(delta);

        var file = Video.Stream.File;
        
        if (Video is not null && file != _videoFile)
        {
            _videoFile = file;
            var size = Video.GetVideoTexture().GetSize();
            Ratio = size.X / size.Y;
        }
    }
}