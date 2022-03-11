using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Animator anim;
    private Rigidbody2D myRigidbody;

    public Vector2 lastMove;
    public float ClampMoveX;
    private Vector2 moveInput;

    private bool isMoving;

    private static bool playerExists;


    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        if(!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {      
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        isMoving = false;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed, 0);
            isMoving = true;
            lastMove = moveInput;
            anim.SetFloat("ClampMoveX", Input.GetAxisRaw("Horizontal"));

            if (lastMove.x >= 0)
                transform.eulerAngles = new Vector2(0, 180);
            else
                transform.eulerAngles = Vector2.zero;
        }
        else if (Input.GetAxisRaw("Vertical") != 0)
        {
           myRigidbody.velocity = new Vector2(0, moveInput.y * moveSpeed);
           isMoving = true;
           lastMove = moveInput;
           anim.SetFloat("ClampMoveY", Input.GetAxisRaw("Vertical"));

           
        }
        else
        {
           myRigidbody.velocity = Vector2.zero;
        }



        anim.SetFloat("MoveX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("IsMoving", isMoving);
        anim.SetFloat("LastMoveX", Mathf.Abs(lastMove.x));
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}
