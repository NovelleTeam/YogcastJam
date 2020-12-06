using System.Collections;
using Managers;
using UnityEngine;

namespace Controllers.Enviroment
{
    public class MiniPlatform : MonoBehaviour
    {
        [SerializeField] private Material _initialMaterial;

        private bool _wasSteppedOn;
        private Vector3 _initialLocation;
        private Quaternion _initialRotation;

        private void Start()
        {
            _initialLocation = transform.position;
            _initialRotation = transform.rotation;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player") && !_wasSteppedOn)
            {
                StartCoroutine(WaitForDrop());
                GetComponent<Renderer>().material = other.gameObject.GetComponent<PlayerManager>().miniPlatformLitUp;
                _wasSteppedOn = true;
            }
            else if (other.gameObject.CompareTag("DeathFloor"))
            {
                Destroy(GetComponent<Rigidbody>());
                GetComponent<Renderer>().material = _initialMaterial;
                _wasSteppedOn = false;
                transform.position = _initialLocation;
                transform.rotation = _initialRotation;
            }
        }

        private IEnumerator WaitForDrop()
        {
            yield return new WaitForSeconds(2);
            gameObject.AddComponent<Rigidbody>();
        }
    }
}