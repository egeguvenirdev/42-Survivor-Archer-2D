using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPooler : MonoSingleton<ObjectPooler>
{
    [SerializeField] private List<ObjectPooledItem> itemsToPool;
    [SerializeField] private List<ObjectPooledItem> textsToPool;
    [SerializeField] private GameObject pooledObjectHolder;

    private List<GameObject> pooledObjects;
    private List<SlideText> pooledText;

    private void Awake()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPooledItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.SetParent(pooledObjectHolder.transform);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

        pooledText = new List<SlideText>();
        foreach (ObjectPooledItem item in textsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.SetParent(pooledObjectHolder.transform);
                obj.SetActive(false);
                pooledText.Add(obj.GetComponent<SlideText>());
            }
        }
    }

    public void DeInit()
    {
        for (int i = 0; i < pooledText.Count; i++)
        {
            pooledText[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObjects[i].gameObject.SetActive(false);
        }
    }

    public GameObject GetPooledObjectWithTag(string tag)
    {
        for (int i = pooledObjects.Count - 1; i > -1; i--)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        //pooledObjects.First(o => o.activeInHierarchy && o.tag == tag);
        foreach (ObjectPooledItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    obj.transform.SetParent(pooledObjectHolder.transform);
                    return obj;
                }
            }
        }
        return null;
    }

    public SlideText GetPooledText()
    {
        for (int i = pooledText.Count - 1; i > -1; i--)
        {
            if (!pooledText[i].gameObject.activeInHierarchy)
            {
                return pooledText[i];
            }
        }

        foreach (ObjectPooledItem item in textsToPool)
        {
            if (item.shouldExpand)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                SlideText slideText = obj.GetComponent<SlideText>();
                obj.SetActive(false);
                pooledText.Add(slideText);
                obj.transform.SetParent(pooledObjectHolder.transform);
                return slideText;
            }
        }
        return null;
    }
}