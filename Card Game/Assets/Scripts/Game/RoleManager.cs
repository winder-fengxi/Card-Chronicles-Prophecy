using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//用户信息管理器（拥有的卡牌等信息金币等）
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();
    public List<string> cardList;//存储拥有的卡牌的id
    public void Init()
    {
        cardList = new List<string>();
        //四张攻击卡 四张防御卡 两张效果卡
        cardList.Add("1015");
        cardList.Add("1015");
        cardList.Add("1015");
        cardList.Add("1015");

        cardList.Add("1013");
        cardList.Add("1013");
        cardList.Add("1013");
        cardList.Add("1013");

        cardList.Add("1003");
        cardList.Add("1003");
        cardList.Add("1003");
        cardList.Add("1003");

        cardList.Add("1006");
        cardList.Add("1006");
        cardList.Add("1006");
        cardList.Add("1006");
    }
}