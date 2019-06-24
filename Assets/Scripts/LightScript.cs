using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private Light lightSource;

    public float depleteAmount = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        lightSource = GetComponent<Light>();
        lightSource.intensity = 1;
    }

    // Update is called once per frame
    void Update()
    {
            lightSource.intensity -= depleteAmount * Time.deltaTime;
    }

    void powerPickup()
    {
        lightSource.intensity = 1;
    }
}
