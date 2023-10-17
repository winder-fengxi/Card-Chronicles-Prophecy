using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawcard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            int cardtype = Random.Range(1, 3);
            Card card = new Card();
            if(cardtype==1)
            {
            }
        }
    }
}
