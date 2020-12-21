using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 

public class PlayerData
{
 
    ///public int level;
    public int currentHealth;
    public int attackDamage;
    public float attackRange;

    public float[] position;

    public PlayerData (PlayerCombat player)
    {
        //level = player.level;
        currentHealth = player.currentHealth;
        attackDamage = player.attackDamage;
        attackRange = player.attackRange;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
