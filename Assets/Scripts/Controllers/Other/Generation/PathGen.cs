using UnityEngine;

public class PathGen : MonoBehaviour
{
    float _jumpboost;
    int _jumpcount;
    float _speed;
    float _gforce;

    float _margin = 0.2f;

    float _maxheightsingle;
    float _maxdistsingle;
    float _maxheighttotal;
    float _maxdisttotal;
    Bounds _jumprange;

    Platform[] path;

    public PathGen(float jumpboost, int jumpcount, float speed, float gforce)
    {
        _jumpboost = jumpboost;
        _jumpcount = jumpcount;
        _speed = speed;
        _gforce = gforce;
        _maxheightsingle = _jumpboost * _jumpboost / 2 / _gforce;
        _maxdistsingle = 2 * _jumpboost / _gforce * _speed;
        _maxheighttotal = _maxheightsingle * _jumpcount;
        _maxdisttotal = _maxdistsingle * _jumpcount;

        _jumprange = new Bounds(new Vector3(0, 0, 0), new Vector3(_maxdisttotal, _maxdisttotal, _maxheighttotal));
    }

    public void genPath(Vector3 from, Vector3 to) // Generates a path from one place to another
    {

    }

    void genplatform()
    {

    }

    bool canJumpto(Vector3 relative)
    {
        float _total = relative.y / _maxheighttotal + Mathf.Sqrt(relative.x * relative.x + relative.y * relative.y) / _maxdisttotal;
        return _total + _margin < 1;
    }


}
class Platform
{
    public Vector3 location;
    public int radius;
    public bool checkProx(Platform[] list, float mindist)
    {
        bool ans = true;
        foreach (Platform plat in list)
        {
            Vector2 to = plat.location - location;
            float dist = to.magnitude;
            if (dist < radius + mindist + plat.radius)
            {
                ans = false;
            }
        }

        return ans;
    }
}