using System.Collections.Generic;
using UnityEngine;

namespace Managers.Generation
{
    public class MainGenerator : MonoBehaviour
    {
        public Transform[] smallPlatforms;

        private readonly List<Platform> _platforms = new List<Platform>();
    
        private void Start()
        {
            var p1 = new PathGenerator(10, 1, 10, 10);
            p1.Path.Add(new Platform(new Vector3(0, 0, 0), 1, 0));
            p1.GeneratePath(new Vector3(0, 0, 0), new Vector3(0, 0, 100));
            _platforms.AddRange(p1.GetPlatforms());
            SpawnSmallPlatforms(_platforms);
            Debug.Log(_platforms.Count);
        }
        
        public void SpawnSmallPlatforms(IEnumerable<Platform> plats)
        {
            foreach (var plat in plats)
            {
                Instantiate(smallPlatforms[plat.Index], plat.Position, Quaternion.identity);
            }
        }
    }
}
