using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ScaleType
{
    NONE,
    LENGTH_ONLY,
    LENGTH_AND_HIGHT
}
public abstract class SpellIndicator : MonoBehaviour
{
    public ScaleType scale_type;
    [SerializeField]
    protected float scale;
    protected Transform player_transform;

    public float Scale {
        get { return scale; }
        set
        {
            this.scale = value;
            onValueChange();
        }
    }

    public Projector[] Projectors {
        get { return GetComponentsInChildren<Projector>(); }
    }

    protected virtual void Start()
    {
        player_transform = GameObject.Find("Player").transform;//transform.parent.GetComponent<Skill>().player_transform;
        resizeIndicator();
    }

    public virtual void initializeIndicator()
    {
        foreach(Projector p in Projectors)
        {
            p.material = new Material(p.material);
            transform.position = Vector3.zero;
        }
    }

    protected virtual void Update() { }
    public virtual void onShow() { }
    public virtual void onHide() { }
    public virtual void onValueChange()
    {
        resizeIndicator();
    }

    protected virtual Vector3 getIndicatorPosition()
    {
        return player_transform.position;
    }
    //public Vector3 getPositionFromPlayerCamera()
    //{
    //    Camera player_camera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
    //    RaycastHit hit;
    //    var ray = player_camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    //    //var ray = player_camera.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //       // Debug.DrawRay(ray.origin, ray.direction, Color.red, 10.0f);
    //        return hit.point;
    //    }
    //    else
    //    {
    //        return player_transform.position;
    //    }
    //}

    protected void resizeIndicator()
    {
        foreach(Projector projector in Projectors)
        {
            resizeProjector(projector);
        }
    }

    private void resizeProjector(Projector projector)
    {
        if (projector != null)
        {
            if (scale_type != ScaleType.NONE)
            {
                if (scale_type == ScaleType.LENGTH_ONLY)
                {
                    //projector.aspectRatio = width / scale;
                }
                else
                {
                    projector.aspectRatio = 1.0f;
                }
                projector.orthographicSize = scale / 2;
            }

        }
    }

    protected void setShaderProperty(string property_name, float value)
    {
        foreach (Projector proj in Projectors)
        {
            if (proj.material.HasProperty(property_name))
            {
                proj.material.SetFloat(property_name, value);
            }
        }
    }
}
