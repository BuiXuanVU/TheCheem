using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheemCollider : MonoBehaviour
{
    [SerializeField] private AudioSource bonk;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Bong")
        {
            bonk.Play();
            HeatManager.instance.MinusHeart();
        }    
    }
}
