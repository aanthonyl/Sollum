using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm_movement : MonoBehaviour
{
        private float max = 11f;
        public float speed = 2f;

        public float startPos;
        public bool forward = true;
        [SerializeField] private arm_controller armCenter;
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
            if(armCenter._currState == arm_controller.ArmState.Idle)
            {
                // transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time*2,max-min)+min);
                if(forward)
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-speed*Time.deltaTime);
                else
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+speed*Time.deltaTime);

                if(Mathf.Abs(transform.position.z-startPos) > max || transform.position.z > startPos)
                    forward = !forward;
            }
        }
}