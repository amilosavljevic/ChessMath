using System;
using UnityEngine;

namespace ChessMath.Shared.Common.AppContextNs
{
    public static class Log
    {
        public static void Error(string text) =>
            Debug.LogError(text);

        public static void Error(Exception e) =>
            Debug.LogException(e);

        public static void Info(string willTriggerSoundEvent) =>
            Debug.Log(willTriggerSoundEvent);
    }
}