using UnityEngine;
using TMPro; // For TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int killCount = 0; // Tracks the number of zombies killed
    public TMP_Text killCountText; // Reference to the UI Text for the kill count
    public Transform player; // Reference to the player
    public Transform killTrackerCanvas; // The canvas for the kill tracker

    private Vector3 offset = new Vector3(0, 2, 0); // Position offset above the player

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        if (player != null && killTrackerCanvas != null)
        {
            // Make the killTrackerCanvas follow the player
            killTrackerCanvas.position = player.position + offset;

            // Keep the canvas facing forward
            killTrackerCanvas.rotation = Quaternion.identity;
        }
    }

    public void AddKill()
    {
        // Increment the kill count
        killCount++;

        // Update the kill count UI
        if (killCountText != null)
        {
            killCountText.text = "Kills: " + killCount;
        }
        else
        {
            Debug.LogWarning("KillCountText is not assigned in GameManager.");
        }
    }
}