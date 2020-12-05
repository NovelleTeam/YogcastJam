using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class MiniPlatform : MonoBehaviour
{
    private bool wasStepedOn = false;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && !wasStepedOn)
        {
            StartCoroutine(waitForDrop());
            GetComponent<Renderer>().material = other.gameObject.GetComponent<PlayerManager>().miniPlatformLitUp;
            wasStepedOn = true;
        }
        else if (other.gameObject.tag == "DethFloor")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator waitForDrop()
    {
        yield return new WaitForSeconds(2);
        gameObject.AddComponent<Rigidbody>();
    }
}
