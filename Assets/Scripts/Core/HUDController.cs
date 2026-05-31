using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI incomeTxt;
    [SerializeField] private TextMeshProUGUI currentGoldTxt;
    [SerializeField] private TextMeshProUGUI waveTimerTxt;

    private string incomeTxtString = "Income: ";
    private string currentGoldTxtString = "Gold: ";

    private string waveTimerTxtString = "Wave ";
    private TeamController playerTC;
    private WaveManager WM;
    private UnitData soldier = UnitRegistry.getUnitData(UnitEnum.Soldier);
    private UnitData tank = UnitRegistry.getUnitData(UnitEnum.Tank);

    // ----------------------------------------------------------------------------------------------------------------


    public void Initialize(WaveManager WM, TeamController playerTC)
    {
        this.playerTC = playerTC;
        this.WM = WM;
        Debug.Log(playerTC.getIsBankInitialized());

        WM.NextWave += waveHandler;
        incomeTxt.text = incomeTxtString + playerTC.getBankWaveIncome().ToString();
        currentGoldTxt.text = currentGoldTxtString + playerTC.getBankCurrentGold().ToString();
    }

    private void Update()
    {
        currentGoldTxt.text = currentGoldTxtString + playerTC.getBankCurrentGold().ToString();

        int min = Mathf.FloorToInt(WM.getTimeToWave() / 60);
        int sec = Mathf.FloorToInt(WM.getTimeToWave() % 60);
        string timerText = $"{min:00}:{sec:00}";
        waveTimerTxt.text = waveTimerTxt.text = waveTimerTxtString + WM.getWaveNumber().ToString() + ": " + timerText;
    }

    // ----------------------------------------------------------------------------------------------------------------


    public void buySoldier()
    {
        Debug.Log("HUDController: Buy Soldier heard");
        playerTC.purchaseHandler(soldier.GoldCost, true);
    }

    public void buyTank()
    {
        Debug.Log("HUDController: Buy Tank heard");
        playerTC.purchaseHandler(tank.GoldCost, true);
    }

    public void growEconomy()
    {
        Debug.Log("HUDController: Grow Econ heard");
        playerTC.purchaseHandler(100, false);
        incomeTxt.text = incomeTxtString + playerTC.getBankWaveIncome().ToString();
    }

    private void waveHandler()
    {
        waveTimerTxt.text = waveTimerTxtString + WM.getWaveNumber().ToString() + ": " + WM.getTimerInterval().ToString();
    }
}
