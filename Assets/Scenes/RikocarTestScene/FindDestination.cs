using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindDestination : MonoBehaviour
{
    private void Start()
    {
		var destination = GameObject.Find("Destination").transform.position;

		GetComponent<interactiveObject>().SetDestination(destination);
    }
}
