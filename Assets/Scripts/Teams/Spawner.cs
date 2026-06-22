using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{

    [SerializeField] Unit soldierPrefab;
    [SerializeField] Unit tankPrefab;
    [SerializeField] Unit archerPrefab;

    private WaveManager WM;
    private Building opposingBase;
    private Color teamColor;
    private Vector3 SpawnPos;
    private Quaternion SpawnRotation;
    private TeamID team;

    private Dictionary<UnitEnum, int> armyPool = new Dictionary<UnitEnum, int>();

    public int getArmySize()
    {
        return armyPool[UnitEnum.Soldier] + armyPool[UnitEnum.Tank];
    }


    // ----------------------------------------------------------------------------------------------------------------

    public void Initialize(WaveManager WM, Building opposingBase, Color teamColor, Transform tform, TeamID team)
    {
        this.WM = WM;
        this.opposingBase = opposingBase;
        this.teamColor = teamColor;
        this.SpawnPos = tform.position;
        this.SpawnRotation = tform.rotation;
        this.team = team;

        setupArmyDict();

        WM.SpawnerAction += SpawnWave;
    }

    // ----------------------------------------------------------------------------------------------------------------

    private void setupArmyDict()
    {
        armyPool.Add(UnitEnum.Soldier, 1);
        armyPool.Add(UnitEnum.Tank, 1);
        armyPool.Add(UnitEnum.Archer, 1);
    }

    public void addUnitToArmy(UnitEnum unit, int num)
    {
        armyPool[unit] += num;
    }

    private void SpawnWave()
    {
        StartCoroutine(SpawnUnit());
    }


    private Unit pickPrefab(KeyValuePair<UnitEnum, int> unit)
    {

        // Debug.Log("SpawnUnit: pickPrefab() " + unit.Key);
        switch (unit.Key)
        {
            case UnitEnum.Soldier:
                return soldierPrefab;
            case UnitEnum.Tank:
                return tankPrefab;
            case UnitEnum.Archer:
                return archerPrefab;
            default:
                return null;
        }
    }


    IEnumerator SpawnUnit()
    {

        foreach (KeyValuePair<UnitEnum, int> unit in armyPool)
        {
            for (int i = 0; i < unit.Value; i++)
            {
            float spread = Random.Range(-.5f, .5f);
            Vector3 SpreadSpawnPos = transform.position + new Vector3(spread, spread, 0);
            Unit prefab = pickPrefab(unit);
            var newUnit = Instantiate(prefab, SpreadSpawnPos, SpawnRotation);
            newUnit.name = $"{team} Unit";
            var unitScript = newUnit.GetComponent<Unit>();
            unitScript.Initialize(opposingBase.transform, teamColor, team, unit.Key);
            }
        }
        yield return new WaitForSeconds(.01f);
    }
}
