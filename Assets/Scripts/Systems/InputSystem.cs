using UnityEngine;
using EventBusSystem;

public class InputSystem : MonoBehaviour
{
    [SerializeField]
    float verticalAxis;

    [SerializeField]
    float horizontalAxis;
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        EventBus.RaiseEvent<IInputHandler>(h => h.HandleInput(horizontalAxis, verticalAxis));

    }
}
