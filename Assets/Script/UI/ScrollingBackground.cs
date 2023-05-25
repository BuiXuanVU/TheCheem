using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : ComponentBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private MeshRenderer backGround;
    [SerializeField] private Material[] groundMaterial;
    private float lastTime;
    int i = 0;
    Vector2 offset;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(backGround == null)
            backGround = GetComponent<MeshRenderer>();
    }
    protected override void Start()
    {
        base.Start();
        offset = new Vector2(speed,0);
    }
    private void Update()
    {
        if(Time.time - lastTime >= 20 )
        {
            lastTime = Time.time;
            if(i == 0) i = 1;
            else i = 0;
            backGround.material = groundMaterial[i];
        }
        backGround.sharedMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
