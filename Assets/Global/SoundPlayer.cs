using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource audioSource;

    // list of playable audioclips, assign in prefab.
    public AudioClip[] audioClips;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = transform.Find("Audio Source").GetComponent<AudioSource>();
    }

    void PlaySound(AudioClip clip)
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void AttemptPlaySound(string name)
    {
        foreach (AudioClip sound in audioClips)
        {
            // UnityEngine.Debug.Log(sound.name);
            if(sound.name == name)
            {
                PlaySound(sound);
                return;
            }
        }
        UnityEngine.Debug.Log("Sound not found");
    }
}
