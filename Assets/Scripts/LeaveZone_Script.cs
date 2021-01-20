using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZone_Script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LevelGenerator_Script.staticLevelGenerator.AddLevelBlock();
            LevelGenerator_Script.staticLevelGenerator.RemoveOldestLevelBlock();
        }
        
    }
}
