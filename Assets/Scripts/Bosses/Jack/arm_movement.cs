using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm_movement : MonoBehaviour
{
        public float min=2f;
        public float max=3f;
        [SerializeField] private arm_controller armCenter;
        // Use this for initialization
        void Start () {
            // min=transform.position.x;
            // max=transform.position.x+3;
        }
        // Update is called once per frame
        void Update () {
            if (armCenter._currState == arm_controller.ArmState.Idle)
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time*2,max-min)+min);
        }
}
