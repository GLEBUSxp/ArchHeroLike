using UnityEngine;
using UnityEngine.AI;

public class ZombieController : Enemy
{
    void Update()
    {
        MoveToTarget();
    }
}
