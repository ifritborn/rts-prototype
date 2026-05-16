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
    private Team team;





    public void Initialize(Transform pos, Color spriteColor, Team team)
    {
        this.targetPOS = pos;
        this.GetComponent<SpriteRenderer>().color = spriteColor;
        this.team = team;
    }

    void Start()
    {
        currentHealth = maxHp;
        isMoving = true;
        isAttacking = false;

    }

    void Update()
    {
        move();
    }




    public Team getTeam()
    {
        return this.team;
    }

    void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPOS.position, unitMvSpd * Time.deltaTime);
    }

    void getTarget()
    {

    }

    IEnumerator AttackTarget(IDamagable target)
    {
        isAttacking = true;

        Debug.Log("Unit: attacking: " + target);

        while (target != null)
        {
            Debug.Log("Unit: swinging on target");
            target.TakeDamage(100);

            yield return new WaitForSeconds(unitAtkSpd);
        }
        Debug.Log("Unit: attack while loop over");
        isAttacking = false;
    }

    private bool CanAttackTarget(IDamagable target)
    {
        bool CanAttack = false;
        if (target.getTeam() != this.team)
        {
            CanAttack = true;
        }

        return CanAttack;
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        IDamagable t = target.gameObject.GetComponent<IDamagable>();
        Debug.Log("Unit: coliding with: " + target.gameObject.name);
        if (isMoving)
        {
            isMoving = false;
            unitMvSpd = 0;
        }

        bool CanAttack = CanAttackTarget(t);
        Debug.Log("Unit: can I attack? " + CanAttack);

        if (CanAttack)
        {
            StartCoroutine(AttackTarget(t));
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Unit: exiting collision");
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
        Debug.Log("Unit: dmg - hp at: " + currentHealth);

        if (currentHealth == 0)
        {
            DeathHandler();
        }

    }

    private void DeathHandler()
    {
        // unit destroyed logic 
        Destroy(gameObject);
        Debug.Log("Unit Death");

    }
}
