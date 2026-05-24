using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bank : MonoBehaviour
{
    private int waveIncome;
    private WaveManager WM;
    private int currentGold;
    private TeamID teamID;

    public int getCurrentGold()
    {
        return currentGold;
    }

    public void Initialize(WaveManager WM, TeamID teamID)
    {
        this.WM = WM;
        this.teamID = teamID;

        currentGold = 0;
        waveIncome = 100;

        WM.NextWave += waveIncomeHandler;
    }

    private void waveIncomeHandler()
    {
        currentGold += waveIncome;
        // Debug.Log("Bank: current gold: " + currentGold + " (" + gameObject.name + ")");
    }

    public bool canAfford(int unitPrice)
    {
        bool canBuy = false;
        if (unitPrice <= currentGold)
        {
            canBuy = true;
        }
        return canBuy;
    }

    public void modifyGold(int amt)
    {
        currentGold += amt;
    }



}
