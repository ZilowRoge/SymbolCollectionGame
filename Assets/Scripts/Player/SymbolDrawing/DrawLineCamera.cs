using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineCamera : MonoBehaviour
{
    public LineRenderer line_renderer;
    public float line_width = 0.04f;
    public float minimum_vertex_distance = 0.1f;
    public Camera draw_camera;

    private bool is_line_started = false;
    // Start is called before the first frame update
    void Start()
    {
        line_renderer.startColor = Color.red;
        line_renderer.endColor = Color.red;

        line_renderer.startWidth = line_width;
        line_renderer.endWidth = line_width;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            line_renderer.positionCount = 0;
            Vector3 position = getWorldPosition();
            line_renderer.positionCount = 2;
            line_renderer.SetPosition(0, position);
            line_renderer.SetPosition(1, position);
            is_line_started = true;
        }

        if (Input.GetMouseButton(0) && is_line_started)
        {
            Vector3 current_pos = getWorldPosition();
            float distance = Vector3.Distance(current_pos, line_renderer.GetPosition(line_renderer.positionCount - 1));
            if (distance > minimum_vertex_distance)
            {
                updateLine();
            }
        }
    }

    private void updateLine()
    {
        line_renderer.positionCount++;
        line_renderer.SetPosition(line_renderer.positionCount - 1, getWorldPosition());
    }

    private Vector3 getWorldPosition()
    {
        return draw_camera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
    }
}
