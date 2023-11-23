using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GrappleBall;

public class GameManager : Singleton<GameManager>
{
    #region Properties
    private GameStates currentGameState;
    public GameStates GameState
    {
        get => currentGameState;
        set
        {
            if (currentGameState == value) return;
            currentGameState = value;
            OnGameStateChanged?.Invoke();
        }
    }
    #endregion

    #region Cleanup
    static public bool cleanedUp = false;

    private void OnApplicationQuit()
    {
        OnApplicationCleanup?.Invoke();
        cleanedUp = true;
    }
    #endregion

    public event Action OnApplicationCleanup;
    public event Action OnGameStateChanged;
}