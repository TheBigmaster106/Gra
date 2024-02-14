using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //pobieraniie kontrolera (poziom)
        float x = Input.GetAxis("Horizontal");
        //wylicz docelowy ruch poziomu (lewo/  prawo w osi x ) mozac wychylenie kontrolera 
        Vector3 movement = Vector3.right * x * Time.deltaTime;

        //pobieraniie kontrolera (poziom)
        float y = Input.GetAxis("Vertical");
        //wylicz docelowy ruch poziomu (lewo/  prawo w osi y ) mozac wychylenie kontrolera 
         movement += Vector3.forward * y * Time.deltaTime;

        movement = movement.normalized;

        movement *= moveSpeed;


        transform.position = movement;

    }
}

