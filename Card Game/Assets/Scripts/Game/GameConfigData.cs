using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 游戏配置表类，每个对象对应一个xt配置表
public class GameConfigData
{
    // 存储配置表中的所有数据
    private List<Dictionary<string, string>> dataDic;
    // 构造函数，参数为字符串
    public GameConfigData(string str)
    {
        // 初始化数据字典
        dataDic = new List<Dictionary<string, string>>();
        // 按换行符切割字符串
        string[] lines = str.Split('\n');
        // 第一行是存储数据的类型
        string[] title = lines[0].Trim().Split('\t');//tab切割
        // 从第三行（下标为2）开始遍历数据，第二行数据是解释说明
        for (int i = 2; i < lines.Length; i++)
        {
            // 创建新的字典存储每行数据
            Dictionary<string, string> dic = new Dictionary<string, string>();
            // 按tab切割每行数据
            string[] tempArr = lines[i].Trim().Split("\t");
            // 将切割后的数据添加到字典中
            for (int j = 0; j < tempArr.Length; j++)
            {
                dic.Add(title[j], tempArr[j]);
            }
            // 将字典添加到数据列表中
            dataDic.Add(dic);
        }
    }

    // 获取所有行的数据
    public List<Dictionary<string, string>> GetLines()
    {
        return dataDic;
    }

    // 根据ID获取一行数据
    public Dictionary<string, string> GetOneById(string id)
    {
        // 遍历数据列表
        for (int i = 0; i < dataDic.Count; i++)
        {
            // 获取当前字典
            Dictionary<string, string> dic = dataDic[i];
            // 如果字典中的ID与参数相同，返回该字典
            if (dic["Id"] == id)
            {
                return dic;
            }
        }
        // 如果没有找到，返回null
        return null;
    }
}