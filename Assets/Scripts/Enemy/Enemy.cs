using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float max_health;
    public float health;

    private Renderer shader_renderer;

    // Start is called before the first frame update
    void Start()
    {
        health = max_health;
        shader_renderer = GetComponent<Renderer>();
        shader_renderer.material.SetFloat("Fill", health / max_health);
    }

    // Update is called once per frame
    void Update()
    {
        updateShader();
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void onDamage(float damage)
    {
        Debug.Log("Damage");
        health -= damage;
    }

    public void updateShader()
    {
        shader_renderer.material.SetFloat("Fill", -health / max_health);
    }
}
