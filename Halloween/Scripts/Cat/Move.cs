using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D player;
    float walkForce = 20.0f;
    float maxWalkSpeed = 1.0f;
    int key = 0;
    public float jumpForce = 150.0f;
    private bool jumpCount = false;
    Animator animator;
    private float vertical = 0;
    private float horizontal = 0;
    bool ground = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 애니메이션 부분
        if(Input.GetAxis("Horizontal") == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            animator.SetFloat("Move X", -1.0f); // 왼쪽
            animator.SetBool("isMoving", true);
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            animator.SetFloat("Move X", 1.0f); // 오른쪽
            animator.SetBool("isMoving", true);
        }

        // 점프

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(ground == true)
            {
                jumpCount = true;
            }
        }
        key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(jumpCount)
        {
            player.AddForce(transform.up * jumpForce);
            jumpCount = false;
            ground = false;
        }

        float speedx = Mathf.Abs(player.velocity.x);

        if(speedx < maxWalkSpeed)
        {
            player.AddForce(transform.right * key * walkForce);
        }
    }

    void OnTriggerEnter2D(Collider2D tile)
    {
        if(tile.gameObject.layer == 6 && player.velocity.y < 0)
        {
            Debug.Log("붙었다.");
            ground = true;
        }
    }
}
