using ChessMath.Shared.Common.AppContextNs;

namespace ChessMath.Shared.Common
{
    public class DebugAudioServiceDecorator : IAudioService
    {
        private readonly IAudioService audioService;
        
        public DebugAudioServiceDecorator(IAudioService audioService)
        {
            this.audioService = audioService;
        }

        public void TriggerEvent(string id)
        {
            Log.Info("Will trigger sound event: " + id);
            audioService?.TriggerEvent(id);
        }
    }

    public static class DebugAudioServiceDecoratorExtensions
    {
        public static DebugAudioServiceDecorator WithDebugLogs(this IAudioService serviceToDecorate) =>
            new DebugAudioServiceDecorator(serviceToDecorate);
    }
}