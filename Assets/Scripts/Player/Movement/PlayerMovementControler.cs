using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player {
    namespace Movement {
        public class PlayerManager : MonoBehaviour {
            PlayerCamera player_camera;
            PlayerMovement player_movment;
            private void Start()
            {
                player_camera = GameObject.Find("CameraHolder").GetComponent<PlayerCamera>();
                player_movment = GetComponent<PlayerMovement>();
            }
            void Update()
            {
                handlePlayerMovement();
                handlePlayerCamera();
            }

            private void handlePlayerMovement()
            {
                float vertical = Input.GetAxis("Vertical");
                float horizontal = Input.GetAxis("Horizontal");
                bool is_running = Input.GetKey(KeyCode.LeftShift);
                bool is_sneaking = Input.GetKey(KeyCode.LeftControl);

                PlayerMovement.MoveState move_state = is_running ? PlayerMovement.MoveState.RUN : is_sneaking ? PlayerMovement.MoveState.SNEAK : PlayerMovement.MoveState.WALK;
                

                Vector3 move_direction = normalizeMoveDirection(vertical, horizontal);
                float move_amount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

                player_movment.movePlayer(move_direction, move_amount);

                if (Input.GetKeyDown(KeyCode.Space)) {
                    Debug.Log("KeyPressed");
                    player_movment.jump();
                }

                if (Input.GetKey(KeyCode.Return)) {
                    transform.position = new Vector3(0, 10, 0);
                }
            }

            private void handlePlayerCamera()
            {
                float mouse_vertical = Input.GetAxis("Mouse X");
                float mouse_horizontal = Input.GetAxis("Mouse Y");

                rotate_camera(mouse_vertical, mouse_horizontal);
            }

            public void rotate_camera(float mouse_vertical, float mouse_horizontal)
            {
                player_camera.handleRotations(mouse_vertical, mouse_horizontal);
            }

            private Vector3 normalizeMoveDirection(float vertical, float horizontal)
            {
                Vector3 vertical_fixed = vertical * player_camera.transform.forward;
                Vector3 horizontal_fixed = horizontal * player_camera.transform.right;
                Vector3 move_direction = (vertical_fixed + horizontal_fixed);
                move_direction.Normalize();
                return move_direction;
            }

        }
    } //namespace Movement
} // namespace Player

