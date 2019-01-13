using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public GameObject[] itemPrefabs;
    public GameObject cannonItem, dragon, coinChain;

    private Transform playerTransform;
    private float spawnZ = -4.0f;
    private float roadLength = 4.0f;
    private float safeZone = 30.0f;
    private int amtRoadOnScreen = 20;
    private int lastPrefabIndex = 0;
    private int count = 0;

    private List<GameObject> activeItem;

    // Use this for initialization
    void Start()
    {
        activeItem = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amtRoadOnScreen; i++)
        {
            if (i < 10)
                makeItem(0);
            else
                makeItem();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().HaveCannon)
        {
            roadLength = 12.0f;
            safeZone = 20.0f;
            if (playerTransform.position.z - safeZone > (spawnZ - amtRoadOnScreen * roadLength))
            {
                if (count == 3)
                {
                    makeItem(1);
                    count = 0;
                }
                else
                {
                    count++;
                    makeItem(2);
                }
                deleteItem();
            }
        }
        else
        {
            roadLength = 6.0f;
            safeZone = 30.0f;
            if (playerTransform.position.z - safeZone > (spawnZ - amtRoadOnScreen * roadLength))
            {
                makeItem();
                deleteItem();
            }
        }
    }

    public void makeCannonItem()
    {
        GameObject go = Instantiate(cannonItem) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = RandomTransformVector();
        spawnZ += roadLength;
        activeItem.Add(go);
    }

    private void makeItem(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
        {
            go = Instantiate(itemPrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else if (prefabIndex == 1)
        {
            go = Instantiate(dragon) as GameObject;
        }
        else if (prefabIndex == 2)
        {
            go = Instantiate(coinChain) as GameObject;
        }
        else
        {
            go = Instantiate(itemPrefabs[prefabIndex]) as GameObject;
        }

        go.transform.SetParent(transform);
        go.transform.position = RandomTransformVector();
        spawnZ += roadLength;
        activeItem.Add(go);
    }

    private Vector3 RandomTransformVector()
    {
        float randomX = 0,
            randomY = 1.14f,
            randomZ = spawnZ;
        switch (Random.Range(1, 6))
        {
            case 1: randomX = -4.2f; break;
            case 2: randomX = -2.4f; break;
            case 3: randomX = -0f; break;
            case 4: randomX = 2.4f; break;
            case 5: randomX = 4.2f; break;
        }
        
        return new Vector3(randomX, randomY, randomZ);
    }

    private void deleteItem()
    {
        Destroy(activeItem[0]);
        activeItem.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (itemPrefabs.Length <= 1)
            return 0;
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, itemPrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}