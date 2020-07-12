using UnityEngine;
using UnityEngine.AI;


public class AISimpleAttack : MonoBehaviour
{
    // NOTE: This is just a simple, very much hardcoded prototype. 
    // Should definitely be refactored for any further serious development
    [SerializeField]
    private Transform AttackTarget;
    [SerializeField]
    private float rate = 1f;
    [SerializeField]
    private float range = 10f;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float HitProbability = 0.1f;
    [SerializeField]
    private Transform ShotOrigin;

    // Effects
    [SerializeField]
    private ParticleSystem[] shootyParticles;
    [SerializeField]
    private ParticleSystem hitParticlesPrefab;
    [SerializeField]
    private ParticleSystem bloodParticlesPrefab;
    [SerializeField]
    private AudioClip fireAudioClip;


    private AudioSource fireAudioSource;
    private float attackTimer;


    void Start()
    {
        attackTimer = rate;
        fireAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        /* 
        Simple AI attack
        * Check player in range
        * Attack Player, hitting with certain probability.
        TODO: this should be more general, s.t. we can change the type of attack 
        that is being performed via dependency injection.  */
        attackTimer += Time.deltaTime;
        if (attackTimer >= rate)
        {
            if (Vector3.Distance(transform.position, AttackTarget.position) < range)
            {
                attackTimer = 0f;
                Fire();
            }
        }
    }

    private void Fire()
    {
        for (int i = 0; i < shootyParticles.Length; i++)
        {
            shootyParticles[i].Play();
        }
        Vector3 shootAt = AttackTarget.position;
        if (Random.value < HitProbability)
        {
            // Hit Player.
            shootAt.y = shootAt.y + 0.5f;
        }
        else
        {
            // Shoot close to, but not directly at, player.
            float offset = Random.Range(1f, 2f);
            shootAt.x += offset;

        }
        RaycastHit hit;
        Vector3 direction = shootAt - ShotOrigin.position;
        // Debug.DrawRay(ray.origin, ray.direction * 5, Color.red, 5f);
        // if (Physics.Raycast(ray, out hit, 100))
        if (Physics.Raycast(ShotOrigin.position, direction, out hit, 100))
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
    }

}