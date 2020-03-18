using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangent : MonoBehaviour
{
    public Vector3 initLocalPos;
    // Start is called before the first frame update
    void Start()
    {
        initLocalPos = transform.localPosition;
    }
}
