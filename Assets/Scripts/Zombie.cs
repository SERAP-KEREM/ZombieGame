using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    Animator ZombieAnim;
    public float speed=10f;

    public Transform Target;
    

    NavMeshAgent Agent;

    public float distance;
    public float health=100f;

    FireSystem fireSystem;


    public PlayerHealth playerHealth;
    public int damage=10;
    public bool isShooting=false;
    public float counter;
    public float proc = 0.5f;



    private void Awake()
    {
        ZombieAnim = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.enabled = true;
        fireSystem = GameObject.FindObjectOfType<FireSystem>();
        Target = GameObject.Find("FPSController").GetComponent<Transform>();
        playerHealth=GameObject.Find("AK47").GetComponent<PlayerHealth>();
      
        counter = proc;
      
    }

    private void Update()
     {
       
        distance = Vector3.Distance(transform.position, Target.position);
         if (health > 0)
         {
             if (distance >3f  )
             {
                ZombieAnim.SetFloat("speed", Agent.velocity.magnitude);
                ZombieAnim.SetFloat("health", health);

                isShooting = false;

                 Agent.enabled = true;
                 Agent.destination = (Target.position);
                 ZombieAnim.SetBool("attack", false);
                 ZombieAnim.SetBool("run", true);
            }
             else if(distance <= 3f)
            {
                ZombieAnim.SetBool("attack", true);
                ZombieAnim.SetBool("run", false);
                isShooting = true;
                counter -= Time.deltaTime;
                if (counter < 0 && isShooting)
                {
                    playerHealth.TakeDamage(damage);
                    counter = proc;
                    
                }
            }
         }
         else
         {
            ZombieAnim.SetBool("isDie",true);
            Destroy(gameObject,4f);
           
        }

     }
}