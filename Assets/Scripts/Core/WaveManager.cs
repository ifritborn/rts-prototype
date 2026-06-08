using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    private MatchStateManager MSM;
    private TeamController player;
    private TeamController ai;
    private Spawner pSpawner;
    private Spawner aiSpawner;
    private float timerInterval = 15f;
    private float timeToWave;
    private MatchState state;
    private int waveNumber;

    public event Action NextWave;
    public event Action AIAction;
    public event Action SpawnerAction;

    public int getWaveNumber()
    {
        return waveNumber;
    }

    public float getTimerInterval()
    {
        return timerInterval;
    }

    public float getTimeToWave()
    {
        return timeToWave;
    }

    // ----------------------------------------------------------------------------------------------------------------


    public void Initialize(MatchStateManager MSM, TeamController player, TeamController ai)
    {
        this.MSM = MSM;
        this.player = player;
        this.ai = ai;

        waveNumber = 0;
        timeToWave = timerInterval;
        MSM.MatchStateChange += MatchStateChangeHandler;
        MatchStateChangeHandler(MSM.getMatchState());
    }

    // ----------------------------------------------------------------------------------------------------------------


    private void MatchStateChangeHandler(MatchState state)
    {
        this.state = state;
        if (state == MatchState.GameStart)
        {

        }
        else if (state == MatchState.GameInProgress)
        {
            StartCoroutine(WaveSystem(timerInterval));

        }
        else if (state == MatchState.GameEnd)
        {
            MSM.MatchStateChange -= MatchStateChangeHandler;
        }
    }

    IEnumerator WaveSystem(float timerInterval)
    {
        while (this.state == MatchState.GameInProgress)
        {
            NextWave?.Invoke();
            SpawnerAction?.Invoke();
            AIAction?.Invoke();
            
            waveNumber += 1;
            while (timeToWave > 0)
            {
                timeToWave -= Time.deltaTime;
                yield return null;
            }
            // Debug.Log("WM: wave num: " + waveNumber); 

            timeToWave = timerInterval;
        }
    }
}