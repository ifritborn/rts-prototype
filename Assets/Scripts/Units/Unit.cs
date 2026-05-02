using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    
    [SerializeField] private Building enemyBase;
    private Vector3 targetPOS; 

    private bool isMoving;

    private bool isAttacking;
    private float unitMvSpd = 1f;
    private float unitAtkSpd = 1.5f;
    private float mtime;

    void Start()
    {
        setEnemyBasePOS();
        isMoving = true;
        isAttacking = false;
    }



    void setEnemyBasePOS()
    {
        
        Debug.Log("Unit: enemyBase = " + enemyBase);
        if(enemyBase != null)
        {
            this.targetPOS = enemyBase.POS;
            Debug.Log("Unit: target base at pos: " + targetPOS);
        }
        else
        {
            Debug.Log("Unit: Bulding script not found");
        }
    }

    void move()
    {
        Vector3 moveDelta = Vector3.right * unitMvSpd * mtime;
        Vector3 newPos = transform.position + moveDelta;
        transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
    }

    void getTarget()
    {
        
    }

    IEnumerator attack(Building target)
    {
        isAttacking = true;

        Debug.Log("Unit: attacking: " + target.gameObject.name);

        while (target != null)
        {
            Debug.Log("Unit: swinging on target");
            target.takeDamage(100);

            yield return new WaitForSeconds(unitAtkSpd);
        }
        Debug.Log("Unit: attack while loop over");
        isAttacking = false;
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        Debug.Log("Unit: coliding with: " + target.gameObject.name);
        if (isMoving)
        {
            isMoving = false;
            unitMvSpd = 0;
        }

        StartCoroutine(attack(target.gameObject.GetComponent<Building>()));
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




    void Update()
    {
        mtime = Time.deltaTime;
        move();
    }
}
