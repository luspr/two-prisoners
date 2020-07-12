using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWRifle: MonoBehaviour, IWeapon
{

    [SerializeField]
    private float timeBetweenShots = 1;

    [SerializeField]
    private int damage = 15;

    [SerializeField]
    private ParticleSystem[] shootyParticles;
    [SerializeField]
    private ParticleSystem hitParticlesPrefab;
    [SerializeField]
    private ParticleSystem bloodParticlesPrefab;
    [SerializeField]
    private AudioClip fireAudioClip;


    private AudioSource fireAudioSource;
    public float reloadTimer;


    void Start()
    {
        fireAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        reloadTimer += Time.deltaTime;
    }


    public void Fire()
    {
        if (reloadTimer > timeBetweenShots) {
            for (int i = 0; i < shootyParticles.Length; i++)
            {
                shootyParticles[i].Play();
            }
            fireAudioSource.PlayOneShot(fireAudioClip);
            Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Health targetHealth = hit.transform.GetComponent<Health>();
                if (targetHealth != null)
                {
                    ParticleSystem bloodParticles = Instantiate(bloodParticlesPrefab, hit.point, Quaternion.identity);
                    bloodParticlesPrefab.Play();
                    targetHealth.TakeDamage(damage);
                }
                else
                {
                    ParticleSystem particles = Instantiate(hitParticlesPrefab, hit.point, Quaternion.identity);
                    particles.Play();
                }
            }
            reloadTimer = 0;
        }
    }
}
