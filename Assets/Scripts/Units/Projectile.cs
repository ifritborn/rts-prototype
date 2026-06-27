using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float arrowSpeed;
    private Transform targetPOS;
    private Projectile prefab;
    private IDamagable target;
    private float counterDmg;
    public void Initialize(Projectile prefab, Transform targetPOS, IDamagable target, float counterDmg)
    {
        this.targetPOS = targetPOS;
        this.prefab = prefab;
        this.target = target;
        this.counterDmg = counterDmg;

    }

    // Update is called once per frame
    void Update()
    {
        if (targetPOS == null)
        {
            Destroy(gameObject);
            return;
        }
        else if (Vector3.Distance(targetPOS.position, transform.position) > .25)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPOS.position, arrowSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("archer dmg here");
            target.TakeDamage(counterDmg);
            Destroy(gameObject);
        }

    }
}
