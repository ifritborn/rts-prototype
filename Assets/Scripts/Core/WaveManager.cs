using System.Collections;
using UnityEditor.UI;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Unit playerUnits;
    [SerializeField] GameManager GM;

    private int spawnCount = 1;

    private Vector3 playerSpawnPos;
    private Quaternion playerSpawnRotation;

    void Start()
    {
        playerSpawnPos = transform.position;
        playerSpawnRotation = transform.rotation;
        GM.GameStateChange += GameStateChangeHandler;
        
    }

    void Update()
    {

    }

    private void GameStateChangeHandler(GameManager.GameState state)
    {
        if (state == GameManager.GameState.GameStart)
        {
            Debug.Log("WaveManager: SpawnUnit(): GameState = " + GM.getGameState());
        }
        else if (state == GameManager.GameState.GameInProgress)
        {
            Debug.Log("WaveManager: SpawnUnit(): GameState = " + GM.getGameState());
            StartCoroutine(SpawnUnit());
        }
        else if (state == GameManager.GameState.GameEnd)
        {
            Debug.Log("WaveManager: SpawnUnit(): GameState = " + GM.getGameState());
            GM.GameStateChange -= GameStateChangeHandler;
        }
    }

    IEnumerator SpawnUnit()
    {
        while (spawnCount > 0)
        {
            
            if (GM.getGameState() == GameManager.GameState.GameInProgress)
            {
                Debug.Log("WaveManager: SpawnUnit(): spawnCount: " + spawnCount);
                Instantiate(playerUnits, playerSpawnPos, playerSpawnRotation);
                
            }
            yield return new WaitForSeconds(1f);
            spawnCount -= 1;
        }
    }




}