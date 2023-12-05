using System.Collections;

namespace ChessMath.Shared.Common
{
	public interface IGameService
	{
		bool IsInitialized { get; }
        IEnumerator Initialize();
	}
}
