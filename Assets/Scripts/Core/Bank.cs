using UnityEngine;


public class Bank : MonoBehaviour
{
    private int waveIncome;
    private WaveManager WM;
    private int currentGold;
    private TeamID teamID;
    public bool isInitialized = false;

    public bool getIsInitialized()
    {
        return isInitialized;
    }

    public int getCurrentGold()
    {
        return currentGold;
    }

    public int getWaveIncome()
    {
        return waveIncome;
    }

    // ----------------------------------------------------------------------------------------------------------------

    public void Initialize(WaveManager WM, TeamID teamID)
    {
        this.WM = WM;
        this.teamID = teamID;

        currentGold = 0;
        waveIncome = 100;

        WM.NextWave += waveIncomeHandler;

        this.isInitialized = true;
        Debug.Log("Bank: current gold: " + currentGold);
    }

    // ----------------------------------------------------------------------------------------------------------------


    private void waveIncomeHandler()
    {
        currentGold += waveIncome;
        Debug.Log("Bank: current gold: " + currentGold + " (" + gameObject.name + ")");
    }

    public bool canAfford(int unitPrice)
    {
        if (unitPrice <= currentGold)
        {
            return true;
        }
        return false;
    }

    public void modifyGold(int amt)
    {
        currentGold += amt;
    }

    public void modifyWaveGold(int amt)
    {
        this.waveIncome += amt;
    }


}
