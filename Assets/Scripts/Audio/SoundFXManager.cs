using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioClip[] SoundClips;
    [SerializeField][Range(0, 1)] private float _volumeShootingSFX;
    [SerializeField][Range(0, 1)] private float _enemyDiedVolume;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void ShootingSFX(int index)
    {
        PlaySoundFXClip(SoundClips[index], transform, _volumeShootingSFX);
    }
    private void EnemyDiedSFX()
    {
        PlaySoundFXClip(SoundClips[2], transform, _enemyDiedVolume);
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

    private void OnEnable()
    {
        Shooting.onShootingSFX += ShootingSFX;
        Entity.onEnemyDie += EnemyDiedSFX;
    }

    private void OnDisable()
    {
        Shooting.onShootingSFX -= ShootingSFX;
        Entity.onEnemyDie -= EnemyDiedSFX;
    }

}
