using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName = "Score")]
public class PlayerScore : ScriptableObject
{
    public static int playerScore = 0; 
}
