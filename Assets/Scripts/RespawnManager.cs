using UnityEngine;
using System.Collections;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;

    private Vector3 lastCheckpointPos = Vector3.zero;
    public Transform defaultRespawnPoint;
    private int lastCheckpointID = -1;
    public GameObject fadeIn;

    private void Start()
    {
        // create a default respawn point if none is set
        if (defaultRespawnPoint == null)
            defaultRespawnPoint = new GameObject("DefaultRespawnPoint").transform;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Vector3 position, int checkpointID)
    {
        if (checkpointID > lastCheckpointID)
        {
            lastCheckpointPos = position;
            lastCheckpointID = checkpointID;
        }
    }

    public Vector3 GetLastCheckpointPosition()
    {
        return lastCheckpointPos;
    }

    public int GetLastCheckpointID()
    {
        return lastCheckpointID;
    }

    public IEnumerator RespawnPlayer(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        // add death animation
        fadeIn.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        playerHealth.isDead = true;

        //if (lastCheckpointPos != Vector3.zero && player != null) // Commented out so it wouldn't go to main menu if player dies in the first region
        //{
        if (player != null)
        {
                Vector3 respawnPosition = lastCheckpointPos != Vector3.zero ? lastCheckpointPos : defaultRespawnPoint.position;
            player.transform.position = lastCheckpointPos;
            Debug.Log("Player respawned");

            // reset player health
            //PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ResetHealth();
            }
            else
            {
                Debug.LogError("PlayerHealth component not found");
            }
        }
        else
        {
            SceneLoader.instance.Load(SceneLoader.Scene.MainMenu);
        }
        yield return new WaitForSeconds(1.0f);
        fadeIn.SetActive(false);
        playerHealth.isDead = true;
    }
}
