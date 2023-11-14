using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    // set unique ID for each checkpoint in the unity editor
    public int checkpointID; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnManager.instance.SetCheckpoint(transform.position, checkpointID);
            Debug.Log("Checkpoint Activated: " + checkpointID);
        }
    }
}


