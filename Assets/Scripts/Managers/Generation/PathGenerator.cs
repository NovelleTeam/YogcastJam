using System.Collections.Generic;
using UnityEngine;

namespace Managers.Generation
{
    public class PathGenerator
    {
        private float _jumpBoost;
        private int _jumpCount;
        private float _speed;
        private float _gForce;

        private float _margin = 0.2f;
        private float _mindist = 5;

        private float _maxHeightSingle;
        private float _maxDistSingle;
        private float _maxHeightTotal;
        private float _maxDistTotal;
        private Bounds _jumpRange;

        private Vector2 _pathSize = new Vector2(20, 20);

        public List<Platform> Path = new List<Platform>();

        public PathGenerator(float jumpBoost, int jumpCount, float speed, float gForce)
        {
            _jumpBoost = jumpBoost;
            _jumpCount = jumpCount;
            _speed = speed;
            _gForce = gForce;
            _maxHeightSingle = _jumpBoost * _jumpBoost / _gForce;
            _maxDistSingle = 2 * _jumpBoost / _gForce * _speed;
            _maxHeightTotal = _maxHeightSingle * _jumpCount;
            _maxDistTotal = _maxDistSingle * _jumpCount;
            Debug.Log(_maxDistTotal);
            Debug.Log(_maxHeightTotal);

            _jumpRange = new Bounds(new Vector3(0, 0, 0), new Vector3(_maxDistTotal, _maxDistTotal, _maxHeightTotal));
        }

        public void GeneratePath(Vector3 pfrom, Vector3 to) // Generates a path from one place to another
        {
            var infCheck = 0;
            var direction = to - pfrom;

            var chanceModifier = 1f;
            var exitCheck = false;
            var progress = 1f;

            while (true)
            {
                var currentProgress = Random.Range(0f, 1f) / progress;
                //progress *= 0.99f;
                //float currentProgress = Mathf.Pow(Random.Range(0.0f, 1.0f), 2);
                //curprog *= (1 - Mathf.Abs(1 - currentProgress / (currentProgress + 0.001f)));
                var platformPos = direction * currentProgress;
                platformPos.y += _pathSize.y * Random.Range(-1.0f, 1.0f);
                var normal = Vector3.Cross(direction, new Vector3(0, 1, 0));
                normal = normal.normalized;
                platformPos += normal * +_pathSize.x * Random.Range(-1.0f, 1.0f);
                platformPos += pfrom;

                var chance = 1f;
                var canJump = 0f;

                var newPlatform = new Platform(platformPos, 1, 0); // Replace with one that randomizes platform

                foreach (var platform in Path)
                    if (CanJumpTo(platform.Position - platformPos))
                        canJump = 1;

                if (canJump == 0)
                    Debug.Log("boop");
                
                chance *= canJump;

                chance *= newPlatform.CheckProx(Path, _mindist);

                if (chance > Random.Range(0.0f, 1.0f))
                {
                    Path.Add(newPlatform);
                    if (CanJumpTo(to - platformPos)) exitCheck = true;
                    //break;
                    if (currentProgress > progress) progress = currentProgress;
                    chanceModifier = 1;
                }
                else
                {
                    chanceModifier *= 1.1f;
                }

                //Debug.Log(infcheck);
                infCheck++;
                if (infCheck > 10000)
                {
                    Debug.Log("timed out");
                    break;
                }

                if (exitCheck) break;
            }
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            return Path;
        }

        private void GeneratePlatform()
        {
        }

        private bool CanJumpTo(Vector3 relative)
        {
            var total = Mathf.Abs(relative.y) / _maxHeightTotal +
                         Mathf.Sqrt(relative.x * relative.x + relative.z * relative.z) / _maxDistTotal;
            return total + _margin < 1;
        }
    }

    public class Platform
    {
        public Vector3 Position;
        public int Radius;
        public int Index;

        public Platform(Vector3 position, int radius, int index)
        {
            Position = position;
            Radius = radius;
            Index = index;
        }

        public float CheckProx(IEnumerable<Platform> list, float mindist)
        {
            var ans = 1f;
            foreach (var plat in list)
            {
                Vector2 to = plat.Position - Position;
                var dist = to.magnitude;
                if (dist < Radius + mindist + plat.Radius) ans = 0;
            }

            return ans;
        }
    }
}