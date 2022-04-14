using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings")]
public class Settings : ScriptableObject
{
    public float speed;
    public float sensitivity;
    public bool isPlaying;
    public bool GameOver = true;
    public bool GameStarted;
    public int level;
    public int points;
}