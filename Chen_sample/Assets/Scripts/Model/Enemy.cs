using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���˽ű�
public class Enemy : MonoBehaviour
{
    protected Dictionary<string, string> data;//�������ݱ���Ϣ
    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }
}
