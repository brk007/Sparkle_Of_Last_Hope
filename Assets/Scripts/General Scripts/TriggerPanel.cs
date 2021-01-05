﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPanel : MonoBehaviour
{
    public GameObject SkillPanel;

    public void openPanel()
    {
        Animator animator = SkillPanel.GetComponent<Animator>();
        animator.SetBool("IsOpen", true);
        PlayerCombat.isMouseInputEnabled = false;
    }
    public void closePanel()
    {
        Animator animator = SkillPanel.GetComponent<Animator>();
        animator.SetBool("IsOpen", false);
        PlayerCombat.isMouseInputEnabled = true;
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
