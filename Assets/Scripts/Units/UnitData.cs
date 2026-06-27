using Unity.Mathematics;

public class UnitData
{
    // This is a POJO/SCHEMA
    public string Name { get; }
    public int GoldCost { get; }
    public int MaxHP { get; }
    public float Dmg { get; }
    public float AtkSpd { get; }
    public float MvSpd { get; }
    public float DetectRange { get; }
    public float AtkRange { get; }

    public UnitData(string Name, int GoldCost, int HP, float DMG, float AtkSpd, float MvSpd, float DetectRange, float AtkRange)
    {
        this.Name = Name;
        this.GoldCost = GoldCost;
        this.MaxHP = HP;
        this.Dmg = DMG;
        this.AtkSpd = AtkSpd;
        this.MvSpd = MvSpd;
        this.DetectRange = DetectRange;
        this.AtkRange = AtkRange;
    }


}
