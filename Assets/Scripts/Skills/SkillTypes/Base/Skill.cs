using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element {FIRE, WATER, WIND, EARTH, LIGTHNING, DARK, LIGHT};
[System.Serializable]
public abstract class Skill : MonoBehaviour
{
    public Transform player_transform;
    public Serialization.SkillStats stats;
    //private Timer duration_timer;
    protected virtual void Start()
    {
        player_transform = GameObject.Find("Player").transform;
    }
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Cast");
            onCast();
        }
        //if (duration_timer != null) {
        //    duration_timer.updateForward();
        //    if (duration_timer.finished()) {
        //        onDestroy();
        //    }
        //}
    }

    public string getTooltip()
    {
        string skill_description = "";
        return skill_description;
    }
    public abstract void onActivation();
    public abstract void onCast();
    protected virtual void onDestroy()
    {
        GameObject.Destroy(this.gameObject);
    }
    
}
