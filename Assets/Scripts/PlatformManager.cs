using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewBehaviourScript : MonoBehaviour
{
    
    public GameObject[] rooftops;
    public float zSpawn;
    public float tileLength = 11;
    public float maxTileLength;
    public Transform playerTransform;
    public int noOfTiles = 3;
    private List<GameObject> activeRoofs = new List<GameObject>();
    public int heightThresholdMin, heightThresholdMax;
    public float SpawnDistanceFromPlayer;
    private float prevPlayerPos;
    private float currPlayerPos;
    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        
        SpawnRoof(0);
        SpawnRoof(0);

        prevPlayerPos = playerTransform.position.z;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        currPlayerPos = playerTransform.position.z;
        //int currentIndex;
        if (playerTransform.position.z > zSpawn - (tileLength)) 
        {
            SpawnRoof(Random.Range(0,noOfTiles-1));
            DeleteRoof();
        }
        //Velocity
        if (currPlayerPos > prevPlayerPos) {
            velocity = currPlayerPos - prevPlayerPos;
            velocity /= Time.deltaTime;
            prevPlayerPos = playerTransform.position.z;
        }
        //Using Velocity to influence spawn Position
        if (tileLength < maxTileLength) {
            tileLength += (velocity * Time.deltaTime);
        }

        //Debug.Log("Velocity: "+velocity);

        
    }

    private void SpawnRoof(int roofIndex)
    {
        //GameObject current = Instantiate(rooftops[roofIndex], transform.forward * zSpawn, transform.rotation);
        GameObject current = Instantiate(rooftops[roofIndex], new Vector3(0, Random.Range( heightThresholdMin, heightThresholdMax),1 * zSpawn), transform.rotation);
        //GameObject current = Instantiate(rooftops[roofIndex], new Vector3(0, Random.Range(heightThresholdMin, heightThresholdMax), 1 * (zSpawn + spawnDistFromPlayer)), transform.rotation);
        zSpawn += tileLength;
        activeRoofs.Add(current);
    }
    private void DeleteRoof()
    {
        Destroy(activeRoofs[0]);
        activeRoofs.RemoveAt(0);
    }


}
