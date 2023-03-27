using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealt = 100;
    public Slider HealtBar;

    FireSystem fireSystem;


    private void Start()
    {
        fireSystem = GameObject.FindObjectOfType<FireSystem>();
        health = maxHealt;
    }

    public void TakeDamage(int amount)
    {
        health-=amount;
        HealtBar.value = health;
        if (health<=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
       
        if(collider.gameObject.tag.Equals("Heart"))
        { 
            health += 10;
            HealtBar.value = health;
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag.Equals("AmmoBox"))
        {
            fireSystem.AddAmmo();
            Destroy(collider.gameObject);
        }
    }

}
