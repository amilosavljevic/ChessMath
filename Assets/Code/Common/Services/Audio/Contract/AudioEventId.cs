namespace ChessMath.Shared.Common
{
    public readonly struct AudioEventId
    {
        public readonly string Id;

        public AudioEventId (string id) =>
            Id = id;

        public static implicit operator AudioEventId(string id) => new AudioEventId(id);
        public static implicit operator string(AudioEventId id) => id.Id;
    }

    public static class AudioEventExtensions
    {
        public static void Trigger(this in AudioEventId eventId)
        {
            var audioService = GlobalContext.GetService<IAudioService>();
            audioService.TriggerEvent(eventId);
        }
        
        public static void Trigger(this in AudioEventId eventId, IAudioService audioService) =>
            audioService.TriggerEvent(eventId);
        
        public static void Trigger(this in AudioEventId eventId, IContext context) =>
            context.GetService<IAudioService>().TriggerEvent(eventId);
    }
}