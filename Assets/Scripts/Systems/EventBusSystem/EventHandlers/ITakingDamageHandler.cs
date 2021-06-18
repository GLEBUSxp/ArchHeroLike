using UnityEngine;
using EventBusSystem;

public interface ITakingDamageHandler : IGlobalSubscriber
{
    void TakingDamageHandle(GameObject obj, int damage);
}
