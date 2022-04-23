using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10.0f;
    [SerializeField] private float projectileLifetime = 5.0f;
    [SerializeField] private float baseFiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] private bool useAi = false;
    [SerializeField] private float firingRateVariance = 0.0f;
    [SerializeField] private float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;
    private Coroutine firingCoroutine;
    private AudioPlayer audioPlayer;

    private void Start()
    {
        isFiring = useAi;
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, transform.rotation);
            Destroy(instance, projectileLifetime);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(GetRandomFiringRate());
        }
    }

    public float GetRandomFiringRate()
    {
        float timeToNextProjectile= Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
        return Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
    }
}
