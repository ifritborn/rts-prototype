using UnityEngine;

public class HUDController : MonoBehaviour
{
    private TeamController playerTC;
    private UnitData soldier = UnitRegistry.getUnitData(UnitEnum.Soldier);

    // ----------------------------------------------------------------------------------------------------------------


    public void Initialize(TeamController playerTC)
    {
        this.playerTC = playerTC;
    }

    // ----------------------------------------------------------------------------------------------------------------


    public void addUnits()
    {
        Debug.Log("HUDController: Button click heard");
        playerTC.purchaseHandler(soldier.GoldCost);
    }
}
