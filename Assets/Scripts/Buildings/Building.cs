using System;
using UnityEngine;

public class Building : MonoBehaviour
{

    [SerializeField] private bool isBase;
    [SerializeField] private int maxHp;
    private int currentHealth;


    public event Action BaseIsDead;

    void Start()
    {
        currentHealth = maxHp;
    }

    private void deathHandler()
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
    public void takeDamage(int dmgVal)
    {

        int updatedHp = currentHealth - dmgVal;
        int hpBounds = Mathf.Clamp(updatedHp, 0, maxHp);
        currentHealth = hpBounds;
        Debug.Log("Building: taking dmg, hp at "+currentHealth);

        if (currentHealth == 0)
        {
            deathHandler();
        }

    }

    private void OnDestroy()
    {


    }



}
