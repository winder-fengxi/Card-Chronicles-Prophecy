using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//用户信息管理器（拥有的卡牌等信息金币等）
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();
    public List<string> cardList;//存储拥有的卡牌的id
    public int coin = 100;
    public void Init()
    {
        cardList = new List<string>();
        //初始抽卡堆

        cardList.Add("1000");
        cardList.Add("1001");
        cardList.Add("1002");
        cardList.Add("1003");

        cardList.Add("1004");
        cardList.Add("1005");
        cardList.Add("1006");
        cardList.Add("1007");

        cardList.Add("1008");
        cardList.Add("1009");
        cardList.Add("1010");
        cardList.Add("1011");
        cardList.Add("1013");
        cardList.Add("1014");

        cardList.Add("1015");
        cardList.Add("1016");
        cardList.Add("1017");
        cardList.Add("1018");

        cardList.Add("1019");
        cardList.Add("1020");
        cardList.Add("1021");
        cardList.Add("1022");

        cardList.Add("1024");
        cardList.Add("1025");
        cardList.Add("1026");
        cardList.Add("1027");

        cardList.Add("1028");
        cardList.Add("1029");

        cardList.Add("1017");
        cardList.Add("1017");
        cardList.Add("1017");
        cardList.Add("1027");
        cardList.Add("1027");
        cardList.Add("1027");
        cardList.Add("1028");
        cardList.Add("1029");
        cardList.Add("1028");
        cardList.Add("1029");
        cardList.Add("1028");
        cardList.Add("1029");
        cardList.Add("1026");
        cardList.Add("1026");
        cardList.Add("1026");
    }
}