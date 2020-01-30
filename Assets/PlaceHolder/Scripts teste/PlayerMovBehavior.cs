using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerAnimBehavior anim;

    [SerializeField] private bool onGround;
    [SerializeField] private bool isDead = false;

    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float maxSpeed = 4;
    [SerializeField] float jumpForce = 0;
    [SerializeField] private float currentSpeed;


    void Start()
    {
        currentSpeed = maxSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<PlayerAnimBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

        Movimentate();
        if (Input.GetButtonDown("Jump") && onGround)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
    }


    void Movimentate()
    {
        x = Input.GetAxis("Horizontal") * currentSpeed;
        y = Input.GetAxis("Vertical") * currentSpeed;


        if (!isDead)
        {

         if(!onGround  && (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") == 0))
            {
                y = 0;
               
            }
            rb.velocity = new Vector2(x, y);
            if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                anim.WalkingAnim();
            }
            else
            {
                anim.EndWalkingAnim();
            }
            Flip();     
        }
    }
    void Jump()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Vertical") * jumpForce, 0));
    }
    void Flip()
    {
        Vector3 scale = transform.localScale;
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            if(scale.x < 0)
            {
                scale.x *= -1;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (scale.x > 0)
            {
                scale.x *= -1;
            }
        }
        transform.localScale = scale;
    }

    void Attack ()
    {
        anim.AttackAnim(); ;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.tag);

        if(other.gameObject.tag == "Ground")
        {
            onGround = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }

}
