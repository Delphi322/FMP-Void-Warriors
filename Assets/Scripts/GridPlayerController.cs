using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlayerController : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.up));
        if (Input.GetKey(KeyCode.DownArrow) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.down));
        if (Input.GetKey(KeyCode.LeftArrow) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.left));
        if (Input.GetKey(KeyCode.RightArrow) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.right));
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
