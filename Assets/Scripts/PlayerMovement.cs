using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 7f;

    private PlayerInput input;

    private Vector3 targetRotation;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
        targetRotation = transform.position + transform.forward;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (input.Horizontal != 0 || input.Vertical != 0)
        {
            targetRotation.x = transform.position.x + input.Horizontal * 100;
            targetRotation.z = transform.position.z + input.Vertical * 100;
            targetRotation.y = transform.position.y;

            if(animator)
                animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        transform.position += new Vector3(input.Horizontal, 0, input.Vertical) * Time.deltaTime * playerSpeed;
        transform.LookAt(targetRotation);      
    }
}
