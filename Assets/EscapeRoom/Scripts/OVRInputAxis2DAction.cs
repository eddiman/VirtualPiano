using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class OVRInputAxis2DAction : Vector2Action
{
    public OVRInput.Controller controller = OVRInput.Controller.Active;
    public OVRInput.Axis2D axis;

    void Update() {
        Receive(OVRInput.Get(axis, controller));
    }
}
