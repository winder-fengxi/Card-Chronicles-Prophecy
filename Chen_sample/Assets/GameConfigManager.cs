using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
// ��Ϸ���ù�����
public class GameConfigManager
{
    // ����ģʽ
    public static GameConfigManager Instance = new GameConfigManager();
    private GameConfigData cardData;//���Ʊ�
    private GameConfigData enemyData;//���˱�
    private GameConfigData levelData;//�ؿ���
    private GameConfigData cardTypeData; //��������
    // �ı���Դ
    private TextAsset textAsset;

    // ��ʼ�������ļ���txt�ļ� �洢���ڴ棩
    public void Init()
    {
        // ���ؿ�������
        textAsset = Resources.Load<TextAsset>("Data/card");
        cardData = new GameConfigData(textAsset.text);

        // ���ص�������
        textAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyData = new GameConfigData(textAsset.text);

        // ���عؿ�����
        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);

        //������������
        textAsset = Resources.Load<TextAsset>("Data/cardType");
        cardTypeData = new GameConfigData(textAsset.text);
    }

    // ��ȡ����������
    public List<Dictionary<string, string>> GetCardLines()
    {
        return cardData.GetLines();
    }

    // ��ȡ����������
    public List<Dictionary<string, string>> GetEnemyLines()
    {
        return enemyData.GetLines();
    }

    // ��ȡ�ؿ�������
    public List<Dictionary<string, string>> GetLevelLines()
    {

        return levelData.GetLines();
    }
    // ����ID��ȡ��������
    public Dictionary<string, string> GetCardById(string id)
    {
        return cardData.GetOneById(id);
    }
    // ����ID��ȡ��������
    public Dictionary<string, string> GetEnemyById(string id)
    {
        return enemyData.GetOneById(id);
    }
    // ����ID��ȡ�ؿ�����
    public Dictionary<string, string> GetLevelById(string id)
    {
        return levelData.GetOneById(id);
    }

    //����ID��ȡ��������
    public Dictionary<string, string> GetCardTypeById(string id)
    {
        return cardTypeData.GetOneById(id);
    }
}

