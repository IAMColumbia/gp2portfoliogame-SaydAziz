using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketManager : MonoBehaviour
{
    [SerializeField] GameObject[] trinketPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Place(int trinketIndex, Vector3 location)
    {
        Instantiate(trinketPrefabs[trinketIndex], location, UnityEngine.Quaternion.identity);
    }
}
