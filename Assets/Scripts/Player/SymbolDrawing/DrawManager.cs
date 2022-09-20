using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum EEnd
{
    LEFT,
    RIGHT,
    UP,
    DOWN
}
public class DrawManager : MonoBehaviour
{
    public Camera player_camera;
    public PickShape shape_picker;
    public RawImage output_image;


    private Texture2D output_texture;
    private EndPoints end_points = new EndPoints();
    private List<Vector2> points_to_normalize;
    private List<Vector3> point_world_position_list;
    public Transform hit_screen;
    private LayerMask draw_layer;
    private bool focus_mode_on = false;

    [Header("Line Settings")]
    public GameObject draw_crosshair; 
    public float distance_between_points;
    public GameObject line_prefab;
    private LineRenderer line_renderer;

    public bool spell_casting_game;

    public bool IsFocusMode {
        get { return focus_mode_on; }
        set { focus_mode_on = value; }
    }

    private void Start()
    {
        draw_layer = 1 << 3;
        point_world_position_list = new List<Vector3>();
        points_to_normalize = new List<Vector2>();
        setLineRenderer();
        //hit_screen = transform.GetChild(0);
    }

    private void setLineRenderer()
    {
        GameObject obj = Instantiate(line_prefab, hit_screen);
        line_renderer = obj.GetComponent<LineRenderer>();
        line_renderer.material = new Material(Shader.Find("Sprites/Default"));
        line_renderer.widthMultiplier = 0.2f;
        line_renderer.positionCount = 0;

        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.yellow, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );

