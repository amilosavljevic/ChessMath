using UnityEngine;

namespace Code.Pechurka
{
    public class Collector : MonoBehaviour
    {
        private Rigidbody2D body2D;
        [SerializeField] private GameObject bottomRight;
        [SerializeField] private GameObject bottomLeft;
        private Vector3 left;
        private Vector3 right;
        
        private void Start()
        {
            body2D = GetComponent<Rigidbody2D>();
            left = bottomLeft.transform.position;
            right = bottomRight.transform.position;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var pechurka = collision.gameObject.GetComponent<Pechurka>();
            
            if (pechurka != null) 
            { 
                Destroy(pechurka.gameObject);
            } 
        }

        private void FixedUpdate()
        {
            var xOffset = Input.mousePosition.x / Screen.width;
            var newPosition = Vector3.Lerp(left, right, xOffset);
            body2D.MovePosition(newPosition);
        }
    }
}