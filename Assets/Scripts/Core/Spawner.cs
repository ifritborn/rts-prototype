using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] WaveManager waveManager;
    [SerializeField] Unit unitPrefab;
    private Building opposingBase;
    private Color teamColor;
    private int spawnCount;
    private Vector3 SpawnPos;
    private Quaternion SpawnRotation;
    private Team team;



    public void Initialize(Building opposingBase, Color teamColor, Transform tform, Team team, int spawncount)
    {
        this.opposingBase = opposingBase;
        this.teamColor = teamColor;
        this.SpawnPos = tform.position;
        this.SpawnRotation = tform.rotation;
        this.team = team;
        this.spawnCount = spawncount;
    }
    void Awake()
    {
        waveManager.NextWave += SpawnWave;
    }


    void Start()
    {
    }

    void SpawnWave()
    {
        StartCoroutine(SpawnUnit());
    }


    IEnumerator SpawnUnit()
    {
        while (spawnCount > 0)
        {
            var newUnit = Instantiate(unitPrefab, SpawnPos, SpawnRotation);
            var unitScript = newUnit.GetComponent<Unit>();
            unitScript.Initialize(opposingBase.transform, teamColor, team);
            yield return new WaitForSeconds(1f);
            spawnCount -= 1;
        }
    }
}
