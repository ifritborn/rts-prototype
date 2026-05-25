using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class CombatController : MonoBehaviour
{

    private CombatStateEnum CState;
    private Unit unit;
    private float unitAtkRng;
    private bool hasTarget;
    private bool isInitialized;
    private Transform targetPOS;


    public Transform getTargetPOS()
    {
        return targetPOS;
    }

    // ----------------------------------------------------------------------------------------------------------------
    public void Initialize(Unit unit, float unitAtkRng)
    {
        this.unit = unit;
        this.unitAtkRng = unitAtkRng;
        CState = CombatStateEnum.Moving;
        hasTarget = false;
        isInitialized = true;

    }


    private void Update()
    {
        if (!hasTarget & isInitialized)
        {
            findTarget();
        }
    }

    // ----------------------------------------------------------------------------------------------------------------



    public CombatStateEnum getCState()
    {
        return CState;
    }

    private void findTarget()
    {

        Collider2D[] targetList = Physics2D.OverlapCircleAll(unit.transform.position, unitAtkRng);
        for (int i = 0; i < targetList.Length; i++)
        {
            Transform t = targetList[i].gameObject.GetComponent<Transform>();
            IDamagable x = targetList[i].gameObject.GetComponent<IDamagable>();
            Debug.Log("CC: x = " + x);
            if (t != null && x != null)
            {
                if (isValidTarget(targetList[i].gameObject.GetComponent<IDamagable>()))
                {
                    targetPOS = t;
                    hasTarget = true;
                    break;
                }
            }
        }


    }



    private bool isValidTarget(IDamagable target)
    {
        bool isValid = false;

        if (target.getTeamID() != unit.getTeamID() && target.getIsAlive() == true)
        {
            isValid = true;
            // Debug.Log("Unit: is this alive? = " + target.getIsAlive());
        }

        return isValid;
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        IDamagable t = target.gameObject.GetComponent<IDamagable>();
        // Debug.Log("Unit: coliding with: " + target.gameObject.name);

        if (t != null)
        {
            bool CanAttack = isValidTarget(t);
            // Debug.Log("Unit: can I attack? " + CanAttack);

            if (CanAttack)
            {
                StartCoroutine(AttackTarget(t));
            }
        }
    }


    IEnumerator AttackTarget(IDamagable target)
    {
        while (target != null)
        {
            if (!isValidTarget(target))
            {
                // Debug.Log("Unit: target dead, start moving");
                CState = CombatStateEnum.Moving;
                break;

            }
            else if (isValidTarget(target))
            {
                Debug.Log("Unit: target alive, stop moving");
                CState = CombatStateEnum.Fighting;
                target.TakeDamage(unit.getDmg());
                yield return new WaitForSeconds(unit.getAtkSpd());
            }

        }
    }


}
