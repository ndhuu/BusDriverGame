using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager1 : MonoBehaviour {
    public GameObject roadPrefab;

    private Transform playerTransform;
    private float spawnZ = -12.0f;
    private float roadLength = 12.0f;
    private float safeZone = 15.0f;
    private int amtRoadOnScreen = 7;

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

    private void makeRoad(int prefabIndex = -1)
    {
        GameObject go;
        go = Instantiate(roadPrefab) as GameObject;
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
