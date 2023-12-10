using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏入口脚本
public class GameApp : MonoBehaviour
{
    void Start()
    {
        Init();
    }

    public void Init()
    {
        //初始化声音管理器
        AudioManager.Instance.Init();

        //显示loginUI创建的脚本名字记得跟预制体物体名字一致
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        //播放bgm
        AudioManager.Instance.PlayBGM("bgm1");

        //初始化配置表
        GameConfigManager.Instance.Init();

        //测试
        string name = GameConfigManager.Instance.GetCardById("1001")["Name"];
        print(name);

        //初始化用户信息
        RoleManager.Instance.Init();
    }
}