namespace ChessMath.Shared.Common
{
    public interface IResourceProvider
    {
        T Load<T>(string fullPath);
        Async<T> LoadAsync<T>(string fullPath);
    }
}
