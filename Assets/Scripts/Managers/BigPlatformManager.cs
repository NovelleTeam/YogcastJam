using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Managers
{
    public class BigPlatformManager : MonoBehaviour
    {
        public List<Renderer> Cables;
        public Material litUpMat;

        public void MakeSuprise()
        {
            foreach (Renderer cable in Cables)
            {
                cable.material = litUpMat;
            }
        }

        IEnumerator waitForBoss()
        {
            DOTween.Init();
            transform.DOMove(transform.position - new Vector3(0, 10, 0), 10);
            yield return new WaitForSeconds(10);
        }
    }
}