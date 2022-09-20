using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    protected float max_time;
    protected float time_passed;

    public Timer(float time, bool reset_to_start = false)
    {
        max_time = time;
        time_passed = reset_to_start ? time : 0;
    }

    public void updateForward()
    {
        if (time_passed < max_time) 
            time_passed += Time.deltaTime;
    }

    public void updateBackward(float multiplier = 1)
    {
        time_passed -= Time.deltaTime * multiplier;
    }

    public void reset()
    {
        time_passed = 0;
    }

    public bool finished()
    {
        return time_passed >= max_time;
    }

    public bool zero()
    {
        return time_passed <= 0;
    }

    public float get_time()
    {
        return time_passed;
    }
}
