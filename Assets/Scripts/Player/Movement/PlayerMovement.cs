using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player {
    namespace Movement {
         
        public class PlayerMovement : MonoBehaviour {
            Rigidbody player_rigidbody;
            //CharacterController player_controller;
            CapsuleCollider player_collider;
            Progress max_speed_progress;
            public enum MoveState
            {
                RUN,
                SNEAK,
                WALK
            }
            bool on_ground = true;
            float distance_to_ground = -0.1f;
            Timer jump_cooldown_timer;
            public float sneak_speed;
            public float walk_speed;
            public float run_speed;
            public float jump_force;
            public float jump_cooldown;
            public float rotation_speed;
            public LayerMask ignore_layer = ~(1 << 10);
            public MoveState current_move_state = MoveState.WALK;


            float max_speed;
            Dictionary<MoveState, float> move_state_to_speed;

            // Start is called before the first frame update
            void Start()
            {
                player_rigidbody = GetComponent<Rigidbody>();
                player_collider = GetComponent<CapsuleCollider>();
                max_speed_progress = new Progress(2);
                move_state_to_speed = new Dictionary<MoveState, float>();
                jump_cooldown_timer = new Timer(jump_cooldown);

                move_state_to_speed.Add(MoveState.RUN,   run_speed);
                move_state_to_speed.Add(MoveState.SNEAK, sneak_speed);
                move_state_to_speed.Add(MoveState.WALK, walk_speed);
                max_speed = walk_speed;
            }

            private void Update()
            {
                if (!jump_cooldown_timer.finished()) {
                    Debug.Log(jump_cooldown_timer.get_time());
                    jump_cooldown_timer.updateForward();
                }
                Debug.Log("On Ground: " + isGrounded());
                //isGrounded();
                limitSpeed();
            }
            public void setMovingState(MoveState move_state)
            {
                current_move_state = move_state;
                max_speed = move_state_to_speed[current_move_state];
            }
            public void movePlayer(Vector3 move_direction, float move_amount)
            {
                configureRigidbodyDrag(move_amount);
               if (isGrounded()) {
                    player_rigidbody.velocity = move_direction * move_state_to_speed[current_move_state] * move_amount  + new Vector3(0, player_rigidbody.velocity.y, 0); //move_amount is for controler's joystick tilt
               }

                //rotatePlayer(move_direction, move_amount);
            }

            public void jump()
            {
                if (jump_cooldown_timer.finished()) {
                   // player_rigidbody.drag = 0;
                    //Debug.Log("Before add force: " + player_rigidbody.velocity.y);
                   player_rigidbody.velocity = new Vector3(player_rigidbody.velocity.x, jump_force, player_rigidbody.velocity.z);
                    //player_rigidbody.AddForce(Vector3.up * jump_force * Time.deltaTime, ForceMode.Impulse);
                   // Debug.Log("After add force: " + player_rigidbody.velocity.y);
                   // player_rigidbody.drag = 0;
                    jump_cooldown_timer.reset();
                }
            }

            public bool isGrounded()
            {
                bool is_grounded = Physics.CheckCapsule(player_collider.bounds.center, new Vector3(player_collider.bounds.center.x, player_collider.bounds.min.y, player_collider.bounds.center.z), player_collider.radius * 0.51f, ignore_layer); // 0.51f is for not exceeding the slope collider

                Debug.DrawLine(player_collider.bounds.center, new Vector3(player_collider.bounds.center.x, player_collider.bounds.min.y, player_collider.bounds.center.z), Color.red);
                Debug.DrawLine(player_collider.bounds.center, new Vector3(player_collider.bounds.center.x, player_collider.bounds.center.y, player_collider.bounds.center.z + player_collider.radius * 0.51f)); 
                return is_grounded;
            }
            private void rotatePlayer(Vector3 move_direction, float move_amount)
            {
                Vector3 target_direction = move_direction;
                target_direction.y = 0;
                if (target_direction == Vector3.zero) {
                    target_direction = transform.forward;
                }
                Quaternion rotation = Quaternion.LookRotation(target_direction);
                Quaternion target_rotation = Quaternion.Slerp(transform.rotation, rotation, rotation_speed * move_amount);

                transform.rotation = target_rotation;
            }

            public void limitSpeed()
            {
                if (Mathf.Abs(player_rigidbody.velocity.x) > max_speed || Mathf.Abs(player_rigidbody.velocity.y) > max_speed)
                {
                    // clamp velocity:
                    Vector3 newVelocity = player_rigidbody.velocity.normalized;
                    newVelocity *= max_speed;
                    player_rigidbody.velocity = new Vector3(newVelocity.x, player_rigidbody.velocity.normalized.y * jump_force, newVelocity.z);
                }
            }

            private void configureRigidbodyDrag(float move_amount)
            {
                player_rigidbody.drag = (move_amount > 0 || !on_ground) ? 0 : 4;
            }

            private bool isGrounded1()
            {
                bool grounded = false;

                Vector3 origin = this.transform.position + (Vector3.up * distance_to_ground);
                Vector3 main_direction = -Vector3.up;
                float distance = distance_to_ground + 1.2f;
                RaycastHit hit;
                Debug.DrawRay(origin, main_direction, Color.black, distance);
                Debug.DrawRay(origin, new Vector3(0, -1, -0.3f), Color.red, distance + 0.5f);
                Debug.DrawRay(origin, new Vector3(0, -1, 0.3f), Color.red, distance + 0.5f);
                Debug.DrawRay(origin, new Vector3(-0.3f, -1, 0), Color.red, distance + 0.5f);
                Debug.DrawRay(origin, new Vector3(0.3f, -1, 0), Color.red, distance + 0.5f);

                grounded = Physics.Raycast(origin, main_direction, out hit, distance, ignore_layer) ||
                           Physics.Raycast(origin, new Vector3(0, -1, -0.3f), out hit, distance, ignore_layer) ||
                           Physics.Raycast(origin, new Vector3(0, -1, 0.3f), out hit, distance, ignore_layer) ||
                           Physics.Raycast(origin, new Vector3(-0.3f, -1, 0), out hit, distance, ignore_layer) ||
                           Physics.Raycast(origin, new Vector3(0.3f, -1, 0), out hit, distance, ignore_layer);
                Debug.Log("Is grounded: " + grounded);
                return grounded;
            }

            private void OnCollisionEnter(Collision collision)
            {
                if (collision.gameObject.layer == LayerMask.GetMask("Level"))
                {
                    player_rigidbody.velocity = new Vector3(player_rigidbody.velocity.x, Physics.gravity.y, player_rigidbody.velocity.z);
                }
            }

        }
    }
}
