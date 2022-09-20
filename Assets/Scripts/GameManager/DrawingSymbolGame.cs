using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DrawingSymbolGame : MonoBehaviour
{
    public int points_for_symbols;
    public PickShape shape_picker;
    //public Text points_text;
    private Timer symbol_draw_timer;
    private float symbol_drawing_time;
    private const float max_time_for_symbol = 30;

    void Start()
    {
        shape_picker = GetComponent<PickShape>();
        symbol_draw_timer = new Timer(max_time_for_symbol, true);
        symbol_drawing_time = 0.0f;
        points_for_symbols = 0;
        //points_text.text = points_for_symbols.ToString();
        shape_picker.nextShape();
    }

    public void onSymbolAdd()
    {
        shape_picker.nextShape();
    }

    public void saveToFile(int symbol_id, string record)
    {
        string filename = symbol_id.ToString() + ".txt";
        Debug.Log("Save to file: " + filename);
        File.AppendAllText(filename, record);
    }

    public int getCurrentSymbolId()
    {
        return shape_picker.sprite_id;
    }

    public void resetTimer()
    {
        symbol_draw_timer.reset();
    }

    void Update()
    {
        symbol_draw_timer.updateForward();


    }
}
