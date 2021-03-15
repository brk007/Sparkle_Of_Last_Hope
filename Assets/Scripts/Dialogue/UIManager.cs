using System.Collections;
using System.Collections.Generic;
using VIDE_Data;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator animator;
    public GameObject container_NPC;
    public GameObject container_PLAYER;
    public Text text_NPC;
    public Text Name;
    public Text[] text_Choices;
    public VIDE_Assign inTrigger;

    // Start is called before the first frame update
    void Start()
    {
        VD.LoadDialogues();
        container_NPC.SetActive(false);
        container_PLAYER.SetActive(false);
        inTrigger = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(!VD.isActive)
            {
                Begin();
            }
            else
            {
                VD.Next();
            }
        }
    }
    void Begin()
    {
        if (inTrigger != null)
        {
            PlayerCombat.isMouseInputEnabled = false;
            animator.SetBool("IsOpen", true);

            VD.OnNodeChange += UpdateUI;
            VD.OnEnd += End;
            VD.BeginDialogue(inTrigger.GetComponent<VIDE_Assign>());
            Name.text = VD.assigned.alias;
        }
    }
    void UpdateUI(VD.NodeData data)
    {
        container_PLAYER.SetActive(false);
        container_NPC.SetActive(false);
        if (data.isPlayer)
        {
            container_NPC.SetActive(true);
            container_PLAYER.SetActive(true);

            for(int i=0;i < text_Choices.Length; i++)
            {
                if(i < data.comments.Length)
                {
                    text_Choices[i].transform.parent.gameObject.SetActive(true);
                    text_Choices[i].text = data.comments[i];
                }
                else
                {
                    text_Choices[i].transform.parent.gameObject.SetActive(false);
                }
            }
            text_Choices[0].transform.parent.GetComponent<Button>().Select();
        }
        else
        {
            container_NPC.SetActive(true);
            text_NPC.text = data.comments[data.commentIndex];
        }
    }
    void End(VD.NodeData data)
    {
        PlayerCombat.isMouseInputEnabled = false;
        animator.SetBool("IsOpen", false);

        container_NPC.SetActive(false);
        container_PLAYER.SetActive(false);
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
    }
    void OnDisable()
    {
        if(container_NPC != null)
        {
            End(null);
        }
    }
    public void SetPlayerChoice(int choice)
    {
        VD.nodeData.commentIndex = choice;
        if (Input.GetMouseButtonUp(0))
        {
            VD.Next();
        }
    }
}
