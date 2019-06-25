using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent navMesh;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMesh = GetComponent<NavMeshAgent>();
        //navMesh.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.destination = player.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 oldRotation = transform.rotation.eulerAngles;
        oldRotation.x = -90;
        transform.rotation = Quaternion.Euler(oldRotation);
    }
}
