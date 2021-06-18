using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EventBusSystem;

public class GameSceneManager : MonoBehaviour, IAllEnemiesKilled, IExitDoorEnterHandler
{
    public GameObject exitDoor;
    private void Start()
    {
        EventBus.Subscribe(this);
        exitDoor.SetActive(false);
    }

    public void AllEnemiesKilled()
    {
        Debug.Log("All Enemies killed");
        exitDoor.SetActive(true);
    }
    
    public void EnterExitDoorHandle()
    {
        Debug.Log("Win");
        SceneManager.LoadScene(0);
    }
}
