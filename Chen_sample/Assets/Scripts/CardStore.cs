using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStore : MonoBehaviour
{
    public TextAsset cardData;
    public List<Card> cardList=new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        //LoadCardData();
        //TestLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCardData()
    {
        string[] dataRow = cardData.text.Split('\n');

        foreach(var row in dataRow)
        {
            string[] rowArray = row.Split(',');

            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "能力类")
            {
                //新建能力卡
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int alk = int.Parse(rowArray[3]);
                string intro = rowArray[4];
                AbilityCard monsterCard = new AbilityCard(id, name, alk, intro);
                cardList.Add(monsterCard);

                Debug.Log("读取到能力卡:"+monsterCard.cardName);
            }
            else if (rowArray[0] == "spell")
            {
                //新建攻击卡
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int alk = int.Parse(rowArray[3]);
                string intro = rowArray[4];
                AttackCard spellcard = new AttackCard(id, name, alk, intro);
                cardList.Add(spellcard);

                Debug.Log("读取到攻击卡:" + spellcard.cardName);
            }
        }
    }

    public void TestLoad()
    {
        foreach(var item in cardList)
        {
            Debug.Log("卡牌：" + item.id.ToString() + item.cardName);
        }
    }

    public Card RandomCard()
    {
        Card card = cardList[Random.Range(0, cardList.Count)];
        return card;
    }

    public Card CopyCard(int _id)
    {
        Card copyCard = new Card(_id, cardList[_id].cardName, cardList[_id].consume, cardList[_id].introduce);
        if (cardList[_id] is AttackCard)
        {
            var attackcard = cardList[_id] as AttackCard;
            copyCard = new AttackCard(_id, attackcard.cardName, attackcard.consume, attackcard.introduce);
        }
        else if (cardList[_id] is AbilityCard)
        {
            var abilitycard = cardList[_id] as AbilityCard;
            copyCard = new AttackCard(_id, abilitycard.cardName, abilitycard.consume, abilitycard.introduce);
        }
        //可添加其他卡牌类型

        return copyCard;
    }
}
