using System;
using ChessMath.Shared.Common;
using ChessMath.Shared.UnityCommon.Utils;
using UnityEngine;

namespace ChessMath.Shared.UnityCommon.Implementations
{
	public class UnityProxyService : IEngineProxy
	{
		public MonoBehaviourProxy Proxy { get; }
		public GameObject GameObject => Proxy.gameObject;
		
		public event Action<float> UpdateTicked;
		public event Action<float> LateUpdateTicked;
		public event Action<float> FixedUpdateTicked;

		public UnityProxyService()
		{
            Proxy = MonoBehaviourProxy.I;
            Proxy.UpdateTicked += dt => UpdateTicked?.Invoke(dt);
			Proxy.LateUpdateTicked += dt => LateUpdateTicked?.Invoke(dt);
            Proxy.FixedUpdateTicked += dt => FixedUpdateTicked?.Invoke(dt);
        }
    }
}
