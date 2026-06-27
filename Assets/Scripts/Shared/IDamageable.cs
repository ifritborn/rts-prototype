
using System;

public interface IDamagable
{
    public TeamID getTeamID();
    public bool getIsAlive();
    public bool getIsBase();
    public void TakeDamage(float dmg){}
    private void DeathHandler(){}
}
