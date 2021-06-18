using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : WeaponBase
{
    public int bulletsInRow;
    public float fireRate;


    public override IEnumerator Attack()
    {
        if (!mayAttack)
            yield break;
        else
        {
            mayAttack = false;
            for (int i=0; i< bulletsInRow; i++) {

                LaunchBullet();

                yield return new WaitForSeconds(fireRate);
            }

            yield return new WaitForSeconds(attackSpeed);
            mayAttack = true;
            if (attack)
                StartCoroutine(Attack());
            yield break;
        }
    }

    public override void LaunchBullet()
    {
        if (bulets.Count <= bulletsInRow)
            ReloadBulets(maxBuletsCount);

        base.LaunchBullet();

    }
}
