using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPoolManager : MonoBehaviour
{
    public GameObject buletPref;
    public int maxBuletsInPool;
    public List<GameObject> buletsPool;

    void Start()
    {
        ReloadPool();
    }

    public GameObject PopBulletFromPool()
    {
        GameObject bulet = buletsPool[buletsPool.Count - 1];
        buletsPool.Remove(bulet);
        return bulet;
    }

    public void PushBulletToPool(GameObject bulet)
    {
        bulet.SetActive(false);
        bulet.transform.position = gameObject.transform.position;
        buletsPool.Add(bulet);
    }

    private void ReloadPool()
    {
        for (int i = 0; i <= maxBuletsInPool; i++)
        {
            GameObject newBullet = Instantiate(buletPref, gameObject.transform);
            newBullet.SetActive(false);
            buletsPool.Add(newBullet);
        }
    }

}
