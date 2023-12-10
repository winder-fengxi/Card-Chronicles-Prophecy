using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timecontrol : MonoBehaviour
{
    public static timecontrol Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void timedelay(float num)
    {
        Invoke("empty", num);
    }

    void empty()
    {

    }
}
