using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Serialization;

public class SkillNodeUI : MonoBehaviour
{
    public int id;
    public Color inactive_color;
    public Color active_color;
    public Color highlight_color = Color.white;
    public Color selected_color = Color.black;
    public Image background;
    public Image skill_image;
    public GameObject tooltip;
    PlayerSkillTree skill_tree;
    SkillStatsDatabase skill_db;

    private void Start()
    {
        skill_tree = GameObject.Find("Player").GetComponent<PlayerSkillTree>();
        skill_db = GameObject.Find("GameManager").GetComponent<SkillStatsDatabase>();
        updateNode();
    }

    public void onClick()
    {
        if ((SkillActivityState)skill_tree.getSkill(id).state == SkillActivityState.ACTIVE)
        {
            skill_tree.setSkillActivityState(id, SkillActivityState.UNLOCKED);
            skill_image.color = active_color;
            background.color = selected_color;
            background.gameObject.SetActive(true);
            updateNode();
        }
    }

    public void onPointerEnter()
    {
        if((SkillActivityState)skill_tree.getSkill(id).state == SkillActivityState.ACTIVE)
        {
            background.color = highlight_color;
            background.gameObject.SetActive(true);
        }
        showTooltip();
    }

    public void onPointerExit()
    {
        if ((SkillActivityState)skill_tree.getSkill(id).state == SkillActivityState.ACTIVE)
        {
            background.gameObject.SetActive(false);
        }
        hideToolTip();
    }


    public void updateNode()
    {
        SkillActivityState skill_state = (SkillActivityState)skill_tree.getSkill(id).state;
        switch(skill_state)
        {
            case SkillActivityState.ACTIVE:
                skill_image.color = active_color;
                break;
            case SkillActivityState.INACTIVE:
                skill_image.color = inactive_color;
                break;
            case SkillActivityState.UNLOCKED:
                skill_image.color = active_color;
                background.gameObject.SetActive(true);
                break;
        }
    }

    private void showTooltip()
    {
        setTooltipPosition();
        SkillTooltipUI tooltip_script = tooltip.GetComponent<SkillTooltipUI>();
        tooltip_script.size_text.text = skill_db.getSkillStat(id).getToolTip();
        tooltip_script.visual_text.text = tooltip_script.size_text.text;
    }

    private void hideToolTip()
    {
        tooltip.SetActive(false);
    }

    private void setTooltipPosition()
    {
        var rect_transform = GetComponent<RectTransform>();
        var foreground_rect = skill_image.GetComponent<RectTransform>();
        var width = foreground_rect.rect.width;
        var height = foreground_rect.rect.height;
        tooltip.SetActive(true);
        tooltip.GetComponent<RectTransform>().position = new Vector3(rect_transform.position.x + width / 2 + 5, rect_transform.position.y - height / 2 - 5, rect_transform.position.z);

    }
}
