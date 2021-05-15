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
			dialogue.transform.GetChild(0).gameObject.SetActive(true);
			UIObject.GetComponent<UIManager>().inTrigger = dialogue.GetComponent<VIDE_Assign>();
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		dialogue.transform.GetChild(0).gameObject.SetActive(false);
		UIObject.GetComponent<UIManager>().inTrigger = null;

	}
	void Update()
    {
		UIObject = GameObject.Find("UIManager");

	}
}