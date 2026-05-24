using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour, IDamagable
{

    private Transform targetPOS;
    private bool isMoving;
    private bool isAttacking;
    [SerializeField] private int maxHp;
    private int currentHealth;
    private float unitMvSpd = 1f;
    private float unitAtkSpd = 1.5f;
    private TeamID teamID;
    private bool isAlive;

    // ----------------------------------------------------------------------------------------------------------------

    public TeamID getTeamID()
    {
        return this.teamID;
    }

    public bool getIsAlive()
    {
        return this.isAlive;
    }

        void getTarget()
    {

    }

    // ----------------------------------------------------------------------------------------------------------------

    public void Initialize(Transform pos, Color spriteColor, TeamID teamID)
    {
        this.targetPOS = pos;
        this.GetComponent<SpriteRenderer>().color = spriteColor;
        this.teamID = teamID;
    }

    void Start()
    {
        currentHealth = maxHp;
        isMoving = true;
        isAttacking = false;
        isAlive = true;

    }

    void Update()
    {
        move();
    }

    // ----------------------------------------------------------------------------------------------------------------



    void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPOS.position, unitMvSpd * Time.deltaTime);
    }

    IEnumerator AttackTarget(IDamagable target)
    {
        isAttacking = true;

        while (target != null)
        {
            if (target.getIsAlive() == false)
            {
                if (!isMoving)
                {
                    isMoving = true;
                    unitMvSpd = 1;
                }
                break;
            }
            else
            {
                if (isMoving)
                {
                    isMoving = false;
                    unitMvSpd = 0;
                }
                target.TakeDamage(25);
                yield return new WaitForSeconds(unitAtkSpd);
            }

        }
        // Debug.Log("Unit: attack while loop over");
        isAttacking = false;
    }

    private bool CanAttackTarget(IDamagable target)
    {
        bool CanAttack = false;
        if (target.getTeamID() != this.teamID && target.getIsAlive() == true)
        {
            CanAttack = true;
            Debug.Log("Unit: is this alive? = " + target.getIsAlive());
        }

        return CanAttack;
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        IDamagable t = target.gameObject.GetComponent<IDamagable>();
        Debug.Log("Unit: coliding with: " + target.gameObject.name);


        bool CanAttack = CanAttackTarget(t);
        Debug.Log("Unit: can I attack? " + CanAttack);

        if (CanAttack)
        {
            StartCoroutine(AttackTarget(t));
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Debug.Log("Unit: exiting collision");
        if (!isMoving)
        {
            isMoving = true;
            unitMvSpd = 1;
        }

    }


    public void TakeDamage(int dmgVal)
    {

        int updatedHp = currentHealth - dmgVal;
        int hpBounds = Mathf.Clamp(updatedHp, 0, maxHp);
        currentHealth = hpBounds;
        // Debug.Log("Unit: dmg - hp at: " + currentHealth);

        if (currentHealth == 0 && this.isAlive == true)
        {
            DeathHandler();
        }

    }

    private void DeathHandler()
    {
        // unit destroyed logic 
        this.isAlive = false;
        Destroy(gameObject);
        // Debug.Log("Unit Death");

    }
}
