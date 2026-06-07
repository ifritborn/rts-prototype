using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI incomeTxt;
    [SerializeField] private TextMeshProUGUI currentGoldTxt;
    [SerializeField] private TextMeshProUGUI waveTimerTxt;
    // [SerializeField] private TextMeshProUGUI aiArmySizeTxt;
    // [SerializeField] private TextMeshProUGUI aiIncomeTxt;

    private string incomeTxtString = "Income: ";
    private string currentGoldTxtString = "Gold: ";
    private string waveTimerTxtString = "Wave ";
     private string AiIncomeTxtString = "Ai Income: ";
    private string aiArmySizeTxtString = "Ai Wave +";
    private TeamController playerTC;
    private TeamController aiTC;
    private WaveManager WM;

    // ----------------------------------------------------------------------------------------------------------------


    public void Initialize(WaveManager WM, TeamController playerTC, TeamController aiTC)
    {
        this.playerTC = playerTC;
        this.aiTC = aiTC;
        this.WM = WM;
        // Debug.Log(playerTC.getIsBankInitialized());

        WM.NextWave += waveHandler;
        WM.SpawnerAction += onSpawn;

        incomeTxt.text = incomeTxtString + playerTC.getBankWaveIncome().ToString();
        currentGoldTxt.text = currentGoldTxtString + playerTC.getBankCurrentGold().ToString();
        // aiArmySizeTxt.text = aiArmySizeTxtString + aiTC.getSpawner().getArmySize();
        // aiIncomeTxt.text = AiIncomeTxtString + aiTC.getBankWaveIncome().ToString();
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
        playerTC.purchaseHandler(UnitEnum.Soldier, 1);
    }

    public void buyTank()
    {
        Debug.Log("HUDController: Buy Tank heard");
        playerTC.purchaseHandler(UnitEnum.Tank, 1);
    }

    public void growEconomy()
    {
        Debug.Log("HUDController: Grow Econ heard");
        playerTC.purchaseHandler(200);
        incomeTxt.text = incomeTxtString + playerTC.getBankWaveIncome().ToString();
    }

    private void waveHandler()
    {
        waveTimerTxt.text = waveTimerTxtString + WM.getWaveNumber().ToString() + ": " + WM.getTimerInterval().ToString();
    }

    private void onSpawn()
    {
        // aiArmySizeTxt.text = aiArmySizeTxtString + aiTC.getSpawner().getArmySize();
        // aiIncomeTxt.text = AiIncomeTxtString + aiTC.getBankWaveIncome().ToString();
    }
}
