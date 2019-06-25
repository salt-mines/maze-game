using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
// Use this for initialization
void Start () {
    Time.timeScale = 1;
}

//Reloads the Level
public void Reload(){
        SceneManager.LoadScene("Level1",LoadSceneMode.Single);
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
