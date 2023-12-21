using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioClip[] SoundClips;
    [Range(0, 1)][SerializeField] private float volumeShooting;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void ShootSFX()
    {
        SoundFXManager.instance.PlaySoundFXClip(SoundClips[0], transform, volumeShooting);
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if(audioClip == null) { Debug.Log("SFX not played, Audio clip null."); return; }
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position ,Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        if (audioClip.Length <= 0) { Debug.Log("SFX not played, Audio clip null."); return; }

        int rand = Random.Range(0, audioClip.Length);

        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip[rand];
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

}
