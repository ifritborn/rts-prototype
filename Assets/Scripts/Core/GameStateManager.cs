using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{


    GameState CurrentGameState;

    [SerializeField] private TeamController player;
    private Building playerBase;
    [SerializeField] private TeamController ai;
    private Building aiBase;
    [SerializeField] private WaveManager WM;

    public event Action<GameState> GameStateChange;



    void Awake()
    {
        aiBase = ai.getBase();
        playerBase = player.getBase();
        Debug.Log("GM: Awake - gamestate = " + getGameState());
        setGameState(GameState.GameStart);


    }
    void Start()
    {
        
        player.Initialize(WM, aiBase, TeamID.Player);
        ai.Initialize(WM, playerBase, TeamID.AI);
        WM.Initialize(this, player, ai);

        player.getBase().BaseIsDead += EndGame;
        ai.getBase().BaseIsDead += EndGame;

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
        player.getBase().BaseIsDead -= EndGame;
        ai.getBase().BaseIsDead -= EndGame;
        Debug.Log("GM: Change Sceen Needed Here Eventually");
        // TODO: This logic should not make it into a build
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
