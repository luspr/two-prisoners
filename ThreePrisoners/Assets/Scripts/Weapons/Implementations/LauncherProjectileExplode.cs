using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherProjectileExplode : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosionEffect;
    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private int maxDamage = 70;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 52)//destroy after explosion
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (timer > 0.02)//dont collide with cylinder
        {
            explosionEffect.Play();
            Destroy(GetComponent<Rigidbody>());
            timer = 50;                         //keep track of when explosion started

            //explode
            Collider[] touched = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider element in touched)        //syntax
            {
                Health healthScript = element.gameObject.GetComponent<Health>();
                if (healthScript != null)
                {
                    healthScript.TakeDamage(maxDamage);
                }
            }
        }
    }

}
