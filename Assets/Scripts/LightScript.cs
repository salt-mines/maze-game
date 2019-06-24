using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private Light lightSource;

    public float depleteAmount = 0.01f;
    public float maxRange = 5f;
    public float minRange = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        lightSource = GetComponent<Light>();
        lightSource.intensity = 1;
        lightSource.range = maxRange;
    }

    // Update is called once per frame
    void Update()
    {
        if(lightSource.range > minRange)
        {
            lightSource.intensity -= depleteAmount * Time.deltaTime;
            lightSource.range -= depleteAmount * Time.deltaTime;
        }
        
    }

    public void powerPickup()
    {
        lightSource.intensity = 1;
        lightSource.range = maxRange;
    }
}
