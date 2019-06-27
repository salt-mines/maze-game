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

    private Animator animator;

    public Material ghostEmission;
    public float resetRange = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMesh.updatePosition = false;
    }
    
    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        if (player)
            if (player.GetComponentInChildren<Light>().range / player.GetComponentInChildren<LightScript>().maxRange > resetRange)
            {
                navMesh.destination = resetPoint.transform.position;
                ghostEmission.SetColor("_EmissionColor", Color.red);
            }
            else
            {
                navMesh.destination = player.transform.position;
                ghostEmission.SetColor("_EmissionColor", Color.white);
            }

        var localVel = transform.InverseTransformDirection(navMesh.velocity);
        animator.SetFloat("speed", localVel.magnitude);
        animator.SetFloat("turning", localVel.x);
    }

    private void OnAnimatorMove()
    {
        transform.position = navMesh.nextPosition;
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
