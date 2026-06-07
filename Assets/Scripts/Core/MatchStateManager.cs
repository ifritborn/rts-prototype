using System;
using UnityEngine;

public class MatchStateManager : MonoBehaviour
{


    private MatchState CurrentMatchState;

    [SerializeField] private TeamController player;
    private Building playerBase;
    [SerializeField] private TeamController ai;
    private AIBehavior AIB;
    private Building aiBase;
    [SerializeField] private WaveManager WM;

    [SerializeField] private HUDController HUD;
    [SerializeField] private GameSceneManager GSM;
    public event Action<MatchState> MatchStateChange;

    // ----------------------------------------------------------------------------------------------------------------


    void Awake()
    {
        aiBase = ai.getBase();
        playerBase = player.getBase();

        AIB = ai.GetComponent<AIBehavior>();
        // Debug.Log("GM: Awake - MatchState = " + getMatchState());
        setMatchState(MatchState.GameStart);


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
        setMatchState(MatchState.GameInProgress);
        // Time.timeScale = 0.5f;
        // Debug.Log("GM: Start - MatchState = " + getMatchState());
    }

    // ----------------------------------------------------------------------------------------------------------------


    public MatchState getMatchState()
    {
        return CurrentMatchState;
    }

    private void setMatchState(MatchState state)
    {
        CurrentMatchState = state;
        MatchStateChange?.Invoke(CurrentMatchState);
    }
    public void EndGame()
    {
        // Debug.Log("GM: EndGame() called");
        setMatchState(MatchState.GameEnd);
        player.getBase().BaseIsDead -= EndGame;
        ai.getBase().BaseIsDead -= EndGame;
        // Debug.Log("GM: Change Sceen Needed Here Eventually");
        GSM.LoadThisScene("EndCard");
    }
}
