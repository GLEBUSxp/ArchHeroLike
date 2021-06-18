using EventBusSystem;

public interface IChangeWeaponHandler : IGlobalSubscriber
{
    void HandleChangeWeapon(string newWeaponType);
}
