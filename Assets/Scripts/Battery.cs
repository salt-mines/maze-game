using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.Rotate(new Vector3(0, 5, 0));
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            player.GetComponentInChildren<LightScript>().powerPickup();
            Destroy(transform.parent.gameObject);
        }
    }



}


