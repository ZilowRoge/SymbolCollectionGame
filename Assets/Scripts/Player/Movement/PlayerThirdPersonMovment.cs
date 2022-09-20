using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThirdPersonMovment : MonoBehaviour
{
    public Transform cam;
    public CharacterController character_controller;
    [Header("Movment")]
    public float walk_speed = 6.0f;
    public float jump_speed = 6.0f;
    public float rotation_speed = 9.0f;
    public float gravity = 9.8f;
    public bool is_double_jump_unlocked = false;

    [Header("Dashing")]
    public bool can_dash = true;
    public float dash_speed = 20.0f;
    private Vector3 dash_direction;
    private float dash_time = 0.2f;
    private float dash_cooldowan = 1.0f;

    private bool can_move = true;
    private bool can_double_jump = true;
    //private float turnSmoothTime = 0.1f;
    //private float turnSmoothVelocity;
    private float vertical_speed = 0;
    private float current_speed;

    private Player.Movement.PlayerCamera player_camera;

    private enum TimerType
    {
        DASH_DURATION,
        DASH_COOLDWON
    }

    private Dictionary<TimerType, Timer> all_timers;

    public float MovmentSpeed {
        get { return current_speed; }
        set { current_speed = value; }
    }
    private void Awake()
    {
        all_timers = new Dictionary<TimerType, Timer>();
        all_timers.Add(TimerType.DASH_DURATION, new Timer(dash_time, true));
        all_timers.Add(TimerType.DASH_COOLDWON, new Timer(dash_cooldowan, true));
        current_speed = walk_speed;
        player_camera = GetComponent<Player.Movement.PlayerCamera>();
    }

    // Update is called once per frame
    public void handleMovment()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float mouse_horizontal = Input.GetAxis("Mouse X");
        float mouse_vertical = Input.GetAxis("Mouse Y");
        player_camera.handleRotations(mouse_vertical, mouse_horizontal);
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveDirection = Vector3.zero;

        if (current_speed == dash_speed)
        {
            direction = dash_direction;
        }


        if (direction.magnitude >= 0.5f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            moveDirection = Quaternion.Euler(0.0f, targetAngle, 0f) * Vector3.forward * current_speed;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && can_dash)
        {
            dash_direction = direction;
            StartCoroutine("dash");
        }

        moveDirection = calculateJumpVector(moveDirection);

        character_controller.Move(moveDirection * Time.deltaTime);

    }

    private Vector3 calculateJumpVector(Vector3 moveDirection)
    {
        if (character_controller.isGrounded)
        {
            vertical_speed = -1;
            can_double_jump = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vertical_speed = jump_speed;
            }
        } else {
            if (is_double_jump_unlocked && can_double_jump && Input.GetKeyDown(KeyCode.Space)) {
                vertical_speed = jump_speed;
                can_double_jump = false;
            }
        }

        vertical_speed -= gravity * Time.deltaTime;
        moveDirection.y = vertical_speed;
        return moveDirection;
    }

    private void rotatePlayer(float horizontal, float vertical)
    {
        float look_angle = rotation_speed * vertical;
        transform.rotation = Quaternion.Euler(0, look_angle, 0);
    }




    public void respawn(Vector3 position)
    {
        character_controller.enabled = false;
        transform.position = position;
        character_controller.enabled = true;

    }

    public void stopMovment()
    {
        can_move = false;
        
    }

    public void startMovment()
    {
        can_move = true;
    }

    public bool canMove()
    {
        return can_move;
    }

    IEnumerator dash()
    {
        can_dash = false;
        current_speed = dash_speed;
        yield return new WaitForSeconds(dash_time);
        current_speed = walk_speed;
        yield return new WaitForSeconds(dash_cooldowan);
        Debug.Log("Cooldown end");
        can_dash = true;
    }
}
