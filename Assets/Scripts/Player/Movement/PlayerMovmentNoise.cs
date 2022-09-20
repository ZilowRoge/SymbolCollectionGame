using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{

    namespace Movement
    {
        public class PlayerMovmentNoise : MonoBehaviour
        {
            public Vector3 last_position;
            public float max_distance_change;
            public float max_sound_range;
            public float range = 0;
            public float range_multiplier = 2;

            public LayerMask target_mask;
            public PlayerMovement movment;

            Dictionary<PlayerMovement.MoveState, float> movment_state_to_range_multiplier;
    
            // Start is called before the first frame update
            void Start()
            {
                last_position = transform.position;
                movment = GetComponent<PlayerMovement>();
                movment_state_to_range_multiplier = new Dictionary<PlayerMovement.MoveState, float>();

                movment_state_to_range_multiplier.Add(PlayerMovement.MoveState.RUN, 20);
                movment_state_to_range_multiplier.Add(PlayerMovement.MoveState.WALK, 10);
                movment_state_to_range_multiplier.Add(PlayerMovement.MoveState.SNEAK, 5);
            }

            // Update is called once per frame
            void FixedUpdate()
            {
                notifiyEnemies();
            }

            private void notifiyEnemies()
            {
        
                if (transform.position != last_position)
                {
                    Vector3 current_position = transform.position;
                    float distance_change_normalized = ((float)(Math.Round(Vector3.Distance(current_position, last_position), 2)) / max_distance_change);
                    //Debug.Log("Normalized: " + distance_change_normalized + " Distance: " + Math.Round(Vector3.Distance(current_position, last_position), 2));
                    range = Mathf.Clamp01(Mathf.Tan(distance_change_normalized)) * movment_state_to_range_multiplier[movment.current_move_state];
                    //Debug.Log("Range:" + range);

                    //foreach (Collider col in Physics.OverlapSphere(transform.position, range, target_mask))
                    //{
                    //    col.gameObject.GetComponent<MonsterController>().notify_sound(transform.position);
                    //}

                    last_position = transform.position;
                }
            }

        }
    } //namespace Movment
} //namespace Player

