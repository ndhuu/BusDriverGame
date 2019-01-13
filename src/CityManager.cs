using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour {
    public GameObject[] cityPrefabs;

    private Transform playerTransform;
    private float spawnZ = -20.0f;
    private float roadLength = 60.0f;
    private float safeZone =20.0f;
    private int amtRoadOnScreen = 2;

    private List<GameObject> activeRoad;

    // Use this for initialization
    void Start()
    {
        activeRoad = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amtRoadOnScreen; i++)
        {
            makeRoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - amtRoadOnScreen * roadLength))
        {
            makeRoad();
            deleteRoad();
        }
    }

    private void makeRoad()
    {
        GameObject go;
        go = Instantiate(cityPrefabs[Random.Range(0,2)]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += roadLength;
        activeRoad.Add(go);
    }

    private void deleteRoad()
    {
        Destroy(activeRoad[0]);
        activeRoad.RemoveAt(0);
    }
}
