using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bank : MonoBehaviour
{
    private int waveIncome;
    private WaveManager WM;
    private int currentGold;

    public int getCurrentGold()
    {
        return currentGold;
    }

    public void Initialize(WaveManager WM)
    {
        this.WM = WM;

        currentGold = 0;
        waveIncome = 100;

        WM.NextWave += waveIncomeHandler;
    }

    private void waveIncomeHandler()
    {
        currentGold += waveIncome;
        Debug.Log("Bank: current gold: " + currentGold + " (" + gameObject.name + ")");
    }
}
