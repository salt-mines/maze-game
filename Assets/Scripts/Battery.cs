﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.Rotate(new Vector3(0, 180, 0)*Time.deltaTime);      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.tag == "Player")
        {
            player.GetComponentInChildren<LightScript>().powerPickup();
            player.GetComponent<PlayerMovement>().BatteryPickUp();
            Destroy(transform.parent.gameObject);
        }
    }
}


