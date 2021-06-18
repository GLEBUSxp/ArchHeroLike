using UnityEngine;

public class MoveSystem : MonoBehaviour
{    public void Move(Vector3 moveDirection, float moveSpeed)
    {
        moveDirection = new Vector3(moveDirection.x * moveSpeed * Time.deltaTime, 0, moveDirection.z * moveSpeed * Time.deltaTime);
        transform.position += moveDirection;
        transform.rotation = Quaternion.LookRotation(moveDirection);
    }

    public void LookAt(Transform target, float rotationSpeed)
    {
        Vector3 rotateDirection = target.position - transform.position;
        rotateDirection.Normalize();
        rotateDirection.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rotateDirection), rotationSpeed);
    }

}
