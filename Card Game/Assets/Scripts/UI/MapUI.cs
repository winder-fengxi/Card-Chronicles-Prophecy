using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//开始界面（要继承UIBase）
public class MapUI : UIBase
{
    void Awake()
    {
        //开始战斗
        Register("Battle").onClick = onStartGameBtn;
        Register("Battle1").onClick = onStartGameBtn;
        Register("Battle2").onClick = onStartGameBtn;
        Register("Battle3").onClick = onStartGameBtn;
        Register("Battle4").onClick = onStartGameBtn;
        Register("Store").onClick = OpenShop;
        Register("Store1").onClick = OpenShop;
        Register("Store2").onClick = OpenShop;
    }
    public void openMap(GameObject obj, PointerEventData pData)
    {
        SceneManager.LoadScene("Map");
    }
    public void OpenShop(GameObject obj, PointerEventData pData)
    {
        SceneManager.LoadScene("Store");
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