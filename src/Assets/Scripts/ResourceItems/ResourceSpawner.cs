using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : Spawner 
{
    public override void SpawnEntity()
    {
        canSpawn = false;
        
        
        for (int i = 0; i < poolDict.Count; i++)
        {
            var entityPool = poolDict[i];
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
}
