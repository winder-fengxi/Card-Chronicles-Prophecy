using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//开始界面（要继承UIBase）
public class LoginUI : UIBase 
{
    void Awake()
    {
        //开始游戏
        Register("bg/startBtn").onClick = onStartGameBtn;

        //退出游戏
        Register("bg/quitBtn").onClick = onQuitGameBtn;
    }
    public void openMap(GameObject obj, PointerEventData pData)
    {
        SceneManager.LoadScene("Map");
    }
    public void onStartGameBtn(GameObject obj, PointerEventData pData)
    {
        //关闭login界面
        Hide();

        //战斗初始化
        FightManager.Instance.ChangeType(FightType.Init);
    }

    public void onQuitGameBtn(GameObject obj, PointerEventData pData)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}