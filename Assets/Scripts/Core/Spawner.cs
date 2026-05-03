using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] WaveManager waveManager;
    [SerializeField] Unit unitPrefab;
    private Building opposingBase;
    private Color teamColor;
    private int spawnCount = 1;
    private Vector3 SpawnPos;
    private Quaternion SpawnRotation;

    public void Initialize(Building opposingBase, Color teamColor)
    {
        this.opposingBase = opposingBase;
        this.teamColor = teamColor;
    }
    void Awake()
    {
        waveManager.NextWave += SpawnWave;
    }


    void Start()
    {
        SpawnPos = transform.position;
        SpawnRotation = transform.rotation;
    }

    void SpawnWave()
    {
        StartCoroutine(SpawnUnit());
    }


    IEnumerator SpawnUnit()
    {
        while (spawnCount > 0)
        {
            Debug.Log("Spawner: SpawnUnit(): spawnCount: " + spawnCount);
            Debug.Log("Spawner: opposingBase = " + opposingBase);
            var newUnit = Instantiate(unitPrefab, SpawnPos, SpawnRotation);
            var unitScript = newUnit.GetComponent<Unit>();
            unitScript.Initialize(opposingBase.transform, teamColor);
            yield return new WaitForSeconds(1f);
            spawnCount -= 1;
        }
    }
}
