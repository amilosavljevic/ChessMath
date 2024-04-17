using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace Code.Pechurka
{
    public class PechurkaFactory : MonoBehaviour
    {
        [SerializeField] private Pechurka pechurkaPrefab;
        [SerializeField] private Canvas canvas;
        private Vector2 min;
        private Vector2 max;
        private Random random;

        private IEnumerator Start()
        {
            var halfWidth = 0.95f * canvas.GetComponent<RectTransform>().sizeDelta.x/2;
            var halfHeight = canvas.GetComponent<RectTransform>().sizeDelta.y/2;
            min = new Vector2(-halfWidth, halfHeight);
            max = new Vector2(halfWidth, halfHeight);
            random = new Random();

            while (true)
            {
                CreatePechurka();
                yield return new WaitForSeconds(2);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private void CreatePechurka()
        {
            var localPechurka = Instantiate(pechurkaPrefab, canvas.transform);

            var newPosition = Vector3.Lerp(min, max, (float)random.NextDouble());
            localPechurka.transform.localPosition = newPosition;
        }
    }
}