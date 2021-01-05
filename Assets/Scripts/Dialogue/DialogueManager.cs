﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public float letterPause;
    public Animator animator;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        InvokeRepeating("NextSentence", 0, 0.1f);
    }
    void NextSentence() 
    {
        if (Input.GetKey("e"))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        PlayerCombat.isMouseInputEnabled = false;
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        } 
            DisplayNextSentence();
            
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void EndDialogue()
    {
        PlayerCombat.isMouseInputEnabled = true;
        animator.SetBool("IsOpen", false);
    }

}
