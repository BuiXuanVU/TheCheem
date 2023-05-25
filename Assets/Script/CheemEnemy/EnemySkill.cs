using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : ComponentBehaviour
{
    public static EnemySkill instance;
    [SerializeField] private float speed = 8;
    [SerializeField] private Transform player;
    [SerializeField] private Transform BigBat1;
    [SerializeField] private Transform BigBat2;
    [SerializeField] private Vector3 dashPos;
    private Vector3 bat1,bat2;
    public bool isStopNormalAttack;
    public bool isDash,isPow;
    bool moveTo = true, toReturn = false, toDash = false, toPow = true,toIn = false, toOut = false;
    protected override void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if(isDash)
        {
            StartCoroutine(dash());
        }    
        if(isPow == true)
        {
            if (toOut == true)
            {
                MoveOutBat();
            }
            if (toPow == true)
            {
                bat1 = new Vector2(BigBat1.transform.position.x + 28, BigBat1.transform.position.y);
                bat2 = new Vector2(BigBat2.transform.position.x - 28, BigBat2.transform.position.y);
                toPow = false;
                toIn = true;
            }    
            if(toIn)
            {
                MoveBigBat();
            }    
            
        }    
    }
    public void Fire(Rigidbody2D rigidbody)
    {
        rigidbody.AddForce(Vector2.left * speed);
    }
    public void FireRotace(Rigidbody2D rigidbody)
    {
        BatCtrl bat = rigidbody.transform.GetComponent<BatCtrl>();
        bat.isRotace = true;
        Fire(rigidbody);
    }
    public void ThreeDirection(List<Rigidbody2D> rigitList)
    {
        int ran = Random.Range(0,1);
        if(ran == 0)
        {
            for (int i = 0; i < rigitList.Count; i++)
            {
                BatCtrl bat = rigitList[i].transform.GetComponent<BatCtrl>();
                bat.isRotace = true;
            }
        }
        rigitList[0].AddForce(new Vector2(-50, 25) * speed * Time.fixedDeltaTime);
        rigitList[1].AddForce(new Vector2(-50, -25) * speed * Time.fixedDeltaTime);
        Fire(rigitList[2]);
    }
    
    IEnumerator dash()
    {
        isStopNormalAttack = true;
        if(moveTo) BigCheemMoveTo();
        yield return new WaitForSeconds(2f);
        moveTo = false;
        yield return new WaitForSeconds(0.5f);
        if (!toReturn) toDash = true;
        if (toDash) BigCheemDash();
        yield return new WaitForSeconds(1f);
        if (toReturn) ReturnOldPos();
    }    
    private void BigCheemMoveTo()
    {
        Vector2 startDashPos = new Vector2(transform.parent.position.x, player.position.y);
        dashPos = transform.parent.position;
        if (Vector2.Distance(transform.parent.position, startDashPos) >= 0.1f)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, startDashPos, 7 * Time.deltaTime);
        }
    }
    private void BigCheemDash()
    {
        if (Vector2.Distance(transform.position, dashPos) <= 25f)
        {
            transform.parent.Translate(Vector3.left * 25 * Time.deltaTime);
        }
        else if((Vector2.Distance(transform.position, dashPos) > 25f))
        {
            transform.parent.position = new Vector2(dashPos.x + 8, dashPos.y);
            toDash = false;
            toReturn = true;
        }    
    }  
    private void ReturnOldPos()
    {
        if (Vector2.Distance(transform.parent.position, dashPos) >= 0.1f)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, dashPos, 5 * Time.deltaTime);
        }
        else
        {
            isDash = false;
            isStopNormalAttack = false;
        }     
    }
    private void MoveBigBat()
    {
        if (Vector2.Distance(BigBat1.transform.position, bat1) >= 1f)
        {
            BigBat1.transform.Translate(Vector3.right * 15 * Time.deltaTime);
        }
        if (Vector2.Distance(BigBat2.transform.position, bat2) >= 1f)
        {
            BigBat2.transform.Translate(Vector3.left * 15 * Time.deltaTime);
        }  
        else
        {
            toIn = false;
            toOut = true;
        }    
    }
    private void MoveOutBat()
    {
        if (Vector2.Distance(BigBat1.transform.position, bat1) <= 28f)
        {
            BigBat1.transform.Translate(Vector3.left * 15 * Time.deltaTime);
        }
        if(Vector2.Distance(BigBat2.transform.position, bat2) <= 28f)
        {
            BigBat2.transform.Translate(Vector3.right * 15 * Time.deltaTime);
        }
        else
        {
            isPow = false;
            isStopNormalAttack =false;
        }    
    }    
}
