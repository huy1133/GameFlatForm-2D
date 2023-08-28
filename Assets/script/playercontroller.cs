using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    enum Status
    {
        move,
        attack,
        beaten
    }
    [SerializeField] float speed;
    [SerializeField] float forceJump;
    new Animator animation;
    new Rigidbody2D rigidbody2D;
    float moveStatus;
    float attackStatus;
    float beatStatus;
    Status status;
    float horizontal;
    bool canJump;
    bool canSlip;

    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        canJump = false;
        status = Status.move;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        move();
        statusAnimation();
    }
    private void move()
    {
        Debug.Log(transform.localScale);
        rigidbody2D.velocity = new Vector2(horizontal*speed,rigidbody2D.velocity.y);
        if (horizontal>0)
        {
            Vector3 temp = transform.localScale;
            transform.localScale =(new Vector3(math.abs(temp.x), temp.y, temp.z));
        }
        else if(horizontal<0)
        {
            Vector3 temp = transform.localScale;
            transform.localScale = (new Vector3(math.abs(temp.x) * (-1), temp.y, temp.z));
        }
        if (canJump&&Input.GetKey(KeyCode.Space))
        {
            rigidbody2D.velocity = Vector2.up * forceJump;
            canJump=false;
        }
    }
    private void statusAnimation()
    {
        if(status == Status.move){
            animation.SetTrigger("Move");
            if (!canJump)
            {
                moveStatus = 4;
            }
            else
            {
                if (rigidbody2D.velocity.x == 0)
                {
                    moveStatus = 0;
                    if (Input.GetKey(KeyCode.C))
                    {
                        moveStatus = 1;
                    }
                }
                else if (rigidbody2D.velocity.x != 0)
                {
                    moveStatus = 2;

                    if (speed == 3)
                    {
                        canSlip = true;
                    }
                    if (Input.GetKey(KeyCode.C) && canSlip)
                    {
                        moveStatus = 3;
                        speed -= 0.015f;
                        if(speed <=2.5)
                            canSlip = false;
                    }
                    else
                    {
                        if (speed <= 3)
                        {
                            speed += 0.015f;
                        }
                    }
                    
                }
            }
            Debug.Log(rigidbody2D.velocity.magnitude);
        }

        animation.SetFloat("status", moveStatus);
    }
}
