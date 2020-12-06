using System.Collections.Generic;
using Managers.Platforms;
using UnityEngine;

namespace Managers.Generation
{
    public class MainGenerator : MonoBehaviour
    {
        //[SerializeField]
        //private Vector3 _startLocation = Vector3.zero;
        //[SerializeField]
        //private Vector3 _endLocation = new Vector3(0, 0, 200);
        [SerializeField] private Transform _generatedPathContainer;
        public Transform start;
        public Transform end;

        public MainPlatformManager[] mainPlatforms;
        public Transform[] smallPlatforms;

        private readonly List<Platform> _platforms = new List<Platform>();

        //private void Start()
        //{
        //  _startLocation = start.position;
        //  _endLocation = end.position;
        //  var p1 = new PathGenerator(5, 1, 5, 8);
        //  p1.Path.Add(new Platform(_startLocation, 1, 0));
        //  p1.GeneratePath(_startLocation, _endLocation);
        //  _platforms.AddRange(p1.GetPlatforms());
        //  SpawnSmallPlatforms(_platforms);
        //  Debug.Log(_platforms.Count);
        //}

        public List<Platform> Generate(Vector3 startLocation, Vector3 endLocation, List<Platform> existingPlatforms, PlayerAttributes attributes, Vector2 pathSize, Platform[] availablePlatforms)
        {
            //Debug.Log(endLocation);
            var p1 = new PathGenerator(attributes.JumpForce, attributes.JumpCount, attributes.Speed, 10, pathSize,availablePlatforms);
            
            p1.AllPlatforms = existingPlatforms;
            p1.Path.Add(new Platform(startLocation, 1, 0.3f, -1));
            p1.AllPlatforms.Add(new Platform(startLocation, 1, 0.3f, -1));
            p1.GeneratePath(startLocation, endLocation);
            _platforms.AddRange(p1.GetPlatforms());
            SpawnSmallPlatforms(_platforms);
            return p1.AllPlatforms;
            //Debug.Log(_platforms.Count);
        }

        private void SpawnSmallPlatforms(IEnumerable<Platform> plats)
        {
            foreach (var plat in plats)
            {

                if (plat.Index >=0) Instantiate(smallPlatforms[plat.Index], plat.Position, Quaternion.identity, _generatedPathContainer);
            }
        }
        public struct PlayerAttributes
        {
            public float Speed;
            public int JumpCount;
            public float JumpForce;
        }
    }
}