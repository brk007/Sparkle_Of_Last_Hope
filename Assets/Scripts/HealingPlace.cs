using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealingPlace : MonoBehaviour
{
	public PlayerCombat playerCombat;
	public HealthBar healthBar;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name.Equals("Player") && playerCombat.currentHealth < playerCombat.maxHealth)
			StartCoroutine("Heal");
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name.Equals("Player"))
			StopCoroutine("Heal");
	}

	IEnumerator Heal()
	{
		for (int currentHealth = playerCombat.currentHealth; currentHealth <= playerCombat.maxHealth; currentHealth += 5)
		{
			playerCombat.currentHealth = currentHealth;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		playerCombat.currentHealth = playerCombat.maxHealth;
	}

}
