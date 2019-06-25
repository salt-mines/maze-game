using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{ 
    public GameObject player;

    public float cameraDistance = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, cameraDistance, (player.transform.position.z - 5));
    }
}
