using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//敌人脚本
public class Enemy : MonoBehaviour
{
    protected Dictionary<string, string> data;//敌人数据表信息
    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }
}
