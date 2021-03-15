using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

   public GameObject player;
    public GameObject dialogueUI;

    Text npcName;
    Text DialogueText;
    Text Response;
    Animator animator;

    int CurDialogue = 0;
    int toWhere = 0;
    
    void Start()
    {
        animator.SetBool("IsOpen", false);
    }

    void NPCDialogue(int id, string name, string dialogue)
    {
        this.DialogueText.text = dialogue;
        id = CurDialogue;
        name = npcName.text;
        
    }
    void PlayerDialogue(int id, string response, int where)
    {   if(id == CurDialogue)
        {
            Response.text = response;
            toWhere = where;
        }
        
    }

    void Manager()
    {
        
    }

    public void StartConversation()
    {
        PlayerCombat.isMouseInputEnabled = false;
        animator.SetBool("IsOpen", true);
    }

    public void EndDialogue()
    {
        PlayerCombat.isMouseInputEnabled = true;
        animator.SetBool("IsOpen", false);
    }
   
}