using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDespawn : MonoBehaviour
{
    [SerializeField] private float dinstance = 30;
    private void Update()
    {
        if (Vector2.Distance(transform.position, Camera.main.transform.position) > dinstance)
        {
            ObjectSpawner.instance.DespawnObject(this.transform);
        }     
    }   
}
