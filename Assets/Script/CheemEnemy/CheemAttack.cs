using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheemAttack : MonoBehaviour
{
    [SerializeField] private float fireRate = 1;
    private float lastTime;
    private float updateTime;
    private int normalHit = 0;
    private bool normalAttack = true;
    private void Update()
    {
        if(EnemySkill.instance.isStopNormalAttack == true)
        {
            return;
        }    
        if(Time.time - updateTime >= 40)
        {
            updateTime = Time.time;
            fireRate += 0.2f;
        }    
    }
    private void FixedUpdate()
    {
        if (EnemySkill.instance.isStopNormalAttack == true)
        {
            return;
        }
        if (Time.time - lastTime > 1 / fireRate)
        {
            lastTime = Time.time;
            if (normalAttack == true)
            {
                Attack();
            }
            normalHit++;
        }
        if (normalHit == 8)
        {
            normalAttack = false;
        }
        if (normalHit == 9)
        {
            ThreeAttach();
            normalHit = 10;
            normalAttack = true;
        }
        if(normalHit == 15)
        {
            EnemySkill.instance.isDash = true;
            EnemySkill.instance.isStopNormalAttack = true;
            normalHit = 16;
        }    
        if(normalHit == 20)
        {
            EnemySkill.instance.isStopNormalAttack = true;
            EnemySkill.instance.isPow = true;
            normalHit = 0;
        }    
    }
    private void Attack()
    {
        Rigidbody2D rigid = GetObject();
        if (normalHit == 3)
        {
            EnemySkill.instance.FireRotace(rigid);
            return;
        }
        EnemySkill.instance.Fire(rigid);
    }
    private void ThreeAttach()
    {
        List<Rigidbody2D> listBat = new List<Rigidbody2D>();
        for(int i = 0; i < 3; i++)
        {
            listBat.Add(GetObject());
        }
        EnemySkill.instance.ThreeDirection(listBat);
        return;
    }
    private Rigidbody2D GetObject()
    {
        Transform bonk = ObjectSpawner.instance.GetObjFromPool();
        Rigidbody2D rigidbody2D = bonk.GetComponent<Rigidbody2D>();
        bonk.gameObject.SetActive(true);
        return rigidbody2D;
    }
}
