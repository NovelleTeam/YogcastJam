using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Managers
{
    public class BigPlatformManager : MonoBehaviour
    {
    public GameObject rewardPlatform;
        public GameObject cables;
        public Material litUpMat;

        public void MakeSuprise()
        {
            foreach (Transform t in cables.transform)
            {
                t.GetComponent<Renderer>().material = litUpMat;
            }
      StartCoroutine(WaitForBoss());
        }

        private IEnumerator WaitForBoss()
        {
      yield return new WaitForSeconds(3);
            transform.DOMove(transform.position - new Vector3(0, 8, 0), 8);
            yield return new WaitForSeconds(9);
      Destroy(gameObject);
      Instantiate(rewardPlatform);
        }
    }
}