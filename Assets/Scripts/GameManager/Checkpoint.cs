using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpoint_id;
    void Start()
    {
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            if (collision.gameObject.GetComponent<CharacterController>().isGrounded) {
                onSave(collision.transform);
            }
        }
    }

    public void onSave(Transform player)
    {
        Debug.Log("Save");
        PlayerPrefs.SetInt("LastCheckpointId", checkpoint_id);
        GameObject.Find("GameManager").transform.GetChild(0).position = transform.GetChild(0).position;
    }

    public void onLoad(Transform player)
    {
        GameObject.Find("GameManager").transform.GetChild(0).position = transform.GetChild(0).position;
        player.transform.position = transform.GetChild(0).position;
    }
}
