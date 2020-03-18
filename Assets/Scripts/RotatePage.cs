using System.Collections;
using System.Collections.Generic;
using OculusSampleFramework;
using UnityEngine;

public class RotatePage : MonoBehaviour
{
    private bool _isMoving;

    private Vector3 _rotationFrom;
    private Vector3 _rotationTo;
    private GameObject _handsManager;
    // Start is called before the first frame update
    void Start()
    {
        _rotationFrom = new Vector3(179.999f, -89.99701f, -11.50598f);
        _rotationTo = new Vector3(179.999f, 89.99701f, 11.506f);
        _handsManager = GameObject.Find("HandsManager");
    }

    public void StartRotation()
    {
        StartCoroutine(RotateToSecondPosition(_handsManager.GetComponent<PianoPositionController>().canPositionPiano));
    }

    private IEnumerator RotateToSecondPosition(bool isPositioning)
    {
        _isMoving = true;
        float t = 0.0f;
        Quaternion quatFrom = Quaternion.Euler(_rotationFrom);
        Quaternion quatTo = Quaternion.Euler(_rotationTo);
        while ( t < 1f )
        {
            t += Time.deltaTime;
            if (isPositioning)
            {
                transform.localRotation = Quaternion.Slerp(quatFrom, quatTo, t/1f);
            }
            else
            {
                transform.localRotation = Quaternion.Slerp(quatTo, quatFrom, t/1f);
            }
            yield return null;

        }
        _isMoving = false;

    }
}
