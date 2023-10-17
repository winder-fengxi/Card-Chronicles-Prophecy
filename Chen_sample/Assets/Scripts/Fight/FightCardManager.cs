using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ս�����ƹ�����
public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();
    public List<string> cardList;//���Ѽ���
    public List<string> usedCardList;//���ƶ�
    //��ʼ��
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();
        //������ʱ����
        List<string> tempList = new List<string>();
        //����ҵĿ��ƴ洢����ʱ����
        tempList.AddRange(RoleManager.Instance.cardList);
        while (tempList.Count > 0)
        {
            //����±�
            int tempIndex = Random.Range(0, tempList.Count);
            //��ӵ�����
            cardList.Add(tempList[tempIndex]);
            //��ʱ����ɾ��
            tempList.RemoveAt(tempIndex);
        }
        Debug.Log(cardList.Count);
    }
}
