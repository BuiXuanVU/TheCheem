using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCtrl : MonoBehaviour
{
    public bool isRotace;
    private void Update()
    {
        if(isRotace == true)
        {
            transform.Rotate(0, 0, 30);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position) * 1000);
            if(this.transform.name != "baseballPlace" && this.transform.name != "baseballBig1" && this.transform.name != "baseballBig2")
            {
                ObjectSpawner.instance.DespawnObject(this.transform);
            }    
        }
    }
}
