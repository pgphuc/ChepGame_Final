
using System;
using UnityEngine;
public class Player : Character
{   
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask GroundLayer;

    
    private float jumpForce = 1500;
    private float speed = 7;

    private bool isActioning;
    private bool isJumping;
    private float horizontal;
    private bool isGrounded;
    private bool isDead = false;
    
    
    private int Coin = 0;
    public Vector3 savePos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SavePos();
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
        isGrounded = CheckGrouned();
        //-1 -> 0 -> 1
        /*
         * -1 là đi sang trái
         * 0 là đứng im
         * 1 là đi sang phải
         */
        horizontal = Input.GetAxisRaw("Horizontal");
        
        //Điều khiển nhân vật

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))//Khi bấm Space 
            {
                Jump();
            }
            else if (Input.GetKeyDown(KeyCode.X)) //Khi bấm phím x
            {
                Attack();
            }
            else if (Input.GetKeyDown(KeyCode.C)) //Khi bấm phím c
            {
                Throw();
            }
            //Khi di chuyển
            //horizontal là số float, số float thường bị sai số chứ !=0
            else if (!isJumping)//Khi bấm phím sang trái phải
            {
                if (Mathf.Abs(horizontal) > 0.1f)
                {
                    ChangeAnim("Run");
                    Moving();
                }
                else if (!isActioning && Mathf.Abs(horizontal) == 0)
                {
                    Idle();
                }
            }
        }
        else
        {
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                Moving();
            }
            //Khi chạm đất
            if (rb.linearVelocity.y < 0 && !isGrounded)
            {
                Debug.Log(isGrounded);
                Fall();
            }
        }
        
    }

    public override void OnInit()//reset các thông số hoặc đưa về trạng thái ban đầu
    {
        base.OnInit();
        isDead = false;
        isActioning = false;
        isJumping = false;
        transform.position = savePos;
        ChangeAnim("Idle");
    }


    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
    }
    private bool CheckGrouned()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down*1.1f, Color.red,10f);
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, GroundLayer);
        
        // if (hit.collider != null)
        //     return true;
        // else
        //     return false;
        return hit.collider != null;
    }

    private void Attack()
    {
        rb.linearVelocity = Vector2.zero;
        ChangeAnim("Attack");
        isActioning = true;
        Invoke("ResetAction", 0.5f);
        
    }
    private void Throw()
    {
        rb.linearVelocity = Vector2.zero;
        ChangeAnim("Throw");
        isActioning = true;
        Invoke("ResetAction", 0.5f);
    }
    private void ResetAction()
    {
        isActioning = false;
        if (Mathf.Abs(horizontal) <= 0.1f)
        {
            ChangeAnim("Idle");
        }
    }
    private void Jump()
    {
        ChangeAnim("Jump");
        isJumping = true;
        rb.AddForce(Vector2.up * jumpForce);
    }

    private void Idle()
    {
        ChangeAnim("Idle");//Chuyển anim đứng im
        rb.linearVelocity = Vector2.zero;
    }

    private void Moving()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        //Quay mặt khi chuyển hướng
        transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0)); //Nếu horizontal lớn hơn 0 -> trả về 0 và ngược lại trả về 180
    }

    private void Fall()
    {
        ChangeAnim("Fall");
        isJumping = false;
    }

    
    
    internal void SavePos()
    {
        savePos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Coin++;
            Debug.Log(Coin);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "DeathZone")
        {
            ChangeAnim("Death");
            isDead = true;
            Invoke("OnInit", 0.5f);
        }

    }

    
}
