using System.Collections;
using Managers;
using UnityEngine;

namespace Controllers.Enviroment
{
    public class MiniPlatform : MonoBehaviour
    {
        private bool _wasSteppedOn;

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
                Destroy(gameObject);
            }
        }

        private IEnumerator WaitForDrop()
        {
            yield return new WaitForSeconds(2);
            gameObject.AddComponent<Rigidbody>();
        }
    }
}