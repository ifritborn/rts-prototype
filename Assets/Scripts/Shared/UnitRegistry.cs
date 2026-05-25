using System.Collections.Generic;

public static class UnitRegistry
{
    private static readonly Dictionary<UnitEnum, UnitData> units = new Dictionary<UnitEnum, UnitData>
    {
        {
            UnitEnum.Soldier, new UnitData(100, 1000, 25, 1.5f, 1f, 3f, 1f)
        }
    };

    public static UnitData getUnitData(UnitEnum unit)
    {
        return units[unit];
    }
}
