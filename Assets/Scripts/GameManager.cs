using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject zombie;
    public GameObject Heart;
    public GameObject AmmoBox;

    public GameObject[] SpawnPoints; 
    GameObject currentPoint;

    private float countZombie;
    private float procZombie=5f;
    private int index;
    private float countHeart;
    private float procHeart = 60f;
    private float countAmmo;
    private float procAmmo = 60f;
   
    private void Start()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        
        countZombie = procZombie;
        countHeart = procHeart;
        countAmmo = procAmmo;
    }

    private void Update()
    {
        countZombie -= Time.deltaTime;
        if (countZombie < 0 )
        {
            index = Random.Range(0, SpawnPoints.Length);
            currentPoint = SpawnPoints[index];
           Vector3 pos=new Vector3(currentPoint.transform.position.x,currentPoint.transform.position.y,currentPoint.transform.position.z);

            Instantiate(zombie, pos, Quaternion.identity);
            
            countZombie = procZombie;
        }
        countHeart -= Time.deltaTime;
        if (countHeart < 0)
        {
            index = Random.Range(0, SpawnPoints.Length);
            currentPoint = SpawnPoints[index];
            Vector3 pos = new Vector3(currentPoint.transform.position.x, currentPoint.transform.position.y, currentPoint.transform.position.z);

            Instantiate(Heart, pos, Quaternion.identity);

            countHeart = procHeart;
        }
        countAmmo -= Time.deltaTime;
        if (countAmmo < 0)
        {
            index = Random.Range(0, SpawnPoints.Length);
            currentPoint = SpawnPoints[index];
            Vector3 pos = new Vector3(currentPoint.transform.position.x, currentPoint.transform.position.y, currentPoint.transform.position.z);

            Instantiate(Heart, pos, Quaternion.identity);

            countAmmo = procAmmo;
        }
    }
 
    }
