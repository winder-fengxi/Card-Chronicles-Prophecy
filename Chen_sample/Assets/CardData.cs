using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardData : MonoBehaviour
{
    void Start()
    {
        // ��test�е����ݼ��ؽ�txt�ı���
        TextAsset txt = Resources.Load("Card") as TextAsset;
        // ������ı�������
        Debug.Log(txt);

        // �Ի��з���Ϊ�ָ�㣬�����ı��ָ���������ַ����������������ʽ������ÿ���ַ���������
        string[] str = txt.text.Split('\n');
        // �����ı��е��ַ������
        for (int i = 0; i < str.Length - 1; i++)
        {
            Debug.Log(str[i]);
        }

        // ��ÿ���ַ����������Զ�����Ϊ�ָ�㣬����ÿ�����ŷָ����ַ������ݱ������
        foreach (string strs in str)
        {
            string[] ss = strs.Split(',');
            Debug.Log(ss[0]);
            Debug.Log(ss[1]);
            Debug.Log(ss[2]);
        }
    }
}
