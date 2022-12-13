using UnityEngine;

namespace ReganAlchemy
{
    public class ConveyorSurface : MonoBehaviour
    {
        Rigidbody _rigidbody;
        [SerializeField]
        float speed = 0.5f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Vector3 startPos = _rigidbody.transform.position;
            _rigidbody.position += speed * Time.deltaTime * _rigidbody.transform.up;
            _rigidbody.MovePosition(startPos);
        }
    }
}