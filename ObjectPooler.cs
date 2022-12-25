using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pools : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static Pools Instance;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> CustomerDictionary;

    private int current;


    private void Awake()
    {
        Instance = this;
        CustomerDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            CustomerDictionary.Add(pool.tag, objectPool);
        }
    }


    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!CustomerDictionary.ContainsKey(tag))
        {
            Debug.Log("Error " + tag + " is null");
            return null;
        }
        GameObject objectToSpawn = CustomerDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        
        CustomerDictionary[tag].Enqueue(objectToSpawn);
        
        return objectToSpawn;
    }
}

