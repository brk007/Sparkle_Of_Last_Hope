using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArea : MonoBehaviour
{
	public VIDE_Assign component;
	public UIManager UIManager;
	public GameObject dialogue;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name.Equals("Player"))
		{
			UIManager.inTrigger = dialogue.GetComponent<VIDE_Assign>();
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		UIManager.inTrigger = null;

	}
}