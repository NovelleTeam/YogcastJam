using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGen : MonoBehaviour
{
    public Transform[] smallPlatforms;

    List<Platform> plats = new List<Platform>();
    
    // Start is called before the first frame update
    void Start()
    {
        
        PathGen p1 = new PathGen(20, 1, 5, 10);
        p1.path.Add(new Platform(new Vector3(0, 0, 0), 1, 0));
        p1.genPath(new Vector3(0, 0, 0), new Vector3(0, 0, 100));
        plats.AddRange(p1.getPlats());
        spawnSPlats(plats);
        Debug.Log(plats.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void spawnSPlats(List<Platform> plats)
    {
        foreach (Platform plat in plats)
        {
            Instantiate(smallPlatforms[plat.index], plat.position, Quaternion.identity);
        }
    }
}
