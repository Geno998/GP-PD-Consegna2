using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StageValues", menuName = "scriptableObjects/Stage/Values")]
public class StageValuesScriptable : ScriptableObject
{
    public int enemy1ToSpawn;
    public int enemy2ToSpawn;
    public int enemy3ToSpawn;
}
