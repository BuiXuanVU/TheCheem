using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheemMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Image coolDown;
    private Vector2 moveDirection;
    private bool canDash = true;
    private bool isDashing = false;
    private bool isCoolDown,isReset;
    private float power = 24f;
    private float dashTime = 0.2f;
    private float dashCoolDown = 1f;
    protected void Update()
    {
        if(UIManager.instance.isPlay == false)
        {
            return;
        }    
        if (Input.GetKeyDown(KeyCode.Space) && canDash == true)
        {
            isDashing = true;
            isReset = true;
        }
        if(isCoolDown)
        {
            coolDown.fillAmount -= 1 / dashCoolDown * Time.deltaTime;
            if (coolDown.fillAmount <= 0)
            {
                coolDown.fillAmount = 0;
                isCoolDown = false;
            }           
        }    
        this.ProcessInput();
    }
    private void FixedUpdate()
    {
        if (isDashing == true)
        {
            StartCoroutine(Dash());
            if(isReset)
            {
                coolDown.fillAmount = 1;
                isCoolDown = true;
                isReset = false;
            }    
            return;
        }
        PlayerMove();
    }
    protected virtual void ProcessInput()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 pos = Camera.main.WorldToViewportPoint(rb.position);
        pos.x = Mathf.Clamp(pos.x,0.1f,0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        Vector3 speed = rb.velocity;
        rb.position = Camera.main.ViewportToWorldPoint(pos);
        rb.velocity = speed;
    }
    protected virtual void PlayerMove()
    {
        rb.velocity = moveDirection * moveSpeed + rb.position * Time.fixedDeltaTime;
    }
    private IEnumerator Dash()
    {
        CheemRotace();
        rb.velocity = moveDirection * power + rb.position;
        yield return new WaitForSeconds(dashTime);
        transform.rotation = Quaternion.identity;
        canDash = false;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
    private void CheemRotace()
    {
        if (Input.GetAxis("Horizontal") >= 0)
            transform.Rotate(0, 0, -50);
        if (Input.GetAxis("Horizontal") < 0)
            transform.Rotate(0, 0, 50);
    }    
}
