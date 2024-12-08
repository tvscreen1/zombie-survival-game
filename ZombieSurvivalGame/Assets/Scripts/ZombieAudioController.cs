using UnityEngine;

public class ZombieAudioController : MonoBehaviour
{
    public AudioSource audioSource;   // Assign in Inspector
    public AudioClip zombieMoanClip;  // Assign zombie sound here
    
    private float minInterval = 5f;   // Minimum time between moans
    private float maxInterval = 15f;  // Maximum time between moans
    private float timer;

    void Start()
    {
        // Set the initial timer to a random interval before the first moan
        timer = Random.Range(minInterval, maxInterval);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // Play moan
            audioSource.PlayOneShot(zombieMoanClip);
            
            // Reset timer for the next moan
            timer = Random.Range(minInterval, maxInterval);
        }
    }
}