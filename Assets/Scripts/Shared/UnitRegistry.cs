using System.Collections.Generic;

public static class UnitRegistry
{
    private static readonly Dictionary<UnitEnum, UnitData> units = new Dictionary<UnitEnum, UnitData>
    {
        {
            UnitEnum.BaseUnit, new UnitData
            {
                GoldCost = 100
            }
        }
    };

    public static UnitData getUnitData(UnitEnum unit)
    {
        return units[unit];
    }
}
