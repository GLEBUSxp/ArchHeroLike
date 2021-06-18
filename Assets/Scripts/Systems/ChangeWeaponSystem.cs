using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class ChangeWeaponSystem : MonoBehaviour, IChangeWeaponHandler
{
    [SerializeField]
    private ChangeWeaponButton button;
 
    public WeaponBase.WeaponType currentWeaponType;
    public GameObject currentWeapon;

    public GameObject[] weapons;
    public Dictionary<WeaponBase.WeaponType, GameObject> weaponsTypes;


    void Start()
    {
        weaponsTypes = new Dictionary<WeaponBase.WeaponType, GameObject>();
        EventBus.Subscribe(this);

        foreach (GameObject weapon in weapons)
        {
            WeaponBase.WeaponType type = weapon.GetComponent<WeaponBase>().type;
            weaponsTypes.Add(type, weapon);
            weaponsTypes[type].SetActive(false);
        }

        currentWeapon = weaponsTypes[WeaponBase.WeaponType.Pistol];
        currentWeapon.SetActive(true);
    }

    public void HandleChangeWeapon(string newWeaponType)
    {
        if (newWeaponType != currentWeaponType.ToString())
        {
            WeaponBase.WeaponType newType = (WeaponBase.WeaponType)Enum.Parse(typeof(WeaponBase.WeaponType), newWeaponType);
            SetWeapon(newType);
            print($"Weapon changed to {newType}");
        }
        else
            print($"{newWeaponType.ToString()} is allready active");
    }

    void SetWeapon(WeaponBase.WeaponType newWeaponType)
    {
        currentWeapon.SetActive(false);

        currentWeaponType = newWeaponType;
        currentWeapon = weaponsTypes[newWeaponType];
        currentWeapon.SetActive(true);
        currentWeapon.GetComponent<WeaponBase>().mayAttack = true;
    }
}
