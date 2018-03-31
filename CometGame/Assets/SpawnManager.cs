using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [Header("Spawners")]
    [SerializeField] GameObject leftSpawn; 
    [SerializeField] GameObject rightSpawn;
    [SerializeField] GameObject topSpawn;
    [SerializeField] GameObject botSpawn;
    [SerializeField] GameObject spawnObjects;

    List<GameObject> _spawnedObjectsList = new List<GameObject>();

    [Space]
    public int spawnLimit;


    // Use this for initialization
    void Start () {

        for (int i = 0; i < spawnLimit/2; i++)
        {
            GameObject spawnedObj = Instantiate(spawnObjects);
            _spawnedObjectsList.Add(spawnedObj);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
