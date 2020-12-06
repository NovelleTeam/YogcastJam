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

        private float _margin = 0.3f;
        private float _mindist = 1f;

        private float _maxHeightSingle;
        private float _maxDistSingle;
        private float _maxHeightTotal;
        private float _maxDistTotal;
        private Bounds _jumpRange;

        private Vector2 _pathSize = new Vector2(10, 10);

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

            _maxHeightTotal = 2f;
            _maxDistTotal = 8f;


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
            var lastPlatform = Path[0];
            lastPlatform = new Platform(new Vector3(0, 0, 0), 0, -1);

            while (true)
            {
                var currentProgress = Random.Range(1f - progress, 1f); // * progress;
                //currentProgress = 0.9f;
                //progress *= 0.99f;
                //float currentProgress = Mathf.Pow(Random.Range(0.0f, 1.0f), 2);
                //curprog *= (1 - Mathf.Abs(1 - currentProgress / (currentProgress + 0.001f)));

                //Debug.Log(currentProgress);

                var platformPos = direction * currentProgress;
                platformPos.y += _pathSize.y * Random.Range(-1.0f, 1.0f);
                var normal = Vector3.Cross(direction, new Vector3(0, 1, 0));
                normal = normal.normalized;
                platformPos += normal * +_pathSize.x * Random.Range(-1.0f, 1.0f);
                platformPos += pfrom;

                //Debug.Log(platformPos);

                var chance = 1f;
                var canJump = 0f;

                var newPlatform = new Platform(platformPos, 1, 0); // Replace with one that randomizes platform


                foreach (var platform in Path)
                {
                    if (newPlatform.CanJumpTo(platform, _maxHeightTotal, _maxDistTotal, _margin))
                        canJump = 1;
                    if (newPlatform.distanceTo(platform).magnitude < newPlatform.distanceTo(lastPlatform).magnitude)
                    {
                        lastPlatform = platform;
                    }
                }

                if (canJump == 0)
                {
                    Debug.Log("adjusting");
                    var closevec = lastPlatform.Position;
                    var closeDirection = platformPos - closevec;
                    closeDirection = closeDirection.normalized;
                    Debug.Log(closeDirection);
                    closeDirection.x *= _maxDistTotal * Random.Range(0.7f, 1f);
                    closeDirection.y *= _maxHeightTotal * Random.Range(0.7f, 1f);
                    closeDirection.z *= _maxDistTotal * Random.Range(0.7f, 1f);
                    Debug.Log(closeDirection);
                    platformPos = closevec + closeDirection;
                    Debug.Log(platformPos);
                    newPlatform.Position = platformPos;
                    canJump = 1;
                }

                chance *= canJump;
                //if (newPlatform.CheckProx(Path, _mindist) == 0) Debug.Log("zoop");
                chance *= newPlatform.CheckProx(Path, _mindist);

                if (chance > Random.Range(0.0f, 1f))
                {
                    Path.Add(newPlatform);
                    //Debug.Log("Added");
                    if (newPlatform.CanJumpTo(new Platform(to, 0, -1), _maxHeightTotal, _maxDistTotal, _margin))
                    {
                        Debug.Log("Can exit");
                        exitCheck = true;
                    }

                    //*
                    Debug.Log((platformPos - to).magnitude);
                    if ((platformPos - to).magnitude < _maxDistTotal)
                    {
                        Debug.Log("Can exit");
                        exitCheck = true;
                    }

                    //*/
                    //break;
                    if (currentProgress + _maxDistTotal / direction.magnitude > progress)
                    {
                        //progress /= 1.1f;
                        lastPlatform = newPlatform;
                    }

                    chanceModifier = 1;
                }
                else
                {
                    chanceModifier *= 1.1f;
                    //infCheck--;
                }

                //Debug.Log(infcheck);
                infCheck++;
                if (infCheck > 1000)
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
                var to = plat.Position - Position;
                to.y = to.y * 3;
                Debug.Log(to);
                var dist = to.magnitude;
                Debug.Log(dist + "; " + (Radius + mindist + plat.Radius));
                if (dist < Radius + mindist + plat.Radius) ans = 0;
            }

            return ans;
        }

        public bool CanJumpTo(Platform target, float maxHeight, float maxDist, float margin)
        {
            var relative = target.Position - Position;
            var total = (Mathf.Clamp(relative.y, 0, Mathf.Infinity) + 1) / maxHeight +
                        Mathf.Sqrt(relative.x * relative.x + relative.z * relative.z) / maxDist;
            //if (target.Index == -1) Debug.Log(total);
            if (total < 1) Debug.Log("yay");
            return total < 1;
        }

        public Vector3 distanceTo(Platform target)
        {
            return target.Position - Position;
        }
    }
}