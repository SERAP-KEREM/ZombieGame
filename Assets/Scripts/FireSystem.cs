using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireSystem : MonoBehaviour
{
    public GameObject impactEffect;
    public GameObject bloodImpactEffect;

    [Header("Weapon System")]
    public float reloadcooldown;
    public float AmmoInGun; // silahtaki mermi
    public float AmmoInPocket; //cebimizdeki mermi
    public float AmmoMax; //silah�n alabilece�i mermi
    float AddableAmmo; //ekleyebilece�imiz mermi
    float reloadtimer;
    public Text AmmoCounter;
    public Text PocketAmmoCounter;

    [Header("Target System")]
    RaycastHit hit;
    RaycastHit takeHit;
    public GameObject RayPoint;
    public GameObject MainRP;

    public CharacterController Character;

    Animator GunAnimator;

    public bool CanFire;

    float gunTimer;
    public float gunCooldown;

    public ParticleSystem MuzzleFlash;

    AudioSource SoundSource;
    public AudioClip FireSound;

    public float range;
    public float takeRange;
    public float damage;

    public AudioClip ReloadSound;
    public GameObject cross;




    private void Start()
    {
        SoundSource = GetComponent<AudioSource>();
        GunAnimator = GetComponent<Animator>();

    }


    private void Update()
    {
        GunAnimator.SetFloat("Speed", Character.velocity.magnitude);
        

        // mermi alma
       /* if (Physics.Raycast(MainRP.transform.position, MainRP.transform.forward, out takeHit, takeRange))
        {
            if (takeHit.collider.gameObject.tag == "AmmoBox")
            {
                cross.GetComponent<Image>().material.color = Color.red;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("Mermi");
                    AmmoInPocket = AmmoInPocket + 60;
                    Destroy(takeHit.collider.gameObject);

                }
            }
            else
            {
                cross.GetComponent<Image>().material.color = Color.green;
            }

        }*/
        AmmoCounter.text = AmmoInGun.ToString();
        PocketAmmoCounter.text = AmmoInPocket.ToString();

        AddableAmmo = AmmoMax - AmmoInGun;

        if (AddableAmmo > AmmoInPocket)
        {
            AddableAmmo = AmmoInPocket;
        }

        if (Input.GetKeyDown(KeyCode.R) && AddableAmmo > 0 && AmmoInPocket > 0)
        {
            if (Time.time > reloadtimer)
            {
                StartCoroutine(Reload());
                reloadtimer = Time.time + reloadcooldown;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && CanFire == true && Time.time > gunTimer && AmmoInGun > 0)
        {
            Fire();
            gunTimer = Time.time + gunCooldown;
        }
       
    }

    void Fire()
    {
        AmmoInGun--;
        //ate� etme
        if (Physics.Raycast(RayPoint.transform.position, RayPoint.transform.forward, out hit, range))
        {
            MuzzleFlash.Play();
            SoundSource.Play();
            SoundSource.clip = FireSound;

            GunAnimator.Play("Fire", -1, 0f);
            if (hit.collider.tag == "Untagged")
            {
              //  Instantiate(impactEffect, new Vector3(RayPoint.transform.position.x, RayPoint.transform.position.y, RayPoint.transform.position.z), Quaternion.LookRotation(hit.point));
              Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

            if (hit.collider.tag == "Enemy")
            {
                //  Instantiate(bloodImpactEffect,new Vector3(RayPoint.transform.position.x, RayPoint.transform.position.y,RayPoint.transform.position.z), Quaternion.LookRotation(hit.point));
                Instantiate(bloodImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));

                 hit.collider.gameObject.transform.root.GetComponent<Zombie>().health = hit.collider.gameObject.transform.root.GetComponent<Zombie>().health-damage;
            }



        }
    }

    IEnumerator Reload()
    {
        GunAnimator.SetBool("isReloading", true);
        GunAnimator.Play("Reload");
        // CanFire = false;

        SoundSource.clip = ReloadSound;
        SoundSource.Play();

        yield return new WaitForSeconds(0.3f);
        GunAnimator.SetBool("isReloading", false);

        yield return new WaitForSeconds(1.4f);
        AmmoInGun = AmmoInGun + AddableAmmo;
        AmmoInPocket = AmmoInPocket - AddableAmmo;
    } 
    public void AddAmmo()
    {
        AmmoInPocket = AmmoInPocket + 60;
    }

}
