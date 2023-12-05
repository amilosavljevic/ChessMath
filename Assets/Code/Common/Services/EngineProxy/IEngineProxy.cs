using System;

namespace ChessMath.Shared.Common
{
    public interface IEngineProxy
    {
        public event Action<float> UpdateTicked;
        public event Action<float> LateUpdateTicked;
        public event Action<float> FixedUpdateTicked;
    }
}