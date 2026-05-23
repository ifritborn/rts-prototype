using System;
using UnityEngine;

public class Building : MonoBehaviour, IDamagable
{

    [SerializeField] private bool isBase;
    [SerializeField] private int maxHp;
    private int currentHealth;
    private TeamID teamID;
    private bool isAlive;

    public event Action BaseIsDead;

    public void Initialize(TeamID teamID)
    {
        this.teamID = teamID;
    }

    public TeamID getTeamID()
    {
        return this.teamID;
    }

    public bool getIsAlive()
    {
        return this.isAlive;
    }

    void Start()
    {
        currentHealth = maxHp;
        this.isAlive = true;
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
            // emit base destroyed event to game manager3
            this.isAlive = false;
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

        if (currentHealth == 0 && this.isAlive == true)
        {
            DeathHandler();
        }

    }

    private void OnDestroy()
    {


    }



}
