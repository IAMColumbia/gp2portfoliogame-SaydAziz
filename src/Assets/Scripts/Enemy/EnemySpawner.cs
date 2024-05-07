using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner: Spawner 
{
    //[SerializeField] protected List<GameObject> reacherPool;
    //[SerializeField] protected List<GameObject> tankerPool;

    [SerializeField] int crowdSpread;
    [SerializeField] int maxCrowd;

    PlayerBeacon playerBeacon;

    //Spawner Values
    int crowdSize;
    public bool willSpawn;
    public float spawnCredits;

    // Start is called before the first frame update
    protected override void Start()
    {
        willSpawn = true;
        playerBeacon = GameObject.Find("Player").GetComponent<Player>().playerBeacon;
        //entityPool = GeneratePoolEntities(entityPrefabs[0], 100);
        //reacherPool = GeneratePoolEntities(entityPrefabs[1], 50);
        //tankerPool = GeneratePoolEntities(entityPrefabs[2], 10);
        spawnCredits = 15;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private List<GameObject> SelectPool()
    {
        Debug.Log("Selecting pool");

        foreach(var pool in poolDict)
        {
            if (spawnCredits - pool.Value[0].GetComponent<Enemy>().GetSpawnCost() >= 0)
            {
                spawnCredits -= pool.Value[0].GetComponent<Enemy>().GetSpawnCost();
                return pool.Value;
            }
        }

        //if (spawnCredits - 100 >= 0)
        //{
        //    spawnCredits -= 100;
        //    return tankerPool;
        //}
        //else if (spawnCredits - 20 >= 0)
        //{
        //    spawnCredits -= 15;
        //    return reacherPool;
        //}
        //else if (spawnCredits - 0.5f >= 0)
        //{
        //    spawnCredits -= 0.5f;
        //    return entityPool;
        //}

        willSpawn = false;
        return null;
    }

    public override void SpawnEntity()
    {
        canSpawn = false;
        crowdSize = Random.Range(1, maxCrowd);
        Vector3 spawnCenter = GetRandomLoc();
        for (int i = 0; i < crowdSize; i++)
        {
            spawnCenter += new Vector3(Random.Range(-crowdSpread, crowdSpread), 0, Random.Range(-crowdSpread, crowdSpread));
            GameObject entity = RequestEntity(spawnCenter, SelectPool());
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

    protected override GameObject RequestEntity(Vector3 location, List<GameObject> selectedPool) 
    {

        foreach (GameObject go in selectedPool)
        {
            if (go.activeInHierarchy == false)
            {
                go.transform.position = location;
                go.SetActive(true);
                return go;
            }
        }

        PopulatePool(selectedPool, selectedPool[0], 50);


        //GameObject newEntity = Instantiate(entityPrefabs[entityID]);
        //newEntity.transform.parent = this.transform;
        //entityPool.Add(newEntity);
        GameObject newEntity = selectedPool[selectedPool.Count - 1];
        newEntity.SetActive(true);
        
        return newEntity;
    }
}
