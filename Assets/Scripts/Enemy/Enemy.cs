using UnityEngine;
using UnityEngine.AI;
using EventBusSystem;

public abstract class Enemy : MonoBehaviour, ITakingDamageHandler, ITargetSeaker
{
    [SerializeField]
    internal int healthPoint;
    internal int damage;
    internal float attackSpeed;
    [SerializeField]
    internal float moveSpeed;
    internal float rotationSpeed;

    public NavMeshAgent agent;

    [SerializeField]
    public Transform target { get; set; }
    internal Vector3 targetDirection;

    internal enum MovementType
    {
        Flying,
        Grounded
    }
    internal MovementType movementType;

   

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        EventBus.Subscribe(this);
    }
    public Transform FindTargetPosition(Transform target)
    {
        Transform targetPosition = target;
        return targetPosition;
    }
    public Vector3 FindTargetDirection(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        direction.y = 0f;
        return direction;
    }

    public void MoveToTarget()
    {
        target = FindTargetPosition(target);
        agent.SetDestination(target.position);
    }

    public void TakingDamageHandle(GameObject obj, int damage)
    {
        if(gameObject == obj)
        {
            healthPoint -= damage;
            if (healthPoint == 0)
            {
                EventBus.RaiseEvent<IDeathHandler>(h => h.DeathHandle(gameObject));
            }
        }
    }

}
