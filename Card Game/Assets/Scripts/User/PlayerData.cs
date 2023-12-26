using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData :MonoBehaviour
{
    private static PlayerData instance;
    public static PlayerData Instance
    {   get
        {
            if (instance ==null)
            {
                instance = new PlayerData();
            }
            return instance;
        }
    }
    //单例模式
    public int coin = 50;
    void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCoin(int cost)
    {
        coin += cost;
    }
}
