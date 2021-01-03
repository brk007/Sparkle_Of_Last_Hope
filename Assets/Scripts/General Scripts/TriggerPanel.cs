using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPanel : MonoBehaviour
{
    public GameObject SkillPanel;

    public void openPanel()
    {
        Animator animator = SkillPanel.GetComponent<Animator>();
        animator.SetBool("IsOpen", true);
    }
    public void closePanel()
    {
        Animator animator = SkillPanel.GetComponent<Animator>();
        animator.SetBool("IsOpen", false);
    }
        
}
