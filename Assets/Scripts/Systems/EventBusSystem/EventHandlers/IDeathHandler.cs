using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public interface IDeathHandler : IGlobalSubscriber
{
    void DeathHandle(GameObject obj);
}
