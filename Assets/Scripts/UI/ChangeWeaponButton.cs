using UnityEngine;
using EventBusSystem;

public class ChangeWeaponButton : MonoBehaviour
{
    public void ChangeWeaponTo(string typeStr)
    {
        EventBus.RaiseEvent<IChangeWeaponHandler>(h => h.HandleChangeWeapon(typeStr));
    }
}
