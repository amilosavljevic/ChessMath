namespace ChessMath.Shared.Common.SettingsProviderNs
{
    public class EmptySettingsProvider : ISettingsProvider
    {
        public T GetSettings<T>() where T : class => null;
    }
}