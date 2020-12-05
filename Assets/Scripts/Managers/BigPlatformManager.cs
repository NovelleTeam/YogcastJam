using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Managers
{
    public class BigPlatformManager : MonoBehaviour
    {
        public GameObject cables;
        public Material litUpMat;

        public void MakeSuprise()
        {
            foreach (Transform t in cables.transform)
            {
                t.GetComponent<Renderer>().material = litUpMat;
            }
        }

        private IEnumerator WaitForBoss()
        {
            DOTween.Init();
            transform.DOMove(transform.position - new Vector3(0, 10, 0), 10);
            yield return new WaitForSeconds(10);
        }
    }
}