using System;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
	[SerializeField]
	private Transform playerRoot;

	private Transform _respawn;

	[SerializeField]
	private int startingLives;
	[SerializeField]
	private bool capMaxLives;
	[SerializeField]
	private int maxLifeCap;
	public int StartingLives { get { return startingLives; } }
	public int CurrentLives { get; private set; }

	public Action onLoseLife;
	public Action onGameOver;

	private void Awake()
	{
		CurrentLives = StartingLives;
	}

	public void GainLife()
	{
		++CurrentLives;
		if (capMaxLives && CurrentLives > maxLifeCap)
		{
			CurrentLives = maxLifeCap;
		}
	}

	public void LoseLife()
	{
		--CurrentLives;

		onLoseLife?.Invoke();

		if (CurrentLives <= 0)
		{
			CurrentLives = 0;
			onGameOver?.Invoke();
		}
	}

	public void SetNewRespawn(Transform respawnTransform)
	{
		_respawn = respawnTransform;
	}

	public void RespawnPlayer()
	{
		playerRoot.transform.position = _respawn.position;
		playerRoot.transform.rotation = _respawn.rotation;
	}
}
