using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : Spawner 
{
    void Start()
    {
        entityPool = GeneratePoolEntities(entityPrefabs[0], 20);
    }

    public override void SpawnEntity()
    {
        canSpawn = false;

        foreach (GameObject go in entityPool)
        {
            if (go.activeInHierarchy == false)
            {
                go.transform.position = GetRandomLoc();
                go.SetActive(true);
            }
        }
    }
}
