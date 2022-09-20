using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : Timer
{
    public Progress(float max_time) : base(max_time)
    {}

    public float get_progress()
    {
        float fill_progress = time_passed / max_time;
        return Mathf.Clamp(fill_progress, 0, 1);
    }

}
