using System.Collections;
using System.Security.Cryptography;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour, IDamagable
{

    private Transform opposingBasePos;
    private Transform targetPOS;
    private CombatController CC;
    private UnitEnum unitType;
    private int maxHp;
    private int unitDmg;
    private float unitMvSpd;
    private float unitAtkSpd;
    private float detectRng;
    private float atkRng;
    private TeamID teamID;
    private int currentHP;
    private bool isAlive;

    // ----------------------------------------------------------------------------------------------------------------

    public TeamID getTeamID()
    {
        return this.teamID;
    }

    public UnitEnum getUnitType()
    {
        return this.unitType;
    }

    public bool getIsAlive()
    {
        return this.isAlive;
    }

    public float getAtkSpd()
    {
        return unitAtkSpd;
    }

    public int getDmg()
    {
        return unitDmg;
    }

    public float getDetectRange()
    {
        return detectRng;
    }

    public float getAtkRng()
    {
        return atkRng;
    }

    // ----------------------------------------------------------------------------------------------------------------


    public void Initialize(Transform pos, Color spriteColor, TeamID teamID, UnitEnum unitType)
    {
        this.opposingBasePos = pos;
        this.targetPOS = opposingBasePos;
        this.GetComponent<SpriteRenderer>().color = spriteColor;
        this.teamID = teamID;
        this.unitType = unitType;

        CC = GetComponent<CombatController>();
        setupUnit();
        CC.Initialize(this, atkRng);
    }

    void Update()
    {
        if (CC.getCState() == CombatStateEnum.Moving)
        {
            setMoveTarget();
            move(targetPOS);
        }
    }

    // ----------------------------------------------------------------------------------------------------------------

    private void move(Transform targetPOS)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPOS.position, unitMvSpd * Time.deltaTime);
    }

    private void setMoveTarget()
    {
        if (!CC.getTargetPOS())
        {
            targetPOS = opposingBasePos;
        }
        else
        {
            targetPOS = CC.getTargetPOS();
        }
    }

    private void setupUnit()
    {
        this.maxHp = UnitRegistry.getUnitData(unitType).MaxHP;
        this.unitDmg = UnitRegistry.getUnitData(unitType).Dmg;
        this.unitAtkSpd = UnitRegistry.getUnitData(unitType).AtkSpd;
        this.unitMvSpd = UnitRegistry.getUnitData(unitType).MvSpd;
        this.detectRng = UnitRegistry.getUnitData(unitType).DetectRange;
        this.atkRng = UnitRegistry.getUnitData(unitType).AtkRange;

        this.currentHP = maxHp;
        this.isAlive = true;
    }



    void OnCollisionExit2D(Collision2D collision)
    {
        // Debug.Log("Unit: exiting collision");
        // if (!isMoving)
        // {
        //     isMoving = true;
        //     unitMvSpd = 1;
        // }

    }


    public void TakeDamage(int dmgVal)
    {

        int updatedHp = currentHP - dmgVal;
        int hpBounds = Mathf.Clamp(updatedHp, 0, maxHp);
        currentHP = hpBounds;
        // Debug.Log("Unit: dmg - hp at: " + currentHealth);

        if (currentHP == 0 && this.isAlive == true)
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
