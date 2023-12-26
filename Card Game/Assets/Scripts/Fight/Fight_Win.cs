using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DG.Tweening;

public class Fight_Win :FightUnit
{

    public float coldTime = 2;
    public override void Init()
    {
        Debug.Log("游戏胜利");
        //何以显示结算界面预制体有了能看到这里的小伙伴应该可以自己补上了
        UIManager.Instance.ShowTip("游戏成功", Color.red, delegate () { });
        DelayDestroy3();
    }


    public void DelayDestroy3()
    {
        float reTime = Time.time;
        float nowTime = Time.time;
        Debug.Log(reTime);
        Debug.Log(nowTime);
        //while (nowTime - reTime < 3)
        //{
        //    nowTime = Time.time;
        //    Debug.Log(nowTime);
        //}
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("map");
    }
}