using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArea : MonoBehaviour
{
	public VIDE_Assign component;
	public GameObject UIObject;
	public GameObject dialogue;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name.Equals("Player"))
		{
			UIObject.GetComponent<UIManager>().inTrigger = dialogue.GetComponent<VIDE_Assign>();
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		UIObject.GetComponent<UIManager>().inTrigger = null;

	}
	void Update()
    {
		UIObject = GameObject.Find("UIManager");

	}
}