using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
    gameStart , playerDraw, playerAction, enemyDraw, enemyAction
}

public class BattleManager : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerData enemyData;//数据

    public List<Card> playerDeckList = new List<Card>();
    public List<Card> enemyDeckList = new List<Card>();//卡组

    public GameObject cardPrefab;//卡牌

    public Transform playerHand;
    public Transform enemyHand;//手牌

    public GameObject[] playerBlocks;
    public GameObject[] enemyBlocks;//格子

    public GameObject playerIcon;
    public GameObject enemyIcon;//头像

    public GamePhase GamePhase = GamePhase.gameStart;

    // Start is called before the first frame update
    void Start()
    {
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //游戏流程
    //开始游戏：加载数据，卡组洗牌，初始化手牌
    //回合结束：游戏阶段

    public void GameStart()
    {
        //读取数据
        ReadDeck();

        //卡组洗牌
        ShuffletDeck(0);
        ShuffletDeck(1);

        //玩家抽卡5、敌人抽卡5
        DrawCard(0, 3);
        DrawCard(1, 3);

        GamePhase = GamePhase.playerDraw;
    }

    public void ReadDeck()
    {
        //加载玩家卡组
        for(int i=0;i<playerData.playerDeck.Length; i++)
        {
            if (playerData.playerDeck[i] != 0) 
            {
                int count = playerData.playerDeck[i];
                for(int j=0;j<count;j++)
                {
                    playerDeckList.Add(playerData.CardStore.CopyCard(i));
                }
            }
        }

        //加载敌人卡组
        for (int i = 0; i < enemyData.playerDeck.Length; i++)
        {
            if (enemyData.playerDeck[i] != 0)
            {
                int count = enemyData.playerDeck[i];
                for (int j = 0; j < count; j++)
                {
                    enemyDeckList.Add(enemyData.CardStore.CopyCard(i));
                }
            }
        }
    }

    public void ShuffletDeck(int _player)//洗牌：0为玩家，1为敌人
    {
        List<Card> shuffletDeck;
        if(_player==0)
        {
            shuffletDeck = playerDeckList;
        }
        else
        {
            shuffletDeck = enemyDeckList;
        }

        for(int i=0;i<shuffletDeck.Count;i++)
        {
            int rad = Random.Range(0, shuffletDeck.Count);
            Card temp = shuffletDeck[i];
            shuffletDeck[i] = shuffletDeck[rad];
            shuffletDeck[rad] = temp;
        }
    }

    public void OnPlayerDraw()
    {
        if(GamePhase==GamePhase.playerDraw)
        {
            DrawCard(0, 1);
            GamePhase = GamePhase.playerAction;
        }
    }

    public void OnEnemyDraw()
    {
        if (GamePhase == GamePhase.enemyDraw)
        {
            DrawCard(1, 1);
            GamePhase = GamePhase.enemyAction;
        }
    }

    public void DrawCard(int _player,int _count)
    {
        List<Card> drawDeck = new List<Card>();
        Transform hand;
        if(_player==0)
        {
            drawDeck = playerDeckList;
            hand = playerHand;
        }
        else
        {
            drawDeck= enemyDeckList;
            hand = enemyHand;
        }

        for(int i=0;i<_count;i++)
        {
            GameObject card = Instantiate(cardPrefab, hand);
            card.GetComponent<CardDisplay>().card = drawDeck[0];
            drawDeck.RemoveAt(0);
        }
    }

    public void OnClickTurnEnd()
    {
        TurnEnd();
    }

    public void TurnEnd()
    {
        if(GamePhase==GamePhase.playerAction)
        {
            GamePhase = GamePhase.enemyDraw;
        }
        else
        {
            GamePhase = GamePhase.playerDraw;
        }
    }
}
