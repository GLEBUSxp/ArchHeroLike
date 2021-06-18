using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class BulletController : MonoBehaviour
{
    public BulletsPoolManager bulletsPoolManager;
    public int damage;
    public new Rigidbody rigidbody;
    [SerializeField]
    private float velosity;
    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float lifeTime;

    void Start()
    {    
        bulletsPoolManager = GameObject.Find("BulletsPoolManager").GetComponent<BulletsPoolManager>();
    }

    void OnEnable()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.velocity = direction * velosity;
        StartCoroutine(lifeTimer());
    }

    public void LaunchBullet(Transform target, float speed, int damage)
    {
        direction = target.position - transform.position;
        direction.Normalize();

        velosity = speed;
        this.damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }

        else
        {
            EventBus.RaiseEvent<ITakingDamageHandler>(h => h.TakingDamageHandle(collision.gameObject, damage));
            bulletsPoolManager.PushBulletToPool(gameObject);
        }
    }
    private IEnumerator lifeTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        bulletsPoolManager.PushBulletToPool(gameObject);
    }

}
