using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPlatform : MonoBehaviour
{
    public RewardPlatformManager rewardPlatformManager;
    private bool _wasSteppedOn;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !_wasSteppedOn)
        {
            rewardPlatformManager.SpawnChest();
            _wasSteppedOn = true;
        }
    }
}