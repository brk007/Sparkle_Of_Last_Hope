using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealingPlace : MonoBehaviour
{
	public PlayerCombat PlayerCombat;
	public GameObject healthbar;

	void Update()
    {
		healthbar = GameObject.Find("Canvas/Healthbar");
		PlayerCombat = GameObject.Find("Player").GetComponent("PlayerCombat") as PlayerCombat;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name.Equals("Player") && PlayerCombat.currentHealth < PlayerCombat.maxHealth)
			StartCoroutine("Heal");
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name.Equals("Player"))
			StopCoroutine("Heal");
	}

	IEnumerator Heal()
	{
		for (float currentHealth = PlayerCombat.currentHealth; currentHealth <= PlayerCombat.maxHealth; currentHealth += 10 * Time.deltaTime)
		{
			PlayerCombat.currentHealth = currentHealth;
			PlayerCombat.currentStamina += 10 * Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		PlayerCombat.currentHealth = PlayerCombat.maxHealth;
	}

}
