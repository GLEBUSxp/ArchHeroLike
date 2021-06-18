using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    internal IDamageDealer damageDealer;

    public Transform bulletSpawner;
    public BulletsPoolManager bulletsPoolManager;
    public int maxBuletsCount;
    public List<GameObject> bulets;

    public float buletSpeed;

    [SerializeField]
    internal int damage;
    [SerializeField]
    internal float attackSpeed;

    public ITargetSeaker targetSeaker;

    [SerializeField]
    internal bool mayAttack;

    public enum WeaponType
    {
        Pistol,
        Rifle
    }
    public WeaponType type;

    public bool attack { get { return damageDealer.attack; } set { value = damageDealer.attack;} }

    void Start()
    {
        damageDealer = GetComponentInParent<IDamageDealer>();
        attackSpeed = damageDealer.attackSpeed;
        damage = damageDealer.damage;

        targetSeaker = GetComponentInParent<ITargetSeaker>();

        bulets = new List<GameObject>();
        ReloadBulets(maxBuletsCount);
    }

    private void OnEnable()
    {
        mayAttack = true;
    }

    void Update()
    {
        if (attack) StartCoroutine(Attack());
    }

    public virtual IEnumerator Attack()
    {
        yield return null;
    }

    
    public virtual void LaunchBullet()
    {
        GameObject launchingBullet = bulets[bulets.Count - 1];
        
        bulets.Remove(launchingBullet);
        launchingBullet.transform.position = bulletSpawner.position;
        
        BulletController bulletController = launchingBullet.GetComponent<BulletController>();
        bulletController.LaunchBullet(targetSeaker.target, buletSpeed, damage);
        
        launchingBullet.SetActive(true);

    }

    public void ReloadBulets(int rounds)
    {
        for (int i=0; i<=rounds; i++)
        {
            bulets.Add(bulletsPoolManager.PopBulletFromPool());
        }
    }
}
