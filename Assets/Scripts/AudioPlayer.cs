using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingClip;
    [SerializeField] [Range(0.0f, 1.0f)] private float shootingVolume = 1.0f;

    [Header("Explosion")]
    [SerializeField] private AudioClip damageClip;
    [SerializeField] [Range(0.0f, 1.0f)] private float damageVolume = 1.0f;

    [SerializeField] private bool useSingletonPattern = false;
    private static AudioPlayer instance;

    private void Awake()
    {
        if (useSingletonPattern)
        {
            ManageSingleton();
        }
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }
}
