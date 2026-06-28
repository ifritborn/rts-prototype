using System;
using System.Collections;
using UnityEngine;

public class CombatController : MonoBehaviour
{

    [SerializeField] Projectile arrowPrefab;
    private CombatStateEnum CState;
    private Unit unit;
    private float unitAtkRng;
    private bool hasTarget;
    private bool isAttacking;
    private bool isInitialized;
    private Transform targetPOS;
    private Transform currentTarget;
    private IDamagable currentDamagable;
    private Unit currentUnitTarget;
    private float baseDmg;
    private float counterDmg;


    private float strongModifier = 2f;

    private float weakModifier = .5f;


    public Transform getTargetPOS()
    {
        return targetPOS;
    }

    // ----------------------------------------------------------------------------------------------------------------
    public void Initialize(Unit unit)
    {
        this.unit = unit;
        this.unitAtkRng = unit.getAtkRng();
        this.baseDmg = unit.getDmg();

        CState = CombatStateEnum.Moving;

        hasTarget = false;
        isAttacking = false;
        isInitialized = true;

    }


    private void Update()
    {
        if (isInitialized)
        {
            if (!hasTarget)
            {
                // if (unit.name == "Player Unit 1")
                // {
                //     Debug.Log(
                //                 "CC - findTarget() \n" +
                //                 "====================\n" +
                //                 gameObject.name + "\n" +
                //                 "====================\n" +
                //                 "State: " + CState + "\n" +
                //                 "Has Target: " + hasTarget + "\n" +
                //                 "Attacking: " + isAttacking + "\n" +
                //                 "Target: " + currentTarget + "\n"
                //             );
                // }
                findTarget();
            }
            else if (hasTarget && !isAttacking)
            {
                // if (unit.name == "Player Unit 1")
                // {
                //     Debug.Log(
                //                 "CC - attackHandler() \n" +
                //                 "====================\n" +
                //                 gameObject.name + "\n" +
                //                 "====================\n" +
                //                 "State: " + CState + "\n" +
                //                 "Has Target: " + hasTarget + "\n" +
                //                 "Attacking: " + isAttacking + "\n" +
                //                 "Target: " + currentTarget + "\n"
                //             ); ;
                // }
                attackHandler(currentTarget, currentDamagable, currentUnitTarget);
            }
        }

    }

    // ----------------------------------------------------------------------------------------------------------------



    public CombatStateEnum getCState()
    {
        return CState;
    }

    private float unitCounterSystem(float baseDmg, Unit u)
    {
        float counterDmg;
        switch (unit.getUnitType())
        {
            case UnitEnum.Soldier:
                if (u.getUnitType() == UnitEnum.Archer)
                {
                    counterDmg = baseDmg * strongModifier;
                    Debug.Log("counterDmg = " + counterDmg);
                    return counterDmg;
                }
                else if (u.getUnitType() == UnitEnum.Tank)
                {
                    counterDmg = baseDmg * weakModifier;
                    Debug.Log("counterDmg = " + counterDmg);
                    return counterDmg;
                }
                else
                {
                    return baseDmg;
                }
            case UnitEnum.Tank:
                if (u.getUnitType() == UnitEnum.Soldier)
                {
                    counterDmg = baseDmg * strongModifier;
                    Debug.Log("counterDmg = " + counterDmg);
                    return counterDmg;
                }
                else if (u.getUnitType() == UnitEnum.Archer)
                {
                    counterDmg = baseDmg * weakModifier;
                    Debug.Log("counterDmg = " + counterDmg);
                    return counterDmg;
                }
                else
                {
                    return baseDmg;
                }
            case UnitEnum.Archer:
                if (u.getUnitType() == UnitEnum.Tank)
                {
                    counterDmg = baseDmg * strongModifier;
                    Debug.Log("counterDmg = " + counterDmg);
                    return counterDmg;
                }
                else if (u.getUnitType() == UnitEnum.Soldier)
                {
                    counterDmg = baseDmg * weakModifier;
                    Debug.Log("counterDmg = " + counterDmg);
                    return counterDmg;
                }
                else
                {
                    return baseDmg;
                }
            default:
                return baseDmg;
        }
    }

    private bool isValidTarget(IDamagable target)
    {
        // if (unit.name == "Player Unit 1" && target.getTeamID() != TeamID.Player)
        //     {
        //          Debug.Log("IVT: target/team " + target + " / " + target.getTeamID());
        //     }
        if (target.getTeamID() != unit.getTeamID() && target.getIsAlive() == true)
        {
            return true;
        }

        return false;
    }

