using System.Collections.Generic;

public static class UnitRegistry
{
    private static readonly Dictionary<UnitEnum, UnitData> units = new Dictionary<UnitEnum, UnitData>
    {
        {
            // int GoldCost, int HP, int DMG, float AtkSpd, float MvSpd, float DetectRange, float AtkRange
            UnitEnum.Soldier, new UnitData(100, 100, 25, 1.5f, 1f, 5f, .35f)
        }
    };

    public static UnitData getUnitData(UnitEnum unit)
    {
        return units[unit];
    }
}
