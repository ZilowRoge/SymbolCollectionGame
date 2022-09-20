using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerThirdPersonMovment movment;
    //private CastSpell spell_caster;
    private DrawManager draw_manager;
    
    private Material standard_material;
    private Material focus_material;
    private bool is_focus_mode_active = false;

    public bool IsFocusModeActive {
        get { return is_focus_mode_active; }
    }
    private void Awake()
    {
        PlayerPrefs.SetString("PlayerName", "Bartosz");
    }
    void Start()
    {
        movment = GetComponent<PlayerThirdPersonMovment>();
        //spell_caster = new CastSpell();
        draw_manager = GetComponentInChildren<DrawManager>();

        loadMaterials();
        deactivateFocusMode();
        //statistics_ui = GameObject.Find("PlayerStatistics").GetComponent<PlayerStatsUI>();
        //updatePlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
    }

    private void handleInput()
    {
        if (movment.canMove())
        {
            movment.handleMovment();
        }
        if (is_focus_mode_active)
        {
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //handleModeChange();
        }
    }

    public void onSave()
    {
    }

    #region ModeHandling
    private void loadMaterials()
    {
        standard_material = Resources.Load<Material>("Materials/Player/StandardPlayerMat");
        focus_material = Resources.Load<Material>("Materials/Player/FocusPlayerMat");
    }
    private void activateFocusMode()
    {
        //Debug.Log("Activate");
        setMaterialsInChildren(focus_material);
        is_focus_mode_active = true;
    }

    private void deactivateFocusMode()
    {
        //Debug.Log("Deactivate");
        //setMaterialsInChildren(standard_material);

        is_focus_mode_active = false;
    }
    private void setMaterialsInChildren(Material material)
    {
        Transform player_body = transform.Find("Body");
        player_body.gameObject.GetComponent<MeshRenderer>().material = material;
        for (int i = 0; i < player_body.childCount; i++)//GetComponentInChildren<MeshRenderer>())
        {
            Transform child = player_body.GetChild(i);
            child.gameObject.GetComponent<MeshRenderer>().material = material;
        }
    }
    private void handleModeChange()
    {
        if (is_focus_mode_active)
        {
            deactivateFocusMode();
            movment.MovmentSpeed = movment.walk_speed;
        }
        else
        {
            activateFocusMode();
            movment.MovmentSpeed = movment.walk_speed * 0.8f;
        }
        draw_manager.IsFocusMode = is_focus_mode_active;
    }

    #endregion

    #region PlayerStatisticsManagment

    //public void updatePlayerStats()
    //{
    //    statistics_ui.health_ui.text = statistics.current_health + "/" + statistics.max_health;
    //    statistics_ui.mana_ui.text = statistics.current_mana + "/" + statistics.max_mana;
    //    statistics_ui.stamina_ui.text = statistics.current_stamina + "/" + statistics.max_stamina;

    //    statistics_ui.vitality_ui.text = statistics.vitality.ToString();
    //    statistics_ui.intelligence_ui.text = statistics.intelligence.ToString();
    //    statistics_ui.endurence_ui.text = statistics.endurence.ToString();
    //    statistics_ui.strength_ui.text = statistics.strength.ToString();
    //    statistics_ui.dexerity_ui.text = statistics.dexerity.ToString();
    //    statistics_ui.power_ui.text = statistics.power.ToString();

    //}
    //public void increaseVitality()
    //{
    //    statistics.vitality++;
    //    updatePlayerStats();
    //}
    //public void increaseIntelligence()
    //{
    //    statistics.intelligence++;
    //    updatePlayerStats();
    //}
    //public void increaseEndurence()
    //{
    //    statistics.endurence++;
    //    updatePlayerStats();
    //}
    //public void increaseStrength()
    //{
    //    statistics.strength++;
    //    updatePlayerStats();
    //}
    //public void increaseDexerity()
    //{
    //    statistics.dexerity++;
    //    updatePlayerStats();
    //}
    //public void increasePower()
    //{
    //    statistics.power++;
    //    updatePlayerStats();
    //}
    //public void decreaseVitality()
    //{
    //    statistics.vitality--;
    //    updatePlayerStats();
    //}
    //public void decreaseIntelligence()
    //{
    //    statistics.intelligence--;
    //    updatePlayerStats();
    //}
    //public void decreaseEndurence()
    //{
    //    statistics.endurence--;
    //    updatePlayerStats();
    //}
    //public void decreaseStrength()
    //{
    //    statistics.strength--;
    //    updatePlayerStats();
    //}
    //public void decreaseDexerity()
    //{
    //    statistics.dexerity--;
    //    updatePlayerStats();
    //}
    //public void decreasePower()
    //{
    //    statistics.power--;
    //    updatePlayerStats();
    //}

    //public void saveStatistics()
    //{
    //    statistics.saveStatistics();
    //    updatePlayerStats();
    //}

    //public void loadStatistics()
    //{
    //    statistics.loadStatistics();
    //    updatePlayerStats();
    //}
    #endregion
}
