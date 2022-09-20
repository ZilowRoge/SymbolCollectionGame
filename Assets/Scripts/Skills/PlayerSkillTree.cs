using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Serialization;
using System;

public class PlayerSkillTree : MonoBehaviour, ILoad, ISave
{
    private List<SkillTreeStats> skill_tree;
    private SkillTreeUI skill_tree_ui;
    private string path;

    void Awake()
    {
        //skill_tree_ui = GameObject.Find("SkillTreeFrame").GetComponent<SkillTreeUI>();
        path = Application.streamingAssetsPath + "/SkillTree.json";

        load();
    }

    public SkillTreeStats getSkill(int id)
    {
        return skill_tree.Count > id ? skill_tree[id] : null;
    }

    public bool isSkillUnlocked(int id)
    {
        return skill_tree.Count > id && skill_tree[id].state == (int)SkillActivityState.UNLOCKED;
    }

    public void setSkillActivityState(int id, SkillActivityState state)
    {
        if(skill_tree.Count > id)
        {
            skill_tree[id].state = (int)state;
            if (isSkillUnlocked(id))
            {
                activateAncestorsSkills(id);
            }
        }
    }

    public void activateAncestorsSkills(int id)
    {
        foreach (int ancestor in skill_tree[id].ancestors)
        {
            if (skill_tree[ancestor].state == (int)SkillActivityState.INACTIVE)
            {
                setSkillActivityState(ancestor, SkillActivityState.ACTIVE);
                //skill_tree_ui.node_list[ancestor].updateNode();
            }
        }
    }

    //public bool canSkillBeUnlocked(int id)
    //{
    //    return skill_tree.skills.Length > id && skill_tree.skills[id].state == (int)SkillActivityState.ACTIVE;
    //}

    //public bool upgradeSkill(int id)
    //{
    //    if (canSkillBeUnlocked(skill_id) && skills.TryGetValue(skill_id, out skill_inspect))
    //    {
    //        skill_inspect.level++;
    //        skills.Remove(skill_id);
    //        skills.Add(skill_id, skill_inspect);
    //        Debug.Log("Skill upgraded");
    //        return true;
    //    }
    //    return false;
    //}

    public void load()
    {
        string data_as_json;
        if (File.Exists(path))
        {
            data_as_json = File.ReadAllText(path);
            SkillTree tmp = JsonUtility.FromJson<SkillTree>(data_as_json);
            skill_tree = new List<SkillTreeStats>(tmp.skills);
            Debug.Log("Loaded skills: " + skill_tree.Count);
        }
        else
        {
            Debug.LogError("Cannot load skillTree");
        }
    }

    public void save()
    {
        SkillTree tree = new SkillTree();
        tree.skills = this.skill_tree.ToArray();
        string json_string = JsonUtility.ToJson(tree);
        if (File.Exists(path))
        {
            File.WriteAllText(path, json_string);
            Debug.Log(json_string);
        }
    }
}
