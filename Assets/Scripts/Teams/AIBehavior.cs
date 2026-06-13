using UnityEngine;

public class AIBehavior : MonoBehaviour
{
    WaveManager WM;
    TeamController TC;
    bool flip = true;
    
    // ----------------------------------------------------------------------------------------------------------------
    public void Initialize(WaveManager WM, TeamController TC)
    {
        this.WM = WM;
        this.TC = TC;

        // WM.AIAction += soldierEcon;
        // WM.AIAction += tankRush;
        // WM.AIAction += soldierRush;
        chooseAI();
    }

    // ----------------------------------------------------------------------------------------------------------------

    private void chooseAI()
    {
        int tactic = Random.Range(0,3);
        Debug.Log("tactic: " + tactic);
        switch (tactic)
        {
            case 0:
                Debug.Log("AIBehavior: soldier rush triggered");
                WM.AIAction += soldierRush;
                break;
            case 1:
                Debug.Log("AIBehavior: tank rush triggered");
                WM.AIAction += tankRush;
                break;
            case 2:
                Debug.Log("AIBehavior: soldier econ triggered");
                WM.AIAction += soldierEcon;
                break;
        }
    }
    private void soldierRush()
    {
        int gold = TC.getBankCurrentGold();
        if (gold < 200)
        {
            return;
        }
        TC.purchaseHandler(UnitEnum.Soldier, 1);
    }

    private void tankRush()
    {
        int gold = TC.getBankCurrentGold();
        if (gold < 200)
        {
            return;
        }
        TC.purchaseHandler(UnitEnum.Tank, 1);
    }

    private void soldierEcon()
    {
        int gold = TC.getBankCurrentGold();
        if (gold < 200)
        {
            return;
        }

        if (flip)
        {
            for (int i = 0; i < (gold/200); i++ )
            {
                TC.purchaseHandler(UnitEnum.Soldier, 1);
            }
            flip = !flip;
        } 
        else
        {
            TC.purchaseHandler(200);
            flip = !flip;
        }
    }
}
