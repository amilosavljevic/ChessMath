namespace ChessMath.Shared.Common.SettingsProviderNs
{
    public class SchemaSettingsProvider : ISettingsProvider
    {
        private readonly SettingsCache cache;

        public SchemaSettingsProvider(object settingsSchema) =>
            cache = new SettingsCache(settingsSchema);

        public T GetSettings<T>() where T : class => cache.Get<T>();
    }
}