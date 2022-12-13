using UnityEngine;


namespace ReganAlchemy
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private float _followSpeed = 1;

        [SerializeField]
        private Transform _followTransform;


        void Update()
        {
            Vector3 position3D = transform.position;
            Vector3 newPosition3D = Vector3.Lerp(position3D, _followTransform.position, _followSpeed * Time.deltaTime);

            transform.position = newPosition3D;
        }
    }
}