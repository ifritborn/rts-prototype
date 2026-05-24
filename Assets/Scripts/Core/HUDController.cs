using UnityEngine;

public class HUDController : MonoBehaviour
{
    private TeamController playerTC;
    private UnitData baseUnit = UnitRegistry.getUnitData(UnitEnum.BaseUnit);

    public void Initialize(TeamController playerTC)
    {
        this.playerTC = playerTC;
    }
    public void addUnits()
    {
        Debug.Log("HUDController: Button click heard");
        playerTC.purchaseHandler(baseUnit.GoldCost);
    }
}
