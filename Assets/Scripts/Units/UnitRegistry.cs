using System.Collections.Generic;

public static class UnitRegistry


{

     
    private static readonly Dictionary<UnitEnum, UnitData> units = new Dictionary<UnitEnum, UnitData>
    {
        {
            //  enum name[0], int GoldCost[1], int HP[2], int DMG[3], float AtkSpd[4], float MvSpd[5], float DetectRange[6], float AtkRange[7]
            UnitEnum.Soldier, new UnitData("Soldier", 200, 100, 25, 1.5f, 1f, 3f, .25f)
        },
        {
            UnitEnum.Tank, new UnitData("Tank", 300, 500, 50, 3f, .5f, 2f, .5f)
        }
    };

    public static UnitData getUnitData(UnitEnum unit)
    {
        return units[unit];
    }
}
