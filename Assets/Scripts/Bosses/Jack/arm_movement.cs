using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class arm_movement : MonoBehaviour
{
        private float max = 13f;
        public float speed = 2f;

        public float startPos;
        public bool forward = true;
        [SerializeField] private arm_controller armCenter;
        [SerializeField] private Transform player;
        private float revUp = 0f;
        [SerializeField] private float rampUpScale;
        // Use this for initialization
        void Start () {
            startPos = transform.position.z;
        }
        // Update is called once per frame
        void FixedUpdate () 
        {
            Move();
        }

        void Move()
        {
            switch(armCenter._currState)
            {
                case arm_controller.ArmState.Idle:
                    // transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time*2,max-min)+min);
                    if(forward)
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-speed*Time.deltaTime);
                        // transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z-speed*Time.deltaTime);
                    else
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+speed*Time.deltaTime);
                        // transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z+speed*Time.deltaTime);

                    if(Mathf.Abs(transform.position.z-startPos) > max || transform.position.z > startPos)
                    // if(transform.localPosition.z < -max || transform.position.z > 0f)
                        forward = !forward;
                    revUp = 0f;
                    break;
                case arm_controller.ArmState.Attack:
                    Debug.Log("Be Still");
                    break;
                case arm_controller.ArmState.Chase:
                    // Lag Behind the Player
                    revUp += rampUpScale;
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, player.position.z), revUp);
                    
                    // transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, player.localPosition.z), revUp);
                    break;
                default:
                    Debug.Log("Movement Default");
                    break;
            }

            // if(armCenter._currState == arm_controller.ArmState.Idle)
            // {
            //     // transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time*2,max-min)+min);
            //     if(forward)
            //         transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-speed*Time.deltaTime);
            //     else
            //         transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+speed*Time.deltaTime);

            //     if(Mathf.Abs(transform.position.z-startPos) > max || transform.position.z > startPos)
            //         forward = !forward;
            // }
        }
}