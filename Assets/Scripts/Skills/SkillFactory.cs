using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFactory
{
    public static GameObject createFireball()
    {
        return GameObject.Instantiate(Resources.Load<GameObject>("Skill/Prefab/Fireball.prefab"));
    }
    public static GameObject createLigthningball()
    {
        return GameObject.Instantiate(Resources.Load<GameObject>("Skill/Prefab/Ligthningball.prefab"));
    }
    public static GameObject createEarthball()
    {
        return GameObject.Instantiate(Resources.Load<GameObject>("Skill/Prefab/Earthball.prefab"));
    }
    public static GameObject createWaterball()
    {
        return GameObject.Instantiate(Resources.Load<GameObject>("Skill/Prefab/Waterball.prefab"));
    }
    public static GameObject createWindball()
    {
        return GameObject.Instantiate(Resources.Load<GameObject>("Skill/Prefab/Windball.prefab"));
    }
}
