using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip flipSound, matchSound, mismatchSound, gameOverSound, music1, music2;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        //musicSource.PlayOneShot(music1);
       
    }

    public void PlayFlipSound()
    {
        audioSource.PlayOneShot(flipSound);
    }

    public void PlayMatchSound()
    {
        audioSource.PlayOneShot(matchSound);
    }

    public void PlayMismatchSound()
    {
        audioSource.PlayOneShot(mismatchSound);
    }

    public void PlayGameOverSound()
    {
        audioSource.PlayOneShot(gameOverSound);
    }
}
