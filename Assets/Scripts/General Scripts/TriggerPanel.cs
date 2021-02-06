using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPanel : MonoBehaviour
{
    public GameObject SkillPanel;

    public void openPanel()
    {
        PlayerCombat.isMouseInputEnabled = false;
        Animator animator = SkillPanel.GetComponent<Animator>();
        animator.SetBool("IsOpen", true);
    }
    public void closePanel()
    {        
        PlayerCombat.isMouseInputEnabled = true;
        Animator animator = SkillPanel.GetComponent<Animator>();
        animator.SetBool("IsOpen", false);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }
    void Pause()
    {
        Time.timeScale = 0f;
    }
}
