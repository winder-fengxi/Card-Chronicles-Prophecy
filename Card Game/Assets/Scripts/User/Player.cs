using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    public static Player Instance;
    private void Awake()
    {

        Instance = this;
    }
}
