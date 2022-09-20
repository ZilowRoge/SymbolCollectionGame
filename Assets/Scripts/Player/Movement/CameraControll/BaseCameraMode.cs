using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCameraMode
{
    public List<Camera> cameras;
    public float field_of_view;
    public Vector3 position;
    public BaseCameraMode(List<Camera> cameras_, float field_of_view_, Vector3 position_)
    {
        cameras = cameras_;
        field_of_view = field_of_view;
    }
    public abstract void handleCameras(float horizontal, float vertical);
}
