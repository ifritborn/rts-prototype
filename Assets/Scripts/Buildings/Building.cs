using System;
using UnityEngine;

public class Building : MonoBehaviour, IDamagable
{

    [SerializeField] private bool isBase;
    [SerializeField] private int maxHp;
    private int currentHealth;
    private Team team;

    public event Action BaseIsDead;

    public void Initialize(Team team)
    {
        this.team = team;
    }

    public Team getTeam()
    {
        return this.team;
    }

    void Start()
    {
        currentHealth = maxHp;
    }

    private void DeathHandler()
    {
        if (!isBase)
        {
            // emit event to player to remove building from pool
            // destroy self
        }
        else
        {
            // emit base destroyed event to game manager
            BaseIsDead?.Invoke();
            Debug.Log("Building: Death Event Triggered");

        }
    }
    public void TakeDamage(int dmgVal)
    {

        int updatedHp = currentHealth - dmgVal;
        int hpBounds = Mathf.Clamp(updatedHp, 0, maxHp);
        currentHealth = hpBounds;
        Debug.Log("Building: taking dmg, hp at " + currentHealth);

        if (currentHealth == 0)
        {
            DeathHandler();
        }

    }

    private void OnDestroy()
    {


    }



}
