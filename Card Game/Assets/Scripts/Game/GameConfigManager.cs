using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
// 游戏配置管理类
public class GameConfigManager
{
    // 单例模式
    public static GameConfigManager Instance = new GameConfigManager();
    private GameConfigData cardData;//卡牌表
    private GameConfigData enemyData;//敌人表
    private GameConfigData levelData;//关卡表
    private GameConfigData cardTypeData; //卡牌类型
    // 文本资源
    private TextAsset textAsset;

    // 初始化配置文件（txt文件 存储到内存）
    public void Init()
    {
        // 加载卡牌数据
        textAsset = Resources.Load<TextAsset>("Data/card");
        cardData = new GameConfigData(textAsset.text);

        // 加载敌人数据
        textAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyData = new GameConfigData(textAsset.text);

        // 加载关卡数据
        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);

        //卡牌类型数据
        textAsset = Resources.Load<TextAsset>("Data/cardType");
        cardTypeData = new GameConfigData(textAsset.text);
    }

    // 获取卡牌行数据
    public List<Dictionary<string, string>> GetCardLines()
    {
        return cardData.GetLines();
    }

    // 获取敌人行数据
    public List<Dictionary<string, string>> GetEnemyLines()
    {
        return enemyData.GetLines();
    }

    // 获取关卡行数据
    public List<Dictionary<string, string>> GetLevelLines()
    {

        return levelData.GetLines();
    }
    // 根据ID获取卡牌数据
    public Dictionary<string, string> GetCardById(string id)
    {
        return cardData.GetOneById(id);
    }
    // 根据ID获取敌人数据
    public Dictionary<string, string> GetEnemyById(string id)
    {
        return enemyData.GetOneById(id);
    }
    // 根据ID获取关卡数据
    public Dictionary<string, string> GetLevelById(string id)
    {
        return levelData.GetOneById(id);
    }

    //根据ID获取卡牌类型
    public Dictionary<string, string> GetCardTypeById(string id)
    {
        return cardTypeData.GetOneById(id);
    }
}