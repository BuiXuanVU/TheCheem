using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatManager : ComponentBehaviour
{
    public static HeatManager instance;
    [SerializeField] private int heart = 3;
    [SerializeField] private List<Transform> allHeart = new List<Transform>();
    [SerializeField] private List<Transform> allBrHearts = new List<Transform>();
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
    private void Update()
    {
        if(heart==0)
        {
            gameObject.SetActive(false);
            UIManager.instance.GameOver();
        }    
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(allHeart.Count < 3)
        {
            for(int i = 0; i < 3; i++)
            {
                allHeart.Add(transform.GetChild(i));
            }
        }
        if(allBrHearts.Count < 3)
        {
            for (int i = 3; i < 6; i++)
            {
                allBrHearts.Add(transform.GetChild(i));
            }
        }    
    }
    public void MinusHeart()
    {
        if(heart>0)
        {
            heart--;
        }    
        foreach(Transform heart in allHeart)
        {
            if(heart.gameObject.activeSelf == true)
            {
                heart.gameObject.SetActive(false);
                break;
            }    
        }
        foreach (Transform heart in allBrHearts)
        {
            if (heart.gameObject.activeSelf == false)
            {
                heart.gameObject.SetActive(true);
                break;
            }
        }
    }    
}
