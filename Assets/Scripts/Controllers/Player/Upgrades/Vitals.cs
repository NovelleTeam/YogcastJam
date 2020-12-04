using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitals : MonoBehaviour
{
	[SerializeField]
	private float maxHealth;
	public float CurHealth { get; private set; }
	public bool IsDead => CurHealth <= 0;

	public event Action onDeath;
	public event Action onTakeDamage;

	public void TakeDamage(int health)
	{
		if (health < 0)
		{
			throw new System.Exception("Vitals.TakeDamage used to heal! Use Vitals.HealDamage instead.");
		}

		CurHealth -= health;

		onTakeDamage?.Invoke();

		if (IsDead)
		{
			CurHealth = 0;
			onDeath?.Invoke();
		}
	}

	public void HealDamage(int health)
	{
		if (health < 0)
		{
			throw new System.Exception("Vitals.HealDamage used to damage! Use Vitals.TakeDamage instead.");
		}

		CurHealth += health;
	}
}
