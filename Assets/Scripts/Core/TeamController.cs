using UnityEngine;
using System.Collections;

public class TeamController : MonoBehaviour
{
    [SerializeField] private Building myBase;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Bank bank;
    private WaveManager WM;
    private Color teamColor;
    private Building opposingBase;
    private TeamID teamID;

    // ----------------------------------------------------------------------------------------------------------------

    public Building getBase()
    {
        return myBase;
    }
    public Spawner getSpawner()
    {
        return spawner;
    }
    public Color getTeamColor()
    {
        return teamColor;
    }

    public TeamID getTeamID()
    {
        return teamID;
    }

    public Building getOpposingBase()
    {
        return opposingBase;
    }

    // ----------------------------------------------------------------------------------------------------------------

    public void Initialize(WaveManager WM, Building opposingBase, TeamID teamID)
    {
        this.teamColor = myBase.GetComponent<SpriteRenderer>().color;
        this.WM = WM;
        this.opposingBase = opposingBase;
        this.teamID = teamID;

        initializeComponents();
    }

    // ----------------------------------------------------------------------------------------------------------------

    private void initializeComponents()
    {
        spawner.Initialize(WM, opposingBase, teamColor, spawner.transform, teamID);
        bank.Initialize(WM, teamID);
    }

    public bool purchaseHandler(int cost)
    {
        if (bank.canAfford(cost)){
            bank.modifyGold(cost * -1);
            spawner.changeArmySize(1);
            Debug.Log("TC: added 1 unit");
            return true;
        }
        else
        {
            Debug.Log("TC: cannot add unit");
            return false;
        }
    }

}