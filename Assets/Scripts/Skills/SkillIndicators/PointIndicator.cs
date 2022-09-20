using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointIndicator : SpellIndicator
{
    public bool restrict_indicator_to_range = false;
    public LayerMask raycast_layer;
    private float range;

    protected override void Start()
    {
        base.Start();
        range = transform.parent.GetComponent<PointRangeSkill>().spell_range;
        //ignore_layer = ~ignore_layer;
    }
    protected override void Update()
    {
        transform.position = getIndicatorPosition();
        if (restrict_indicator_to_range)
        {
            restrictIndicatorToRange();
        }
    }

    protected override Vector3 getIndicatorPosition()
    {
        Camera player_camera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        RaycastHit hit;
        var ray = player_camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //var ray = player_camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycast_layer))
        {
            // Debug.DrawRay(ray.origin, ray.direction, Color.red, 10.0f);
            return hit.point;
        }
        else
        {
            return player_transform.position;
        }
    }

    private void restrictIndicatorToRange()
    {
        if (Vector3.Distance(player_transform.position, transform.position) > range)
        {
            transform.position = player_transform.position + Vector3.ClampMagnitude(transform.position - player_transform.position, range);
        }
    }
}
