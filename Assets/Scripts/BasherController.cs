using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasherController : MonoBehaviour
{
    //gracz
    GameObject player;
    
    public float walkSpeed = 7f;
    
    GameObject levelManager;

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelManager = GameObject.Find("LevelManager");
    }

    // Update is called once per frame
    void Update()
    {
        //patrz siê na gracza
        transform.LookAt(player.transform.position);
        //idz do przodu
        transform.position += transform.forward * Time.deltaTime * walkSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Trafiony");
        //obiekt z którym mamy kolizje
        GameObject projectile = collision.gameObject;

        //tylko jeœli trafi³ nas gracz
        if (projectile.CompareTag("PlayerProjectile"))
        {
            //dolicz punkty
            levelManager.GetComponent<LevelManager>().AddPoints(1);

            //zniknij pocisk
            Destroy(projectile);

            //zniknij przeciwnika
            Destroy(transform.gameObject);

        }
       
     
    }
}