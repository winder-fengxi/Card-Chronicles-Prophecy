using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        //播放bgm
        //AudioManager.Instance.PlayBGM("bgm1");
    }

    public void GetScene()
    {
        //方法一，通过索引值切换场景
        //SceneManager.LoadScene(1);
        //方法二，通过场景名字切换场景
        SceneManager.LoadScene("game");
        //GameApp.Instance.Init();
    }

    //退出游戏
    public void Exit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
