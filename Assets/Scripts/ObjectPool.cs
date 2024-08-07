using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject[] objectPrefabs;
    private List<GameObject> pooledObjects;
    void Awake() {
        pooledObjects = new();
    }

    public GameObject GetObject(string type)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            GameObject gameObject = pooledObjects[i];
            if (gameObject.name == type && !gameObject.activeInHierarchy) {
                gameObject.SetActive(true);
                return gameObject;
            }
        }
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            if (objectPrefabs[i].name == type)
            {
                GameObject newObject = Instantiate(objectPrefabs[i]);
                newObject.name = type;
                pooledObjects.Add(newObject);
                return newObject;
            }
        }
        return null;
    }

    public void ReleaseObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
