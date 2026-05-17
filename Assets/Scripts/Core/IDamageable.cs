
public interface IDamagable
{
    public Team getTeam();
    public bool getIsAlive();
    public void TakeDamage(int dmg){}
    private void DeathHandler(){}
}
