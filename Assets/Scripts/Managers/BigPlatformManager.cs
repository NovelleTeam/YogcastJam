using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Managers
{
    public class BigPlatformManager : MonoBehaviour
    {
        public List<Renderer> cables;
        public Material litUpMat;

        public void MakeSuprise()
        {
            foreach (var cable in cables) cable.material = litUpMat;
        }

        private IEnumerator WaitForBoss()
        {
            DOTween.Init();
            transform.DOMove(transform.position - new Vector3(0, 10, 0), 10);
            yield return new WaitForSeconds(10);
        }
    }
}