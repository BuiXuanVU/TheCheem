using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : ComponentBehaviour
{
    public static ObjectSpawner instance;
    [SerializeField] private List<Transform> poolObjects = new List<Transform>();
    [SerializeField] private Transform Bong;
    [SerializeField] private Transform Holder;
    [SerializeField] private Transform firePlace;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (firePlace == null)
        {
            firePlace = transform.GetChild(0);
        }
    }
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
    private Transform SpawnNewObj()
    {
        Transform objectSpawn = Instantiate(Bong, firePlace.position, Quaternion.Euler(0,0,135));
        objectSpawn.transform.SetParent(Holder);
        return objectSpawn;
    }
    public Transform GetObjFromPool()
    {
        string itemTag = "Bong";
        foreach (Transform obj in poolObjects)
        {
            if (obj.tag == itemTag)
            {
                refreshObject(obj);
                poolObjects.Remove(obj);
                return obj;
            }
        }
        return SpawnNewObj();
    }
    private Transform refreshObject(Transform poolObj)
    {
        poolObj.position = firePlace.position;
        poolObj.rotation = Quaternion.Euler(0, 0, 135);
        return poolObj;
    }
    public void DespawnObject(Transform despawnObject)
    {
        despawnObject.GetComponent<BatCtrl>().isRotace = false;
        despawnObject.gameObject.SetActive(false);
        poolObjects.Add(despawnObject);
    }
}
