using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLevel : MonoBehaviour
{
    public PlayerCombat player;
    int level;
    public Text myText;

    void Start()
    {
        level = 1;
        myText.text = level.ToString();
    }

    void Update()
    {
        level = player.level;
        myText.text = level.ToString();
    }
}
