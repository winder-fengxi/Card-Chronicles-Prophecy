using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
    public void Tomap()
    {
         SceneManager.LoadScene("map");
    }

public void Tostore()
{
  SceneManager.LoadScene("Store");
}
public void battle()
{
     SceneManager.LoadScene("game");
}
}
