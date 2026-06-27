using System;
using UnityEngine;

public class Building : MonoBehaviour, IDamagable
{

    [SerializeField] private bool isBase;
    [SerializeField] private int maxHp;
    private float currentHealth;
    private TeamID teamID;
    private bool isAlive;
    public event Action BaseIsDead;

    // ----------------------------------------------------------------------------------------------------------------


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
        Debug.Log("Building: am I alive? - " + isAlive );
        return this.isAlive;
    }

    public bool getIsBase()
    {
        return this.isBase;
    }
    public float getCurrentHp()
    {
        return currentHealth;
    }

    // ----------------------------------------------------------------------------------------------------------------


    void Awake()
    {
        currentHealth = maxHp;
        this.isAlive = true;
    }

    private void OnDestroy()
    {
    }

    // ----------------------------------------------------------------------------------------------------------------

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
    public void TakeDamage(float dmgVal)
    {

        float updatedHp = currentHealth - dmgVal;
        float hpBounds = Mathf.Clamp(updatedHp, 0, maxHp);
        currentHealth = hpBounds;
        Debug.Log("Building: taking dmg, hp at " + currentHealth);

        if (currentHealth == 0 && this.isAlive == true)
        {
            DeathHandler();
        }

    }





}
