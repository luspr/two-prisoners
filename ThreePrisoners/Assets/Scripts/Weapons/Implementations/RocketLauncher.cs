using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour, IWeapon
{

    [SerializeField]
    private float timeBetweenShots = 1.5f ;
    [SerializeField]
    private GameObject projectile;

    private AudioSource fireAudioSource;
    private Transform nozzlePos;
    private Vector3 weaponDistance;

    private float reloadTimer;


    void Awake()
    {
        fireAudioSource = GetComponent<AudioSource>();

        nozzlePos = transform.GetChild(0);       //weapon nozzle
        weaponDistance = Quaternion.Inverse(transform.rotation) * (nozzlePos.position - transform.position);     //distance between weapon and player transform in relative coordinates
        reloadTimer = 1f;

    }


    void Update()
    {
        reloadTimer += Time.deltaTime;
    }

    public void Fire()
    {
        if (reloadTimer > timeBetweenShots)
        {
            Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
            Vector3 direction = Vector3.Normalize(ray.direction);

            GameObject shot = Instantiate(projectile, nozzlePos.position + new Vector3(0f, 0.3f, 0f), nozzlePos.rotation);  //instanziiert ein projektil an der stelle der waffe
            shot.GetComponent<Rigidbody>().AddForce(4000 * (direction + new Vector3(0f, 0.005f, 0f)));

            reloadTimer = 0;
        }  
    }
}
