using System;
using System.Collections;
using UnityEditor.UI;
using UnityEngine;

public class WaveManager : MonoBehaviour
{



    [SerializeField] GameManager GM;
    [SerializeField] private Building playerBase;
    [SerializeField] private Building enemyBase;

    [SerializeField] Spawner p_spawner;
    [SerializeField] Spawner e_spawner;

    [SerializeField] float timerInterval;

    private GameManager.GameState state;


    public event Action NextWave;



    void Start()
    {
        playerBase.Initialize(Team.Player);
        p_spawner.Initialize(enemyBase, playerBase.GetComponent<SpriteRenderer>().color, p_spawner.transform, Team.Player, 5);

        enemyBase.Initialize(Team.Opponent);
        e_spawner.Initialize(playerBase, enemyBase.GetComponent<SpriteRenderer>().color, e_spawner.transform, Team.Opponent, 5);

        GM.GameStateChange += GameStateChangeHandler;
        GameStateChangeHandler(GM.getGameState());

    }

    void Update()
    {

    }

    private void GameStateChangeHandler(GameManager.GameState state)
    {
        this.state = state;
        if (state == GameManager.GameState.GameStart)
        {

        }
        else if (state == GameManager.GameState.GameInProgress)
        {
            StartCoroutine(WaveSystem(timerInterval));

        }
        else if (state == GameManager.GameState.GameEnd)
        {
            GM.GameStateChange -= GameStateChangeHandler;
        }
    }

    IEnumerator WaveSystem(float timerInterval)
    {
        while (this.state == GameManager.GameState.GameInProgress)
        {
            NextWave?.Invoke();
            yield return new WaitForSeconds(timerInterval);
        }


    }






}