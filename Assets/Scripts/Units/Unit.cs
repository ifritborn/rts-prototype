using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private Transform targetPOS;

    private bool isMoving;

    private bool isAttacking;
    private float unitMvSpd = 1f;
    private float unitAtkSpd = 1.5f;
    

    public void Initialize(Transform pos, Color spriteColor)
    {
        this.targetPOS = pos;
        this.GetComponent<SpriteRenderer>().color = spriteColor;
    }

    void Start()
    {
        isMoving = true;
        isAttacking = false;
    }

    void Update()
    {
        move();
    }


    void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPOS.position, unitMvSpd * Time.deltaTime);
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





}
