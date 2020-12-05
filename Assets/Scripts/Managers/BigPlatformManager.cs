using System.Collections.Generic;
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
    }
}