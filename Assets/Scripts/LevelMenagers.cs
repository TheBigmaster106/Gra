using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //pozycja gracza
    Transform player;

    //prefab przeciwnika
    public GameObject basherPrefab;

    //czas między respawnem kolejnego bashera
    public float spawnInterval = 1;

    //czas od ostatniego respawnu
    float timeSinceSpawn;

    //bezpieczna odległość spawnu
    float spawnDistance = 30;

    // Start is called before the first frame update
    void Start()
    {
        //zlinkuj aktualna pozycje gracza do zmiennej transform
        player = GameObject.FindWithTag("Player").transform;

        //zerujemy licznik
        timeSinceSpawn = 0;
    }

    
    void Update()
    {
        
        timeSinceSpawn += Time.deltaTime;

      
        if (timeSinceSpawn > spawnInterval)
        {
            

            //wygeneruj randomową pozycję na kole o promieniu 1
            Vector2 random = Random.insideUnitCircle.normalized;

            //skonwertuj x,y na x,z i zerową wysokość
            Vector3 randomPosition = new Vector3(random.x, 0, random.y);

            //zwielokrotnij odległosć od gracza tak, żeby spawn następował poza kamerą
            randomPosition *= spawnDistance;

            
            randomPosition += player.position;

            
            if (!Physics.CheckSphere(new Vector3(randomPosition.x, 1, randomPosition.z), 0.5f))
            {
             
                Instantiate(basherPrefab, randomPosition, Quaternion.identity);

                //wyzeruj licznik
                timeSinceSpawn = 0;
            }
            
        }

     

    }
}