using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currentDay;
    public int reputation;
    public int relationship;
    public DayState dayState;
    public GameState gameState;

    public GameData()
    {
        this.currentDay = 1;
        this.reputation = 50;
        this.relationship = 50;
        this.dayState = DayState.Shelving;
        this.gameState = GameState.MainMenu;
    }
}
