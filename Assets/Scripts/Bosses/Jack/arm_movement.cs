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
            startPos = transform.localPosition.z;
        }
        // Update is called once per frame
        void FixedUpdate () 
        {
            Move();
        }

    void Move()
    {
        switch (armCenter._currState)
        {
            case arm_controller.ArmState.Idle:
                // transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.PingPong(Time.time*2,max-min)+min);
                if(forward)
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z-speed*Time.deltaTime);
                    // g = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z-speed*Time.deltaTime);
                else
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z+speed*Time.deltaTime);
                    // g = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z+speed*Time.deltaTime);

                if(Mathf.Abs(transform.localPosition.z-startPos) > max || transform.localPosition.z > startPos)
                // if(g.z < -max || transform.localPosition.z > 0f)
                    forward = !forward;
                revUp = 0f;
                break;
            case arm_controller.ArmState.Attack:
                // Debug.Log("Be Still");
                break;
            case arm_controller.ArmState.Chase:
                // Lag Behind the Player
                revUp += rampUpScale;
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, player.position.z), revUp);
                
                // g = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, player.localPosition.z), revUp);
                break;
            default:
                // Debug.Log("Movement Default");
                break;
        }

            // if(armCenter._currState == arm_controller.ArmState.Idle)
            // {
            //     // transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.PingPong(Time.time*2,max-min)+min);
            //     if(forward)
            //         transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z-speed*Time.deltaTime);
            //     else
            //         transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z+speed*Time.deltaTime);

            //     if(Mathf.Abs(transform.localPosition.z-startPos) > max || transform.localPosition.z > startPos)
            //         forward = !forward;
            // }
    }
}
