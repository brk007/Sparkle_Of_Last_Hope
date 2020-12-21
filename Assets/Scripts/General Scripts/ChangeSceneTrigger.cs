using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTrigger : MonoBehaviour
{
    public int prevNumber;
    public int mapNumber;
    public LevelLoader levelLoader;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            levelLoader.LoadNextLevel(mapNumber);
        }
    }
}
