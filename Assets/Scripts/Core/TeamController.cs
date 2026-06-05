using UnityEngine;
using System.Collections;
using System;

public class TeamController : MonoBehaviour
{
    [SerializeField] private Building myBase;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Bank bank;
    private WaveManager WM;
    private Color teamColor;
    private Building opposingBase;
    private TeamID teamID;

    public bool isInitialized = false;

    // ----------------------------------------------------------------------------------------------------------------

    public bool getIsInitialized()
    {
        return isInitialized;
    }
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

    public int getBankWaveIncome()
    {
        return bank.getWaveIncome();
    }

    public int getBankCurrentGold()
    {
        return bank.getCurrentGold();
    }
    public bool getIsBankInitialized()
    {
        return bank.isInitialized;
    }

    // ----------------------------------------------------------------------------------------------------------------

    public void Initialize(WaveManager WM, Building opposingBase, TeamID teamID)
    {
        this.teamColor = myBase.GetComponent<SpriteRenderer>().color;
        this.WM = WM;
        this.opposingBase = opposingBase;
        this.teamID = teamID;

        initializeComponents();
        this.isInitialized = true;
    }

    // ----------------------------------------------------------------------------------------------------------------

    private void initializeComponents()
    {
        myBase.Initialize(teamID);
        spawner.Initialize(WM, opposingBase, teamColor, spawner.transform, teamID);
        bank.Initialize(WM, teamID);
    }

    public bool purchaseHandler(int cost)
    {
        
            if (bank.canAfford(cost))
            {
                bank.modifyGold(cost * -1);
                bank.modifyWaveGold(50);
                Debug.Log("TC: added 50g to economy");
                return true;
            }
            else
            {
                Debug.Log("TC: cannot add to economy");
                return false;
            }
    }

    public bool purchaseHandler(UnitEnum e, int num)
    {
        int cost = UnitRegistry.getUnitData(e).GoldCost;
        if (bank.canAfford(cost))
            {
                bank.modifyGold(cost * -1);
                spawner.addUnitToArmy(e, num);
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