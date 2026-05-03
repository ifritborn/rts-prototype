using System;
using UnityEditor.UI;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField] GameManager GM;
    [SerializeField] private Building playerBase;
    [SerializeField] private Building enemyBase;

    [SerializeField] Spawner p_spawner;
    [SerializeField] Spawner e_spawner;

    public event Action NextWave;



    void Start()
    {
        p_spawner.Initialize(enemyBase, playerBase.GetComponent<SpriteRenderer>().color);
        e_spawner.Initialize(playerBase, enemyBase.GetComponent<SpriteRenderer>().color);
        GM.GameStateChange += GameStateChangeHandler;
        GameStateChangeHandler(GM.getGameState());

    }

    void Update()
    {

    }

    private void GameStateChangeHandler(GameManager.GameState state)
    {
        if (state == GameManager.GameState.GameStart)
        {

        }
        else if (state == GameManager.GameState.GameInProgress)
        {

            NextWave?.Invoke();
        }
        else if (state == GameManager.GameState.GameEnd)
        {
            GM.GameStateChange -= GameStateChangeHandler;
        }
    }






}