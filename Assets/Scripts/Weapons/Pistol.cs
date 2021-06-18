using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponBase
{
    public override IEnumerator Attack()
    {
        if (!mayAttack)
            yield break;
        else
        {
            mayAttack = false;

            //print("Pistol Pew " + Time.time);
            LaunchBullet();

            yield return new WaitForSeconds(attackSpeed);
            mayAttack = true;

            if (attack)
                StartCoroutine(Attack());
            yield break;
        }
    }

    public override void LaunchBullet()
    {
        if (bulets.Count <= 0)
            ReloadBulets(maxBuletsCount);

        base.LaunchBullet();
    }

}
