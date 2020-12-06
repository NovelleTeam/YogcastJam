using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Managers.Platforms
{
    public class BigPlatformManager : MonoBehaviour
    {
        public GameObject rewardPlatform;
        public GameObject cables;
        public Material litUpMat;

        public void MakeSuprise()
        {
            foreach (Transform t in cables.transform) t.GetComponent<Renderer>().material = litUpMat;
            StartCoroutine(WaitForBoss());
        }

        private IEnumerator WaitForBoss()
        {
            //yield return new WaitForSeconds(3);
            transform.DOMove(transform.position - new Vector3(0, 7, 0), 7);
            yield return new WaitForSeconds(5);
            Instantiate(rewardPlatform);
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}