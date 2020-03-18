using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicalButtonPressed : MonoBehaviour
{
    public UnityEvent onPressed;
    public UnityEvent onReleased;
    protected bool lastState = false;

    public void SetState(bool state) {
        if (state && !lastState && onPressed != null) {
            onPressed.Invoke();
        }
        else
        {
            onReleased.Invoke();
        }
        lastState = state;
    }
}
