using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlayerController : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;

    public Vector2 lastMove;
    private Vector2 moveInput;

    private Animator anima;

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        lastMove = moveInput;

        if (lastMove.x >= 0)
            transform.eulerAngles = new Vector2(0, 180);
        else
            transform.eulerAngles = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.up));
        if (Input.GetKey(KeyCode.DownArrow) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.down));
        if (Input.GetKey(KeyCode.LeftArrow) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.left));
        if (Input.GetKey(KeyCode.RightArrow) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.right));
            

        anima.SetFloat("MoveX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        anima.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anima.SetBool("IsMoving", isMoving);
        anima.SetFloat("LastMoveX", Mathf.Abs(lastMove.x));
        anima.SetFloat("LastMoveY", lastMove.y);
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;


        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

            while(elapsedTime < timeToMove)
            {
                transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

        transform.position = targetPos;

        isMoving = false;
    }
}
