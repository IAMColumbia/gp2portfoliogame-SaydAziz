using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject[] entityPrefabs;
    //[SerializeField] protected List<GameObject> entityPool;
    [SerializeField] protected int entityAmount;

    protected Dictionary<int , List<GameObject>> poolDict = new Dictionary<int , List<GameObject>>();

    [SerializeField] protected Collider spawnPerimeter;

    [SerializeField] protected float spawnInterval;

    protected bool canSpawn;

    LayerMask enviroLayer;

    protected virtual void Start()
    {
        enviroLayer = LayerMask.GetMask("Environment");
        for (int i = 0; i < entityPrefabs.Length; i++)
        {
            poolDict.Add(i, GeneratePoolEntities(entityPrefabs[i], 50));
        }
    }

    public abstract void SpawnEntity();
    protected virtual Vector3 GetRandomLoc()
    {
        bool validLoc = false;
        Vector3 randLoc = new Vector3(0, 0, 0);
        Vector3 colliderSize = spawnPerimeter.bounds.size;

        while (!validLoc)
        {
            float randX = Random.Range(-colliderSize.x / 2f, colliderSize.x / 2f);
            float randZ = Random.Range(-colliderSize.z / 2f, colliderSize.z / 2f);

            randLoc = new Vector3(randX, 0, randZ);

            Collider[] Overlaps = Physics.OverlapBox(randLoc, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, enviroLayer);
            if (Overlaps.Length == 0 )
            {
                validLoc = true;
            }
        }




        return spawnPerimeter.transform.position + randLoc;
    }

    protected virtual List<GameObject> GeneratePoolEntities(GameObject entity, int numOfEntities)
    {
        Debug.Log("Generating Pool");
        List<GameObject> entityPool = new List<GameObject>();
        PopulatePool(entityPool, entity, numOfEntities);
        return entityPool;
    }

    protected virtual void PopulatePool(List<GameObject> pool, GameObject entity, int numOfEntities)
    {
        for (int i = 0; i < numOfEntities; i++)
        {
            GameObject go = Instantiate(entity);
            go.transform.parent = this.transform;
            go.SetActive(false);
            pool.Add(go);
        }
    }

    protected virtual GameObject RequestEntity(Vector3 location, List<GameObject> entityPool) 
    {
        foreach (GameObject go in entityPool)
        {
            if (go.activeInHierarchy == false)
            {
                go.transform.position = location;
                go.SetActive(true);
                return go;
            }
        }

        PopulatePool(entityPool, entityPool[0], 50);


        //GameObject newEntity = Instantiate(entityPrefabs[entityID]);
        //newEntity.transform.parent = this.transform;
        //entityPool.Add(newEntity);
        GameObject newEntity = entityPool[entityPool.Count - 1];
        newEntity.SetActive(true);
        
        return newEntity;
    }

}
