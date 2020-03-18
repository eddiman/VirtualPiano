using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPositionController : MonoBehaviour
{
    public bool canPositionPiano;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void TogglePositioning()
    {
        canPositionPiano = !canPositionPiano;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
