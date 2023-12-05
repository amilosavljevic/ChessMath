namespace ChessMath.Shared.Common
{
    public interface ISettingsProvider
    {
        T GetSettings<T>() where T : class;
    }
}