    private void findTarget()
    {
        // Debug.Log("findTarget() called");
        Collider2D[] targetList = Physics2D.OverlapCircleAll(unit.transform.position, unit.getDetectRange());
        // Debug.Log("target list length = " + targetList.Length);


        for (int i = 0; i < targetList.Length; i++)
        {
            Unit u = targetList[i].gameObject.GetComponent<Unit>();
            Transform t = targetList[i].gameObject.GetComponent<Transform>();
            IDamagable x = targetList[i].gameObject.GetComponent<IDamagable>();



            if (t != null && x != null)
            {
                if (isValidTarget(x))
                {
                    targetPOS = t;
                    // if (unit.name == "Player Unit 1")
                    // {
                    //     Debug.Log("CC: x = " + x);
                    //     Debug.Log(unit.name + " Unit - findTarget: Target found");
                    // }
                    hasTarget = true;
                    currentTarget = t;
                    currentDamagable = x;
                    currentUnitTarget = u;
                    break;
                }
            }
        }
        return;
    }

    private void isTargetAlive(IDamagable x)
    {
        // if (unit.name == "Player Unit 1")
        // {
        //     Debug.Log("isTargetAlive = " + x);

        // }
        if (!x.getIsAlive() || x == null)
        {
            hasTarget = false;
        }
    }

    private void attackHandler(Transform t, IDamagable x, Unit u)
    {
        isTargetAlive(x);
        if (t != null && x != null)
        {

            
            float dist;

            if (x.getIsBase() || u.getUnitType() == UnitEnum.Tank)
            {
                Collider2D collider = t.GetComponent<Collider2D>();
                Vector2 closestpoint = collider.ClosestPoint(transform.position);

                dist = Vector2.Distance(transform.position, closestpoint);
                // Debug.Log("dist = " + dist);
                Debug.Log("atackHandler: base distance triggered");
            }
            else
            {
                dist = Vector2.Distance(transform.position, t.transform.position);
                Debug.Log("atackHandler: soldier distance triggered");
            }


            if (dist > unitAtkRng)
            {
                // if (unit.name == "Player Unit 1")
                // {
                Debug.Log(unit.name + " target = " + currentTarget);
                Debug.Log("tardget too far!");
                // }
                return;
            }
            else
            {
                if (u != null)
                {
                    counterDmg = unitCounterSystem(baseDmg, u);
                }
                isAttacking = true;
                // if (unit.name == "Player Unit 1")
                // {
                Debug.Log(unit.name + " Unit - attackHandler: in attack range, starting attack");
                // }
                StartCoroutine(AttackTarget(x, dist, t));
                return;
            }
        }


    }


    IEnumerator AttackTarget(IDamagable target, float dist, Transform t)
    {
        // Debug.Log("AttackTarget() called");
        while (target != null)
        {
            if (!isValidTarget(target))
            {
                // Debug.Log("Unit: target dead, start moving");
                CState = CombatStateEnum.Moving;
                // if (unit.name == "Player Unit 1")
                // {
                // Debug.Log(unit.name + " Unit - atk coroutine: !hasTarget - CState = " + CState);
                // }
                hasTarget = false;
                isAttacking = false;
                targetPOS = null;
                currentTarget = null;
                currentDamagable = null;
                currentUnitTarget = null;
                counterDmg = baseDmg;
                break;

            }
            else if (dist > unitAtkRng)
            {
                // if (unit.name == "Player Unit 1")
                // {
                // Debug.Log(unit.name + " out of range exiting AttackTarget");
                // }
                break;
            }
            else if (isValidTarget(target))
            {
                CState = CombatStateEnum.Fighting;
                // if (unit.name == "Player Unit 1")
                // {
                // Debug.Log(unit.name + "Unit - atk coroutine: attacking - CState = " + CState);
                // }
                if (unit.getUnitType() == UnitEnum.Archer)
                {
                    Debug.Log("archer attack heard");
                    Quaternion SpawnRotation = unit.transform.rotation;
                    var newArrow = Instantiate(arrowPrefab, unit.GetComponent<Transform>().position, SpawnRotation);
                    var projectileScript = newArrow.GetComponent<Projectile>();
                    projectileScript.Initialize(arrowPrefab, t, target, counterDmg);
                }
                else
                {
                    target.TakeDamage(counterDmg);
                }
                yield return new WaitForSeconds(unit.getAtkSpd());
            }
        }
    }

}
