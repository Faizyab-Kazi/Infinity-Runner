using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    public GameObject[] rooftops;
    public float zSpawn;
    public float tileLength = 11;
    public Transform playerTransform;
    public int noOfTiles = 0;
    private List<GameObject> activeRoofs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnRoof(0);
        SpawnRoof(0);
    }

    // Update is called once per frame
    void Update()
    {
        //int currentIndex;
        if (playerTransform.position.z > zSpawn - (tileLength)) 
        {
            SpawnRoof(0);
            DeleteRoof();
        }
        
    }

    private void SpawnRoof(int roofIndex)
    {
        GameObject current = Instantiate(rooftops[roofIndex], transform.forward * zSpawn, transform.rotation);
        zSpawn += tileLength;
        activeRoofs.Add(current);
    }
    private void DeleteRoof()
    {
        Destroy(activeRoofs[0]);
        activeRoofs.RemoveAt(0);
    }


}
