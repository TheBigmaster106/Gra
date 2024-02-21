﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //pobierz stan kontrolera (poziom)
        float x = Input.GetAxisRaw("Horizontal");
        //wylicz docelowy ruch poziomo (lewo/prawo po osi x) mno¿¹c wychylenie kontrolera przez "1"
        Vector3 movement = Vector3.right * x;

        //pobierz stan kontrolera (pion)
        float y = Input.GetAxisRaw("Vertical");
        movement += Vector3.forward * y;

        //normalizuj ruch
        movement = movement.normalized;

        //przelicz przez czas od ostatniej klatki
        movement *= Time.deltaTime;

        //pomnó¿ ruch przez prêdkoœæ
        movement *= moveSpeed;

        //na³ó¿ zmianê po³o¿enia na obiekt gracza
        transform.position += movement;

    }
}