using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GrappleBall.States;

public class GameManager : Singleton<GameManager>
{
    private GameStates currentGameState;

    public GameStates GameStates
    {
        get => currentGameState;
        set
        {
            if (currentGameState == value) return;
            currentGameState = value;
            OnGameStateChanged?.Invoke();
        }
    }

    public event Action OnGameStateChanged;
}