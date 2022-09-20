using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject[] checkpoint;
    public GameObject player;
    public Transform respawn_point;
    //public GameObject dead_panel;
    private Timer respawn_timer;
    private PlayerThirdPersonMovment player_movment;
    private float player_speed;

    private void Awake()
    {
        
    }
    void Start()
    {
        respawn_timer = new Timer(5.0f, true);
        //respawn_point = GameObject.Find("RespawnPoint").transform;
        //dead_panel.SetActive(false);
        player_movment = player.GetComponent<PlayerThirdPersonMovment>();
        player_speed = player_movment.walk_speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldRespawn())
        {
            Debug.Log("Respawn Player");
            player_speed = player_movment.walk_speed;
            player_movment.respawn(respawn_point.position);
            //dead_panel.SetActive(true);
            respawn_timer.reset();
        }


        if (!respawn_timer.finished())
        {
            respawn_timer.updateForward();
            player_movment.walk_speed = 0;
        }
        else
        {
            player_movment.walk_speed = player_speed;
           // dead_panel.SetActive(false);
        }
    }

    private bool shouldRespawn()
    {
        //Debug.Log("Position.y: " + player.transform.position.y);
        return false; // player.transform.position.y < 0;
    }

    private void loadGame()
    {

        int checkpoint_id = PlayerPrefs.GetInt("LastCheckpointId");
        GameObject last_checkpoint = checkpoint[checkpoint_id];
        respawn_point = last_checkpoint.transform.GetChild(0);
    }
}