        line_renderer.colorGradient = gradient;
    }
    void Update()
    {
        if (IsFocusMode || spell_casting_game)
        {
            drawSpellSymbol();
        }

    }

    void drawSpellSymbol()
    {
        if (Input.GetMouseButtonDown(0))
        {
            setHitScreen();
        }
        if (Input.GetMouseButton(0) && player_camera != null)
        {
            draw_crosshair.SetActive(true);
            RaycastHit hit;
            var ray = player_camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit, 100.0f, draw_layer))
            {
                if (hit.point != null)
                {
                    addPoint(hit.point);
                }
            }

            
        }

        if (spell_casting_game)
        {
            if (Input.GetMouseButtonUp(0))
            {
                draw_crosshair.SetActive(false);
                draw_image(points_to_normalize, end_points);

            }
            if (Input.GetKeyDown(KeyCode.E) && point_world_position_list.Count > 40)
            {
                saveTexture(output_texture);
                shape_picker.nextShape();
                clear();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                clear();
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                draw_crosshair.SetActive(false);
                draw_image(points_to_normalize, end_points);
                clear();
            }
        }
        
    }

    public void addPoint(Vector3 point)
    {
        if (canAddPoint(point))
        {
            line_renderer.positionCount++;
            if (point_world_position_list.Count > 0)
            {
                //drawLine(point_world_position_list[point_world_position_list.Count - 1], point, Color.cyan);
            }
            point_world_position_list.Add(point);
            line_renderer.SetPosition(line_renderer.positionCount - 1, point);
            Vector2 local_point = translateWorldToLocalPosition(hit_screen, point);
            //Debug.Log(local_point);
            checkForImageBoundry(local_point);
            points_to_normalize.Add(local_point);
        }
    }

    private void setHitScreen()
    {
        hit_screen.transform.position = transform.parent.position + transform.parent.forward * 30;
        hit_screen.transform.forward = -transform.parent.forward;
    }

    private void clear()
    {
        output_image.texture = clearTexture(new Texture2D(128, 128));
        end_points = new EndPoints();
        point_world_position_list.Clear();
        points_to_normalize.Clear();
        for (int i = hit_screen.childCount - 1; i >= 0; i--)
        {
            Destroy(hit_screen.GetChild(i).gameObject);
        }
        setLineRenderer();
    }

    bool canAddPoint(Vector3 current_point)
    {
        return point_world_position_list.Count == 0 ||
               (point_world_position_list.Count > 0 &&
                    Vector3.Distance(
                        point_world_position_list[point_world_position_list.Count - 1],
                        current_point) >= distance_between_points);
    }

    void checkForImageBoundry(Vector3 point)
    {
        if (point.x < end_points.left)
        {
            end_points.left = point.x;
        }
        if (point.x > end_points.right)
        {
            end_points.right = point.x;
        }
        if (point.y < end_points.down)
        {
            end_points.down = point.y;
        }
        if (point.y > end_points.up)
        {
            end_points.up = point.y;
        }
        //Debug.Log("End points" + end_points.toString() + " point = " + point);
    }

    Vector3 translateWorldToLocalPosition(Transform parent, Vector3 position)
    {
        GameObject point_object = new GameObject();
        point_object.name = "Point Object";
        point_object.transform.position = position;
        point_object.transform.parent = parent;
        Vector3 point = point_object.transform.localPosition;
        Destroy(point_object);
        return point;
    }

    void draw_image(List<Vector2> points, EndPoints end_points)
    {
        Debug.Log("Draw image");
        //foreach(Vector2 point in normalized_points)
        //{
        //    Debug.Log(point);
        //}
        Texture2D texture = createTexture(normalize(end_points, points), 128);
        output_texture = texture;
        output_image.texture = texture;
    }

    public List<Vector2> normalize(EndPoints end_points, List<Vector2> points)
    {
        List<Vector2> normalized_points = new List<Vector2>();

        float min_x = end_points.left;
        float max_x = end_points.right;

        float min_y = end_points.down;
        float max_y = end_points.up;

        foreach (Vector2 point in points)
        {
            float normalized_x = (point.x - min_x) / (max_x - min_x);
            float normalized_y = (point.y - min_y) / (max_y - min_y);

            normalized_points.Add(new Vector2(normalized_x, normalized_y));
        }

        return normalized_points;
    }

    public Texture2D clearTexture(Texture2D texture)
    {
        Color[] pixels = texture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.black;
        }
        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }
    public Texture2D createTexture(List<Vector2> normalized_points, int resolution)
    {
        Texture2D texture = new Texture2D(resolution, resolution);
        clearTexture(texture);

        foreach (Vector2 point in normalized_points)
        {
            int pixel_x = (int)(point.x * (resolution));
            int pixel_y = (int)(point.y * (resolution));
            texture.SetPixel(pixel_x, pixel_y, new Color(1.0f, 1.0f, 1.0f));

        }
        texture.Apply();
        return texture;
    }

    private void saveTexture(Texture2D texture)
    {
        byte[] bytes = texture.EncodeToPNG();
        Debug.Log("Symbol ID: " + shape_picker.sprite_id.ToString());
        var dirPath = Application.dataPath + "/RenderOutput/" + shape_picker.sprite_id.ToString() + "/";
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        string[] files = System.IO.Directory.GetFiles(dirPath);
        System.IO.File.WriteAllBytes(dirPath + "/R_" + files.Length/2 + ".png", bytes);
        Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath);

        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    //public GameObject draw_prefab;
    //public Camera player_camera;
    //private GameObject draw_trail;
    //private Plane plane_object;
    //private Vector3 start_position;

    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //GameObject free_look = transform.parent.GetChild(2).gameObject;
    //if (Input.GetMouseButtonDown(0))
    //{
    //    transform.parent.GetComponent<PlayerThirdPersonMovment>().stopMovment();
    //    plane_object = new Plane(player_camera.transform.forward * -1, transform.parent.position - transform.forward * 2);
    //    //free_look.SetActive(false);
    //    draw_trail = (GameObject)Instantiate(draw_prefab, transform.position, Quaternion.identity);
    //    draw_trail.transform.parent = this.transform.parent;

    //    Ray mouse_ray = player_camera.ScreenPointToRay(Input.mousePosition);//player_camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
    //    Debug.DrawRay(mouse_ray.origin, mouse_ray.direction, Color.red, 100.0f);
    //    float distance;
    //    if (plane_object.Raycast(mouse_ray, out distance))
    //    {
    //        start_position = mouse_ray.GetPoint(distance);
    //    }
    //}
    //else if (Input.GetMouseButton(0))
    //{

    //    Ray mouse_ray = player_camera.ScreenPointToRay(Input.mousePosition); //player_camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));//ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
    //    float distance;
    //    if (plane_object.Raycast(mouse_ray, out distance))
    //    {
    //        draw_trail.transform.position = mouse_ray.GetPoint(distance);
    //    }
    //}
    //else if (Input.GetMouseButtonUp(0))
    //{
    //    Vector3[] positions = new Vector3[100];
    //    Debug.Log(draw_trail.GetComponent<TrailRenderer>().GetPositions(positions));
    //    string str = "";
    //    foreach (Vector3 point in positions)
    //    {
    //        str += point - transform.position + " ";
    //    }
    //    Debug.Log(str);
    //    GameObject.Destroy(draw_trail);
    //    transform.parent.GetComponent<PlayerThirdPersonMovment>().startMovment();
    //}
    //    GameObject free_look = transform.parent.GetChild(2).gameObject;
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        transform.parent.GetComponent<PlayerThirdPersonMovment>().stopMovment();
    //        plane_object = new Plane(player_camera.transform.forward * -1, transform.parent.forward * 0.5Fy);
    //        plane_object.GetDistanceToPoint(transform.parent.position);

    //        Ray mouse_ray = player_camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
    //        float distance;
    //        if (plane_object.Raycast(mouse_ray, out distance))
    //        {
    //            start_position = mouse_ray.GetPoint(distance);
    //        }
    //        //free_look.SetActive(false);
    //        draw_trail = (GameObject)Instantiate(draw_prefab, start_position, Quaternion.identity);
    //        draw_trail.transform.parent = this.transform.parent;

    //    }
    //    else if (Input.GetMouseButton(0))
    //    {

    //        Ray mouse_ray = player_camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));//ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
    //        float distance;
    //        if (plane_object.Raycast(mouse_ray, out distance))
    //        {
    //            draw_trail.transform.position = mouse_ray.GetPoint(distance);
    //        }
    //    }
    //    else if (Input.GetMouseButtonUp(1))
    //    {
    //        Vector3[] positions = new Vector3[100];
    //        if (draw_trail.GetComponent<TrailRenderer>().positionCount <= 100)
    //        {
    //            Debug.Log(draw_trail.GetComponent<TrailRenderer>().GetPositions(positions));
    //            string str = "";
    //            foreach (Vector3 point in positions)
    //            {
    //                str += point + " ";
    //            }
    //            Debug.Log(str);
    //        }
    //        GameObject.Destroy(draw_trail);
    //        transform.parent.GetComponent<PlayerThirdPersonMovment>().startMovment();
    //    }
    //    followCamera();

    //}

    //private void followCamera()
    //{
    //    //Vector3 temp_pos = 
    //    //temp_pos.z = 10.0f;
    //    //transform.localPosition = player_camera.ScreenToWorldPoint(temp_pos);
    //    Vector3 temp_pos = player_camera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
    //    temp_pos.z = 10.0f;
    //    transform.position = Camera.main.ScreenToWorldPoint(temp_pos);
    //}
}
