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
        [SerializeField] private MainPlatformManager platmanpref;
        public Transform start;
        public Transform end;
        private int platformCount = 0;

        public List<MainPlatformManager> mainPlatforms= new List<MainPlatformManager>();
        public Transform[] smallPlatforms;
        public List<BigPlatformManager> platformPool = new List<BigPlatformManager>();
        public List<BigPlatformManager> randomizedPlatformPool = new List<BigPlatformManager>();

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


        public void Start()
        {
            for (int i = 0; i < platformPool.Count; i++)
            {
                int rand = Random.Range(0, platformPool.Count);
                randomizedPlatformPool.Add(platformPool[rand]);
                platformPool.RemoveAt(rand);
            }

            CreatePlatform(new Vector3(0, 2, -21));
        }

        public void NextPlatform(Vector3 endpoint)
        {
            Vector3 nextpoint = endpoint + new Vector3(Random.Range(-20f, 20f), Random.Range(-10f, 5f), Random.Range(40f, 70f));
            CreatePlatform(nextpoint);
            Controllers.Player.PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controllers.Player.PlayerMovement>();
            Generate(endpoint, nextpoint+new Vector3(0,3,0), new List<Platform>(), new PlayerAttributes() { JumpCount = player.maxJumps, JumpForce = player.jumpForce, Speed = player.moveSpeed }, new Vector2(10, 10), new Platform[] { new Platform(new Vector3(0, 0, 0), 1, 0.3f, 0), new Platform(new Vector3(0, 0, 0), 1, 0.3f, 1), new Platform(new Vector3(0, 0, 0), 1, 0.3f, 2), new Platform(new Vector3(0, 0, 0), 1, 0.3f, 3), new Platform(new Vector3(0, 0, 0), 1, 0.3f, 4), new Platform(new Vector3(0, 0, 0), 1, 0.3f, 5), new Platform(new Vector3(0, 0, 0), 1, 0.3f, 6), new Platform(new Vector3(0, 0, 0), 1, 0.3f, 7), new Platform(new Vector3(0, 0, 0), 1, 0.3f, 8) });
        }

        public void CreatePlatform(Vector3 startpoint)
        {
            //MainPlatformManager newplatform = Instantiate(randomizedPlatformPool[platformCount], startpoint - randomizedPlatformPool[platformCount].GetPlatformBegin(), Quaternion.identity);
            MainPlatformManager newplatform = Instantiate(platmanpref, startpoint - platmanpref.GetPlatformBegin(), Quaternion.identity);
            mainPlatforms.Add( newplatform);
            newplatform.gameObject.layer = 9;
            newplatform._generatedPathContainer = GameObject.Find("GeneratedPathContainer").transform;
            platformCount++;

        }

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