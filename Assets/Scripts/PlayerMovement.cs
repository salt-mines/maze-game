using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 7f;

    private PlayerInput input;

    private Vector3 targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
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
        }

        transform.position += new Vector3(input.Horizontal, 0, input.Vertical) * Time.deltaTime * playerSpeed;
        transform.LookAt(targetRotation);
    }
}
