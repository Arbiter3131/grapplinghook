using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    float musicTimer;

    [SerializeField] AudioClip music;

    float currentLength;

    protected AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        PlayMusic();
    }

    void PlayMusic()
    {
        if(music != null)
        {
            musicTimer = Time.time;
            currentLength = music.length;
            audioSource.PlayOneShot(music);
        }
    }

    void Update()
    {
        if(music != null && Time.time > musicTimer + currentLength)
        {
            PlayMusic();
        }
    }
}
