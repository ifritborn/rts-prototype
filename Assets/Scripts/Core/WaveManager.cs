using System;
using System.Collections;
using UnityEditor.UI;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    private GameStateManager GSM;
    private TeamController player;
    private TeamController ai;
    private Spawner pSpawner;
    private Spawner aiSpawner;
    private float timerInterval = 15f;
    private GameState state;
    private int waveNumber;

    public event Action NextWave;

    public void Initialize(GameStateManager GSM, TeamController player, TeamController ai)
    {
        this.GSM = GSM;
        this.player = player;
        this.ai = ai;

        waveNumber = 0;
        GSM.GameStateChange += GameStateChangeHandler;
        GameStateChangeHandler(GSM.getGameState());
    }

    private void GameStateChangeHandler(GameState state)
    {
        this.state = state;
        if (state == GameState.GameStart)
        {

        }
        else if (state == GameState.GameInProgress)
        {
            StartCoroutine(WaveSystem(timerInterval));

        }
        else if (state == GameState.GameEnd)
        {
            GSM.GameStateChange -= GameStateChangeHandler;
        }
    }

    IEnumerator WaveSystem(float timerInterval)
    {
        while (this.state == GameState.GameInProgress)
        {
            NextWave?.Invoke();
            waveNumber += 1;
            Debug.Log("WM: wave num: " + waveNumber);
            yield return new WaitForSeconds(timerInterval);
        }


    }






}