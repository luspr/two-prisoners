using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ChargeState
{
    Idle,
    Charging,
    Cooldown
};

public class RailGun : MonoBehaviour, IWeapon
{

    [SerializeField]
    private float reloadDuration = 1;
    [SerializeField]
    private float chargeCap = 3;
    [SerializeField]
    private float autoRelease = 5;
    [SerializeField]
    private int maxDamage = 50;
    [SerializeField]
    private int minDamage = 15;

    [SerializeField]
    private ParticleSystem[] chargeParticles;
    [SerializeField]
    private ParticleSystem laserBeamParticles;
    [SerializeField]
    private ParticleSystem[] laserShotParticles;
    [SerializeField]
    private AudioClip fireAudioClip;
    [SerializeField]
    private AudioClip chargeAudioClip;

    private AudioSource fireAudioSource;
    private Vector3 weaponDistance;

    private float beamWidth;    
    private LineRenderer lr;

    private ChargeState chargeState = ChargeState.Idle;   
    private float timer;
    private float intensity;
    private bool audioFlag;         //started playing charge audio
    private bool input = false;


    void Awake()
    {
        fireAudioSource = GetComponent<AudioSource>();
        lr = GetComponent<LineRenderer>();

        Transform childPos = transform.GetChild(0);       //weapon nozzle
        weaponDistance = Quaternion.Inverse(transform.rotation) * (childPos.position - transform.position);     //distance between weapon and player transform in relative coordinates

    }

    void LateUpdate()
    {
        //state machine
        switch (chargeState)
        {
            case ChargeState.Idle:
                lr.SetWidth(0f, 0f);       //remove beam
                if (input) { chargeState = ChargeState.Charging;  }        //start charging
                break;

            case ChargeState.Charging:
                timer += Time.deltaTime;
                intensity = Mathf.Min(chargeCap, timer) / chargeCap;

                for (int i = 0; i < chargeParticles.Length; i++)    //play all charging particles
                {
                    chargeParticles[i].Play();
                }

                if(audioFlag == false)               //play audio once
                {
                   
                    fireAudioSource.PlayOneShot(chargeAudioClip);
                    audioFlag = true;
                }


                if (!input||timer>autoRelease)     //either release of mouse or reached autorelease: shoot with according intensity
                {
                    fireAudioSource.Stop();
                    CreateLaserBeam();
                    chargeState = ChargeState.Cooldown;
                    timer = 0;
                    audioFlag = false;

                    for (int i = 0; i < chargeParticles.Length; i++)    //stop all charging particles
                    {
                        chargeParticles[i].Stop();
                    }

                }
                break;

            case ChargeState.Cooldown:                             
                timer += Time.deltaTime;

                beamWidth = Mathf.Max(0.05f * (1f - timer / 0.4f),0f); //make beam shrink
                lr.SetWidth(beamWidth, beamWidth);
                

                if (timer >= 0.4)            //remove shot particles
                {
                    for (int i = 0; i < laserShotParticles.Length; i++) //stop partices TODO: shoot particles need to not loop in the first place
                    {
                        laserShotParticles[i].Stop();
                    }
                    lr.SetWidth(0f,0f);       //remove beam

                }

                if(timer >= reloadDuration) //after reloading go back to waiting for charge
                {
                    chargeState = ChargeState.Idle;
                    timer = 0;
                }
                break;
        }
        input = false;
    }


    public void Fire()
    {
        input = true;
    }


    public void CreateLaserBeam()
    {
        for (int i = 0; i < laserShotParticles.Length; i++)
        {
            laserShotParticles[i].Play();
        }

        fireAudioSource.PlayOneShot(fireAudioClip);

        Vector3 offset = transform.rotation * weaponDistance;           //rotate weapon player distance     
        Vector3 beamOrigin = transform.position + offset;       //this is where the weapon is relative to the player

        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;
        Vector3 beamTerminus;

        if (Physics.Raycast(ray, out hit, 500))
        {
            beamTerminus = hit.point;   //laser destination
            Health targetHealth = hit.transform.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage((int)(minDamage + intensity * (maxDamage - minDamage)));
            }
        }
        else
        {
            beamTerminus = ray.origin + ray.direction * 500;
        }

        beamWidth = 0.05f;
        lr.SetWidth(beamWidth, beamWidth);
        lr.SetPosition(0, beamOrigin);
        lr.SetPosition(1, beamTerminus);
    }
}
