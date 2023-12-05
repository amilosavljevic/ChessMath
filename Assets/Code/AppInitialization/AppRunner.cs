using System.Collections;
using UnityEngine;

namespace Solitaire.Application
{
    public class AppRunner : MonoBehaviour
    {
        public IEnumerator Start()
        {
            var appContext = new ChessMathContext();
            yield return appContext.Initialize();
            
            // TODO: Load main scene
        }
    }
}
