using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Serialization;
public class SkillStatsDatabase : MonoBehaviour
{
    List<SkillStats> skill_database;

    string path;
    private void Awake()
    {
        path = Application.streamingAssetsPath + "/Skills.json";
        load();
    }

    public SkillStats getSkillStat(int id)
    {
        return skill_database[id];
    }

    private void load()
    {
        string data_as_json;
        if (File.Exists(path))
        {
            data_as_json = File.ReadAllText(path);
            SkillDatabase tmp = JsonUtility.FromJson<SkillDatabase>(data_as_json);
            skill_database = new List<SkillStats>(tmp.skills);
            Debug.Log("SkillStatsDatabase loaded " + skill_database.Count + " records.");
        }
        else
        {
            Debug.LogError("Cannot load skillTree");
        }
    }

    private void save()
    {
        SkillDatabase database = new SkillDatabase();
        database.skills = this.skill_database.ToArray();
        string json_string = JsonUtility.ToJson(database);
        if (File.Exists(path))
        {
            File.WriteAllText(path, json_string);
            Debug.Log(json_string);
        }
    }


}
