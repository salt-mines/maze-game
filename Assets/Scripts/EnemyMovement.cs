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

    // Update is called once per frame
    void Update()
    {
        if (player)
            if (player.GetComponentInChildren<Light>().range / player.GetComponentInChildren<LightScript>().maxRange > resetRange)
            {
                navMesh.destination = resetPoint.transform.position;
            }
            else
            {
                navMesh.destination = player.transform.position;
            }

        Vector3 worldDeltaPosition = navMesh.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        transform.position = navMesh.nextPosition;
    }

    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;

    private void LateUpdate()
    {
        if (animator)
        {
            animator.SetFloat("speed", velocity.y / navMesh.speed);
            animator.SetFloat("turning", velocity.x);
        }
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
