using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiano : MonoBehaviour
{
    public Transform heightObj;
    public Transform heightObj2;
    public GameObject LHand;
    public GameObject RHand;
    public bool moveUp;
    public bool moveDown;

    public float offset = 1;

    private void FixedUpdate()
    {
        if(moveUp && heightObj.position.y < 1.01f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveDistance,
                transform.position.z);
        }
        if(moveDown && heightObj.position.y > 0.38909)
        {
            transform.position = new Vector3(transform.position.x , transform.position.y - moveDistance ,transform.position.z);
        }
    }

    public void moveToHand()
    {
        StartCoroutine(MoveToHandCoroutine(LHand.transform.position));
    }

    IEnumerator MoveToHandCoroutine(Vector3 handPos)
    {
        float counter = 0;
        float duration = 4f;

        //Get the current position of the object to be moved

        while (counter < duration)
        {
            counter += Time.deltaTime;
            var position = new Vector3(transform.position.x,  transform.position.y, transform.position.z);
            Vector3 newPos = new Vector3(position.x, handPos.y - offset, position.z);

            position =
                Vector3.Lerp(position, newPos, counter / duration);
            transform.position = position;
            yield return null;
        }

    }

    public void SetMoveUp(bool boole)
    {
        moveUp = boole;
    }
    public void SetMoveDown(bool boole)
    {
        moveDown = boole;
    }
    public float moveDistance = 0.1f;

    public void MoveFurtherCloser()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y ,transform.position.z - moveDistance);
    }
    public void MoveLeft()
    {
        transform.position = new Vector3(transform.position.x - moveDistance, transform.position.y ,transform.position.z);
    }
    public void MoveRight()
    {
        transform.position = new Vector3(transform.position.x + moveDistance, transform.position.y ,transform.position.z);
    }
    public void MoveUp(float value)
    {
        transform.position = new Vector3(transform.position.x , transform.position.y + value ,transform.position.z);
    }
    public void MoveDown()
    {
        transform.position = new Vector3(transform.position.x , transform.position.y - moveDistance ,transform.position.z);
    }
}
