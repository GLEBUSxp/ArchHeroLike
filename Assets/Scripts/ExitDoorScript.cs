using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class ExitDoorScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EventBus.RaiseEvent<IExitDoorEnterHandler>(h => h.EnterExitDoorHandle());
        }
    }
}
