using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// controls the max spawn, spawn object, and maxspawn flags of spawnsources
public class SpawnSourceBehaviour : MonoBehaviour
{

    public GameObject sourceItem;
    public int spawnLimit = 3;
    public List<GameObject> spawnedItems = new List<GameObject> {};
    public bool maxSpawnFlag = false;
    int tick = 0;
    int fixedTick = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tick += 1;

        if (tick % 4 == 0)
        {
            // CheckSpawnCount(); <-- too hard to track across abstracted components, removing for simplicity
        }

    }

    private void FixedUpdate()
    {
        fixedTick += 1;


    }

    public void AddSpawn (GameObject item)
    {
        spawnedItems.Add(item);
    }

    public void RemoveSpawn (GameObject item)
    {
        spawnedItems.Remove(item);
    }

    void CheckSpawnCount ()
    {
        if(spawnedItems.Count >= spawnLimit)
        {
            maxSpawnFlag = true;
        }
        else
        {
            maxSpawnFlag = false;
        }
    }

}
