using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiano : MonoBehaviour
{
    public float moveDistance = 0.1f;
    public void MoveFurtherAway()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y ,transform.position.z + moveDistance);
    }
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
    public void MoveUp()
    {
        transform.position = new Vector3(transform.position.x , transform.position.y + moveDistance ,transform.position.z);
    }
    public void MoveDown()
    {
        transform.position = new Vector3(transform.position.x , transform.position.y - moveDistance ,transform.position.z);
    }
}
