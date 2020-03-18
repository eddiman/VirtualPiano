using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour
{

    public string TriggerTag = "HandAnchor";
    public float percentageOfHeightToPress = 60f;
    public UnityEvent onButtonPressed;
    private float _moveDuration = .5f;
    private Vector3 _moveToVector;
    private Vector3 _originLocalPos;
    private bool _isMoving;
    private float rendererHeight;
    private void Start()
    {
        rendererHeight = GetComponent<MeshRenderer>().bounds.size.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        _originLocalPos = transform.localPosition;
        if(other.transform.CompareTag(TriggerTag) && !_isMoving)
            StartCoroutine(DownPress(_moveToVector));

    }

    private IEnumerator DownPress(Vector3 _moveToVector)
    {
        _isMoving = true;
        var position = transform.localPosition;
        _moveToVector = new Vector3(_originLocalPos.x, _originLocalPos.y - ((rendererHeight / 100) * percentageOfHeightToPress), _originLocalPos.z);
        float t = 0.0f;
        while ( t < _moveDuration )
        {
            t += Time.deltaTime;
            position = Vector3.Lerp(position, _moveToVector, t/_moveDuration);
            transform.localPosition = position;
            yield return null;

        }
        //if (Math.Abs(t - _moveDuration) < 0.01f) yield break;
        onButtonPressed.Invoke();
        Debug.Log("down_finished");
        StartCoroutine(MoveBackup());

    }
    private IEnumerator MoveBackup()
    {
        var position = transform.localPosition;
        _moveToVector = new Vector3(position.x, position.y + ((rendererHeight / 100) * percentageOfHeightToPress), position.z);
        float t = 0.0f;
        while ( t < _moveDuration )
        {
            t += Time.deltaTime;
            position = Vector3.Lerp(position, _moveToVector, t/_moveDuration);
            transform.localPosition = position;
            yield return null;

        }
        Debug.Log("up is done");
        _isMoving = false;
    }
}
