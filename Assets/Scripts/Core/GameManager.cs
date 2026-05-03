using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GameStart,
        GameInProgress,
        GameEnd
    };

    GameState CurrentGameState;

    [SerializeField] private Building playerBase;
    [SerializeField] private Building enemyBase;

    public event Action<GameState> GameStateChange;



    void Awake()
    {

        Debug.Log("GM: Awake - gamestate = " + getGameState());
        setGameState(GameState.GameStart);

        playerBase.BaseIsDead += EndGame;
        enemyBase.BaseIsDead += EndGame;
    }
    void Start()
    {
        setGameState(GameState.GameInProgress);
        Debug.Log("GM: Start - gamestate = " + getGameState());
    }


    public GameState getGameState()
    {
        return CurrentGameState;
    }

    private void setGameState(GameState state)
    {
        CurrentGameState = state;
        GameStateChange?.Invoke(CurrentGameState);
    }
    public void EndGame()
    {
        Debug.Log("GM: EndGame() called");
        setGameState(GameState.GameEnd);
        playerBase.BaseIsDead -= EndGame;
        enemyBase.BaseIsDead -= EndGame;
        Debug.Log("GM: Change Sceen Needed Here Eventually");
        // TODO: This logic should not make it into a build
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
