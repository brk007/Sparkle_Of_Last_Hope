using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDialogueArea : MonoBehaviour
{
	public DialogueTrigger dialogueTrigger;
	public DialogueManager dialogueManager;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name.Equals("Player"))
		{
			dialogueTrigger.TriggerDialogue();	
		}
	}
	void OnTriggerExit2D(Collider2D col)
    {
		dialogueManager.EndDialogue();

	}
}
