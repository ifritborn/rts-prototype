using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{


    private GameState CurrentGameState;

    [SerializeField] private TeamController player;
    private Building playerBase;
    [SerializeField] private TeamController ai;
    private AIBehavior AIB;
    private Building aiBase;
    [SerializeField] private WaveManager WM;

    [SerializeField] private HUDController HUD;

    public event Action<GameState> GameStateChange;

    // ----------------------------------------------------------------------------------------------------------------


    void Awake()
    {
        aiBase = ai.getBase();
        playerBase = player.getBase();

        AIB = ai.GetComponent<AIBehavior>();
        // Debug.Log("GM: Awake - gamestate = " + getGameState());
        setGameState(GameState.GameStart);


    }
    void Start()
    {

        player.Initialize(WM, aiBase, TeamID.Player);
        ai.Initialize(WM, playerBase, TeamID.AI);

        WM.Initialize(this, player, ai);
        HUD.Initialize(WM, player, ai);

        // player.getSpawner().changeArmySize(0);
        // ai.getSpawner().changeArmySize(0);



        player.getBase().BaseIsDead += EndGame;
        ai.getBase().BaseIsDead += EndGame;
        AIB.Initialize(WM, ai);
        setGameState(GameState.GameInProgress);
        // Time.timeScale = 0.5f;
        // Debug.Log("GM: Start - gamestate = " + getGameState());
    }

    // ----------------------------------------------------------------------------------------------------------------


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
        // Debug.Log("GM: EndGame() called");
        setGameState(GameState.GameEnd);
        player.getBase().BaseIsDead -= EndGame;
        ai.getBase().BaseIsDead -= EndGame;
        // Debug.Log("GM: Change Sceen Needed Here Eventually");
        // TODO: This logic should not make it into a build
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
