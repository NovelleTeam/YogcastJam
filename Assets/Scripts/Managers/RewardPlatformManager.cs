using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RewardPlatformManager : MonoBehaviour
{
  [SerializeField]
  private Transform _chestSpawn;
  [SerializeField]
  private GameObject _chest;

  private void Start()
  {
    transform.DOMoveY(0.0f, 2.0f).SetEase(Ease.Linear);
  }

  public void SpawnChest()
  {
    Instantiate(_chest, _chestSpawn.position, _chestSpawn.rotation);
  }
}
