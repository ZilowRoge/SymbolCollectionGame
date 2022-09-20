using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public float max_health;
    public float current_health;
    public float max_mana;
    public float current_mana;
    public float max_stamina;
    public float current_stamina;

    public int vitality;
    public float vitality_progress;
    public int endurence;
    public float endurence_progress;
    public int intelligence;
    public float intelligence_progress;
    public int strength;
    public float strength_progress;
    public int dexerity;
    public float dexerity_progress;
    public int power;

    private const float base_health = 100;
    private const float base_mana = 100;
    private const float base_stamina = 100;

    public PlayerStats()
    {
        loadStatistics();

        max_health = base_health + vitality * 10;
        current_health = max_health;
        max_mana = base_mana + intelligence * 10;
        current_mana = max_mana;
        max_stamina = base_stamina + endurence * 10;
        current_stamina = base_stamina;
    }

    public void saveStatistics()
    {
        PlayerPrefs.SetInt("Vitality", vitality);
        PlayerPrefs.SetInt("Endurence", endurence);
        PlayerPrefs.SetInt("Intelligence", intelligence);
        PlayerPrefs.SetInt("Strength", strength);
        PlayerPrefs.SetInt("Dexerity", dexerity);
        PlayerPrefs.SetInt("Power", power);
    }

    public void loadStatistics()
    {
        vitality = PlayerPrefs.GetInt("Vitality", 0);
        endurence = PlayerPrefs.GetInt("Endurence", 0);
        intelligence = PlayerPrefs.GetInt("Intelligence", 0);
        strength = PlayerPrefs.GetInt("Strength", 0);
        dexerity = PlayerPrefs.GetInt("Dexerity", 0);
        power = PlayerPrefs.GetInt("Power", 0);
    }
}
