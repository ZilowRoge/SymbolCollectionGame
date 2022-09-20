using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetClearFlag : MonoBehaviour
{
    public CameraClearFlags flag;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().clearFlags = flag;
    }
}
