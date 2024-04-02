using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner: Spawner 
{
    [SerializeField] int crowdSpread;
    [SerializeField] int maxCrowd;

    PlayerBeacon playerBeacon;

    //Spawner Values
    int crowdSize;

    // Start is called before the first frame update
    void Start()
    {
        playerBeacon = GameObject.Find("Player").GetComponent<Player>().playerBeacon;
        entityPool = GeneratePoolEntities(entityPrefabs[0], 300);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SpawnEntity()
    {
        canSpawn = false;
        crowdSize = Random.Range(1, maxCrowd);
        Vector3 spawnCenter = GetRandomLoc();
        for (int i = 0; i < crowdSize; i++)
        {
            spawnCenter += new Vector3(Random.Range(-crowdSpread, crowdSpread), 0, Random.Range(-crowdSpread, crowdSpread));
            GameObject entity = RequestEntity(0, spawnCenter);
        }
    }

    protected override List<GameObject> GeneratePoolEntities(GameObject entity, int numOfEntities)
    {
        Debug.Log("Generating Pool");
        List<GameObject> entityPool = new List<GameObject>();
        PopulatePool(entityPool, entity, numOfEntities);
        return entityPool;
    }

    protected override void PopulatePool(List<GameObject> pool, GameObject entity, int numOfEntities)
    {
        for (int i = 0; i < numOfEntities; i++)
        {
            GameObject go = Instantiate(entity);
            go.transform.parent = this.transform;
            go.GetComponent<Enemy>().player = playerBeacon;
            go.SetActive(false);
            pool.Add(go);
        }
    }

    protected override GameObject RequestEntity(int entityID, Vector3 location) 
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
