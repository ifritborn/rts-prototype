
using System;

public interface IDamagable
{
    public TeamID getTeamID();
    public bool getIsAlive();
    public bool getIsBase();
    public void TakeDamage(int dmg){}
    private void DeathHandler(){}
}
