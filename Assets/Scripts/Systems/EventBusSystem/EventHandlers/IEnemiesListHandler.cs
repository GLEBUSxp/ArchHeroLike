using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public interface IEnemiesListHandler : IGlobalSubscriber
{
    void AddEnemyToList(GameObject enemy);
    void RemoveEnemyFromList(GameObject enemy);
}
