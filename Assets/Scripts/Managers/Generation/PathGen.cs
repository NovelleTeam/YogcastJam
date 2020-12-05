using System.Collections.Generic;
using UnityEngine;

public class PathGen
{
    float _jumpboost;
    int _jumpcount;
    float _speed;
    float _gforce;

    float _margin = 0.2f;
    float mindist = 5;

    float _maxheightsingle;
    float _maxdistsingle;
    float _maxheighttotal;
    float _maxdisttotal;
    Bounds _jumprange;

    Vector2 _pathsize = new Vector2(20, 20);

    public List<Platform> path = new List<Platform>();

    public PathGen(float jumpboost, int jumpcount, float speed, float gforce)
    {
        _jumpboost = jumpboost;
        _jumpcount = jumpcount;
        _speed = speed;
        _gforce = gforce;
        _maxheightsingle = _jumpboost * _jumpboost  / _gforce;
        _maxdistsingle = 2 * _jumpboost / _gforce * _speed;
        _maxheighttotal = _maxheightsingle * _jumpcount;
        _maxdisttotal = _maxdistsingle * _jumpcount;
        Debug.Log(_maxdisttotal);
        Debug.Log(_maxheighttotal);

        _jumprange = new Bounds(new Vector3(0, 0, 0), new Vector3(_maxdisttotal, _maxdisttotal, _maxheighttotal));
    }

    public void genPath(Vector3 pfrom, Vector3 to) // Generates a path from one place to another
    {
        int infcheck = 0;
        Vector3 dirvec = to - pfrom;

        float chancemod = 1f;
        bool exitcheck = false;
        float progress = 1f;
        while (true)
        {
            float curprog = Random.Range(0f, 1f)/progress;
            //progress *= 0.99f;
            //float curprog = Mathf.Pow( Random.Range(0.0f, 1.0f),2);
            //curprog *= (1 - Mathf.Abs(1 - curprog /(progress+0.001f)));
            Vector3 platpos = dirvec * curprog;
            platpos.y += _pathsize.y * Random.Range(-1.0f, 1.0f);
            Vector3 normal = Vector3.Cross(dirvec, new Vector3(0, 1, 0));
            normal = normal.normalized;
            platpos += normal * +_pathsize.x* Random.Range(-1.0f, 1.0f);
            platpos += pfrom;

            float chance = 1f;
            float canjump = 0;

            Platform newplat = new Platform(platpos,1,0); // Replace with one that randomizes platform

            foreach (Platform plat in path)
            {
                if (canJumpto(plat.position - platpos))
                {
                    canjump = 1;
                }
            }
            if (canjump == 0) Debug.Log("boop");
            chance *= canjump;
            
            chance *= newplat.checkProx(path, mindist);
            
            if (chance > Random.Range(0.0f, 1.0f))
            {
                path.Add(newplat);
                if (canJumpto(to - platpos))
                {
                    exitcheck = true;
                    
                }
                //break;
                if (curprog > progress) progress = curprog;
                chancemod = 1;
            }
            else
            {
                chancemod *= 1.1f;
            }

            //Debug.Log(infcheck);
            infcheck++;
            if (infcheck > 10000)
            {
                Debug.Log("timed out");
                break;
            }
            if (exitcheck) break;
        }
    }

    public List<Platform> getPlats()
    {
        return path;
    }

    void genplatform()
    {

    }

    bool canJumpto(Vector3 relative)
    {
        float _total = Mathf.Abs( relative.y) / _maxheighttotal + Mathf.Sqrt(relative.x * relative.x + relative.z * relative.z) / _maxdisttotal;
        return _total + _margin < 1;
    }


}
public class Platform
{
    public Vector3 position;
    public int radius;
    public int index;

    public Platform(Vector3 position, int radius, int index)
    {
        this.position = position;
        this.radius = radius;
        this.index = index;
    }

    public float checkProx(List<Platform> list, float mindist)
    {
        float ans = 1f;
        foreach (Platform plat in list)
        {
            Vector2 to = plat.position - position;
            float dist = to.magnitude;
            if (dist < radius + mindist + plat.radius)
            {
                ans = 0;
            }
            
        }

        return ans;
    }
}