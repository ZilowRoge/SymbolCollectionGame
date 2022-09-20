using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickShape : MonoBehaviour
{
    public List<Sprite> shapes = new List<Sprite>();
    public Image shape_to_draw;
    public int sprite_id = 0;

    private void Start()
    {
        shape_to_draw.sprite = shapes[sprite_id];
    }
    public void nextShape()
    {
        int next_id = Random.Range(0, shapes.Count);
        Debug.Log("Next symbol id: " + next_id);
        shape_to_draw.sprite = shapes[next_id];
        sprite_id = next_id;
    }

}
