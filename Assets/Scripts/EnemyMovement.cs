using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private GameManager gameManager;

    private GameObject player;
    public GameObject resetPoint;
    private NavMeshAgent navMesh;

    public float resetRange = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
            if(player.GetComponentInChildren<Light>().range/player.GetComponentInChildren<LightScript>().maxRange > resetRange)
            {
                navMesh.destination = resetPoint.transform.position;
            }
            else
            {
                navMesh.destination = player.transform.position;
            }
        
    }

    private void LateUpdate()
    {
        Vector3 oldRotation = transform.rotation.eulerAngles;
        oldRotation.x = -90;
        transform.rotation = Quaternion.Euler(oldRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.tag == "Player")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            gameManager.PlayerTakeDamage();
        }
    }
}
