using UnityEngine;
using System.Collections.Generic;
using EventBusSystem;


public class PlayerController : MonoBehaviour, IDamageDealer, IInputHandler, ITargetSeaker, ITakingDamageHandler, IDeathHandler, IEnemiesListHandler
{

    public int healthPoint;
    public int _damage;
    public int damage { get { return _damage; } set { value = _damage; } }
    public float _attackSpeed;
    public float attackSpeed { get { return _attackSpeed; } set { value = _attackSpeed; } }
    public float moveSpeed;

    public Transform target { get; set; }

    MoveSystem moveSystem;
 
    public List<GameObject> enemies;

    public bool attack { get; set; }

    void Start()
    {
        EventBus.Subscribe(this);

        moveSystem = GetComponent<MoveSystem>();

    }


    public void HandleInput(float horizontalAxis, float verticalAxis)
    {
        if (horizontalAxis == 0 && verticalAxis == 0)
        {
            PrepearToAttack();
        }

        else
        {
            attack = false;
            moveSystem.Move(new Vector3(horizontalAxis, 0, verticalAxis), moveSpeed);
        }
    }


    private void PrepearToAttack()
    {
        if (enemies.Count > 0) {
            target = FindTarget(enemies).transform;
            transform.LookAt(target);

            attack = true; 
        }
        else
            EventBus.RaiseEvent<INoLiveEnemyHandler>(h => h.NoLiveEnemyLeft());
    }

    private GameObject FindTarget(List<GameObject> enemies)
    {

        GameObject closestEnemy = null;
        
        float distance = Mathf.Infinity;
        Vector3 playerPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - playerPosition;
            float currDistance = diff.sqrMagnitude;

            if (currDistance < distance)
            {
                closestEnemy = enemy;
                distance = currDistance;
            }
        }

        return closestEnemy;
    }

    public void TakingDamageHandle(GameObject obj, int damage)
    {
        if (gameObject == obj)
        {
            healthPoint -= damage;
            Debug.Log($"{gameObject.name} taking {damage} damage. Health: {healthPoint}");
        }
    }

    public void DeathHandle(GameObject obj)
    {
        if(gameObject == obj)
            Debug.Log("Player is dead");
    }

    public void AddEnemyToList(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
}
