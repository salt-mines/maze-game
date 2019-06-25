using UnityEngine;

public class LightScript : MonoBehaviour
{
    private Light lightSource;

    public float rangeDeplete = 0.25f;
    public float maxRange = 5f;
    public float minRange = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        lightSource = GetComponent<Light>();
        lightSource.range = maxRange;
    }

    // Update is called once per frame
    void Update()
    {
        if(lightSource.range > minRange)
        {
            lightSource.range -= rangeDeplete * Time.deltaTime;
        }
        
    }

    public void powerPickup()
    {
        lightSource.range = maxRange;
    }
}
