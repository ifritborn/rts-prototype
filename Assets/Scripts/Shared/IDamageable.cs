
public interface IDamagable
{
    public TeamID getTeamID();
    public bool getIsAlive();
    public void TakeDamage(int dmg){}
    private void DeathHandler(){}
}
