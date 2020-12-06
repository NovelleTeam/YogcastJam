using System.Collections.Generic;
using UnityEngine;

namespace Managers.Generation
{
    public class MainGenerator : MonoBehaviour
    {
    public Vector3 startLocation;
    public Vector3 endLocation;
        public Transform[] smallPlatforms;

        private readonly List<Platform> _platforms = new List<Platform>();

        private void Start()
        {
            var p1 = new PathGenerator(0.01f, 1, 0.01f, 9.81f);
            p1.Path.Add(new Platform(startLocation, 1, 0));
            p1.GeneratePath(startLocation, endLocation);
            _platforms.AddRange(p1.GetPlatforms());
            SpawnSmallPlatforms(_platforms);
            Debug.Log(_platforms.Count);
        }

        public void SpawnSmallPlatforms(IEnumerable<Platform> plats)
        {
            foreach (var plat in plats) Instantiate(smallPlatforms[plat.Index], plat.Position, Quaternion.identity);
        }
    }
}