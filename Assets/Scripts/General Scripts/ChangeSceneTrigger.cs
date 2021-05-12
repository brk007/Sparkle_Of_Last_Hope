using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTrigger : MonoBehaviour
{
    public int prevNumber;
    public int mapNumber;
    public GameObject Changer;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            PlayerCombat.isMouseInputEnabled = true;
            Changer.GetComponent<LevelLoader>().LoadNextLevel(mapNumber);
        }
    }
    public void Update()
    {
        Changer = GameObject.Find("LevelLoader");

    }
}
