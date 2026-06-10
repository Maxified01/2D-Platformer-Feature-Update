using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;

    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;

    private float jumpPower = 12f;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();   // Fixed was myBody.GetComponent — never assigned
        anim = GetComponent<Animator>();
    }

    void Start()
    {
    }

    void Update()
    {
        CheckIfGrounded();   // Fixed these two lines were missing
        PlayerJump();
    }

    void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk()
    {
        float h = Input.GetAxis("Horizontal");  // Fixed was "0" — invalid  unity axis name

        if (h > 0)
        {
            myBody.linearVelocity = new Vector2(speed, myBody.linearVelocity.y);
            ChangeDirection(1);
        }
        else if (h < 0)
        {
            myBody.linearVelocity = new Vector2(-speed, myBody.linearVelocity.y);
            ChangeDirection(-1);
        }
        else
        {
            myBody.linearVelocity = new Vector2(0f, myBody.linearVelocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.linearVelocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {  // Fixed was checking "jumped" — now checks spacebar
                jumped = true;
                myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, jumpPower);
                anim.SetBool("Jump", true);
            }
        }
    }

} // class