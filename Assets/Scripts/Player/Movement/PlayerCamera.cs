using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player {
    namespace Movement {
        [System.Serializable]
        public class CameraMode
        {
            public Vector3 position;
            public float field_of_view;
            public float camera_move_speed;
            public float camera_rotation_speed;
            public float distance_from_player;
        }

        public class PlayerCamera : MonoBehaviour {
            public float rotation_speed = 1.0f;
            public Transform camera_pivot;
            public CameraMode normal_mode;
            public CameraMode bow_aiming_mode;
            

            float turn_smoothing = 0.1f;
            //float min_angle = -35.0f;
            //float max_angle = 35.0f;

            float smooth_x;
            float smooth_y;
            float smooth_x_velocity;
            float smooth_y_velocity;
            float tilt_angle;

            List<GameObject> player_cameras;
            public void Start()
            {
                player_cameras = new List<GameObject>();
                player_cameras.Add(GameObject.Find("PlayerCamera"));
                player_cameras.Add(GameObject.Find("PlayerCamera (1)"));
            }

            public void handleRotations(float mouse_vertical, float mouse_horizontal)
            {
                if (turn_smoothing > 0)
                {
                    smooth_x = Mathf.SmoothDamp(smooth_x, mouse_vertical, ref smooth_x_velocity, turn_smoothing);
                    smooth_y = Mathf.SmoothDamp(smooth_y, mouse_horizontal, ref smooth_y_velocity, turn_smoothing);
                }
                else
                {
                    smooth_x = mouse_vertical;
                    smooth_y = mouse_horizontal;
                }

                //if (is_camera_locked)
                //{

                //}
                transform.Rotate(0, smooth_y * rotation_speed, 0);
                tilt_angle = -smooth_x * rotation_speed;
                camera_pivot.Rotate(tilt_angle, 0, 0);
                if (camera_pivot.transform.localRotation.eulerAngles.x > 30 && camera_pivot.transform.localRotation.eulerAngles.x < 180)
                {
                    camera_pivot.transform.localRotation = Quaternion.Euler(30, camera_pivot.transform.localRotation.eulerAngles.y, camera_pivot.transform.localRotation.eulerAngles.z);
                }
                else if (camera_pivot.transform.localRotation.eulerAngles.x < 280 && camera_pivot.transform.localRotation.eulerAngles.x > 180)
                {
                    camera_pivot.transform.localRotation = Quaternion.Euler(280, camera_pivot.transform.localRotation.eulerAngles.y, camera_pivot.transform.localRotation.eulerAngles.z);
                }
            }

            private void setCamerasFieldOfView(float camera_field_of_view)
            {

                foreach(Camera cam in transform.GetComponentsInChildren<Camera>())
                {
                    
                }
            }

        }
    } // namespace Control
} // namespace Player

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    namespace Movement
    {
        namespace CameraControll
        {
            struct CamerasHandle
            {
                public CamerasHandle(GameObject cam1, GameObject cam2)
                {
                    foreground = cam1;
                    background = cam2;
                    foreground_transform = cam1.transform;
                    background_transform = cam2.transform;
                }
                public GameObject foreground;
                public GameObject background;
                public Transform foreground_transform;
                public Transform background_transform;
            }
        }
        public class PlayerCamera : MonoBehaviour
        {
            public bool is_camera_locked;
            public float rotation_speed = 10.0f;
            public float follow_speed = 9.0f;

            float turn_smoothing = 0.1f;
            float min_angle = -35.0f;
            float max_angle = 35.0f;

            float smooth_x;
            float smooth_y;
            float smooth_x_velocity;
            float smooth_y_velocity;
            float look_angle;
            float tilt_angle;

            CameraControll.CamerasHandle cameras;
            Transform camera_transform;
            Transform camera_pivot;
            Transform target;
            public void Start()
            {
                cameras = new CameraControll.CamerasHandle(GameObject.Find("ForegroundPlayerCamera"), GameObject.Find("BackgroundPlayerCamera"));
                camera_pivot = camera_transform.parent;
                target = this.transform.parent;
            }
            public void set_target(Transform target)
            {
                this.target = target;
            }

            public void follow_target()
            {
                Vector3 targert_position = Vector3.Lerp(this.transform.position, target.position, follow_speed);
                transform.position = targert_position;
            }

            public void handle_rotations(float mouse_vertical, float mouse_horizontal)
            {
                if (turn_smoothing > 0)
                {
                    smooth_x = Mathf.SmoothDamp(smooth_x, mouse_vertical, ref smooth_x_velocity, turn_smoothing);
                    smooth_y = Mathf.SmoothDamp(smooth_y, mouse_horizontal, ref smooth_y_velocity, turn_smoothing);
                }
                else
                {
                    smooth_x = mouse_vertical;
                    smooth_y = mouse_horizontal;
                }

                if (is_camera_locked)
                {

                }

                look_angle += smooth_x * rotation_speed;
                transform.rotation = Quaternion.Euler(0, look_angle, 0);

                tilt_angle -= smooth_y * rotation_speed;
                tilt_angle = Mathf.Clamp(tilt_angle, min_angle, max_angle);
                camera_pivot.localRotation = Quaternion.Euler(tilt_angle, 0, 0);
            }

            public GameObject get_player_camera()
            {
                return player_camera;
            }

        }
    } // namespace Control
} // namespace Player
*/

