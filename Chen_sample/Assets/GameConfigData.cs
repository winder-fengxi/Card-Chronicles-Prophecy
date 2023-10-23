using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��Ϸ���ñ��࣬ÿ�������Ӧһ��xt���ñ�
public class GameConfigData
{
    // �洢���ñ��е���������
    private List<Dictionary<string, string>> dataDic;
    // ���캯��������Ϊ�ַ���
    public GameConfigData(string str)
    {
        // ��ʼ�������ֵ�
        dataDic = new List<Dictionary<string, string>>();
        // �����з��и��ַ���
        string[] lines = str.Split('\n');
        // ��һ���Ǵ洢���ݵ�����
        string[] title = lines[0].Trim().Split('\t');//tab�и�
        // �ӵ����У��±�Ϊ2����ʼ�������ݣ��ڶ��������ǽ���˵��
        for (int i = 2; i < lines.Length; i++)
        {
            // �����µ��ֵ�洢ÿ������
            Dictionary<string, string> dic = new Dictionary<string, string>();
            // ��tab�и�ÿ������
            string[] tempArr = lines[i].Trim().Split("\t");
            // ���и���������ӵ��ֵ���
            for (int j = 0; j < tempArr.Length; j++)
            {
                dic.Add(title[j], tempArr[j]);
            }
            // ���ֵ���ӵ������б���
            dataDic.Add(dic);
        }
    }

    // ��ȡ�����е�����
    public List<Dictionary<string, string>> GetLines()
    {
        return dataDic;
    }

    // ����ID��ȡһ������
    public Dictionary<string, string> GetOneById(string id)
    {
        // ���������б�
        for (int i = 0; i < dataDic.Count; i++)
        {
            // ��ȡ��ǰ�ֵ�
            Dictionary<string, string> dic = dataDic[i];
            // ����ֵ��е�ID�������ͬ�����ظ��ֵ�
            if (dic["Id"] == id)
            {
                return dic;
            }
        }
        // ���û���ҵ�������null
        return null;
    }
}
