using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    [SerializeField] Unit unitPrefab;
    private WaveManager WM;
    private Building opposingBase;
    private Color teamColor;
    private Vector3 SpawnPos;
    private Quaternion SpawnRotation;
    private TeamID team;
    private int armySize = 1;


    // ----------------------------------------------------------------------------------------------------------------

    public void Initialize(WaveManager WM, Building opposingBase, Color teamColor, Transform tform, TeamID team)
    {
        this.WM = WM;
        this.opposingBase = opposingBase;
        this.teamColor = teamColor;
        this.SpawnPos = tform.position;
        this.SpawnRotation = tform.rotation;
        this.team = team;


        WM.NextWave += SpawnWave;
    }

    // ----------------------------------------------------------------------------------------------------------------


    public void changeArmySize(int num)
    {
        armySize += num;
    }

    void SpawnWave()
    {
        StartCoroutine(SpawnUnit());
    }


    IEnumerator SpawnUnit()
    {
        for (int i = 0; i < armySize; i++)
        {
            float spread = Random.Range(-.5f, .5f);
            Vector3 SpreadSpawnPos = transform.position + new Vector3(spread, spread, 0);
            var newUnit = Instantiate(unitPrefab, SpreadSpawnPos, SpawnRotation);
            newUnit.name = $"{team} Unit {i + 1}";
            var unitScript = newUnit.GetComponent<Unit>();
            unitScript.Initialize(opposingBase.transform, teamColor, team, UnitEnum.Soldier);
            yield return new WaitForSeconds(.01f);
        }
    }
}
