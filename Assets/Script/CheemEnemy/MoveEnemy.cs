using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] List<Transform> post = new List<Transform>();
    private int timeRate = 1;
    private float lastTime;
    int numberPort = 1;
    private void Update()
    {  
        if(Time.time - lastTime >= timeRate/10 && EnemySkill.instance.isStopNormalAttack == false)
        {
            if (Vector2.Distance(transform.position, post[numberPort].position) >= 0.1f)
            {
                Move(numberPort);
                return;
            }
            else
            {
                numberPort = RandomPos();
                RandomTime();
            }
        }
    }
    private int RandomPos()
    {
        return Random.Range(0, post.Count);
    }
    private void RandomTime()
    {
        timeRate = Random.Range(1, 4);
    }
    private void Move(int i)
    {
        transform.position = Vector3.MoveTowards(transform.position, post[i].position, 5 * Time.deltaTime);
    }
}
