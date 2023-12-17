using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> SetPooledObject(GameObject obj, int amountToPool)
    {
        List<GameObject> objects = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject objPool = Instantiate(obj);
            objPool.SetActive(false);
            objects.Add(objPool);
        }

        return objects;
    }

    public GameObject GetPooledObject(List<GameObject> pooledObjects)
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
