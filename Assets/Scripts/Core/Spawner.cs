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



    public void Initialize(Building opposingBase, Color teamColor, Transform tform, Team team, int spawnCount)
    {
        this.opposingBase = opposingBase;
        this.teamColor = teamColor;
        this.SpawnPos = tform.position;
        this.SpawnRotation = tform.rotation;
        this.team = team;
        this.spawnCount = spawnCount;

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
        for (int i = 0; i < spawnCount; i++)
        {
            float spread = Random.Range(-.25f, .25f);
            Vector3 SpreadSpawnPos = transform.position + new Vector3(spread, spread, 0);
            var newUnit = Instantiate(unitPrefab, SpreadSpawnPos, SpawnRotation);
            newUnit.name = $"{team} Unit";
            var unitScript = newUnit.GetComponent<Unit>();
            unitScript.Initialize(opposingBase.transform, teamColor, team);
            yield return new WaitForSeconds(.01f);
        }
    }
}
