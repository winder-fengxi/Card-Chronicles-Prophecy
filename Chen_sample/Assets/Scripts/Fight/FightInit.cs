using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����ս����ʼ��
public class FightInit : FightUnit
{
    public override void Init()
    {
        //�л�bgm
        //AudioManager.Instance.PlayBGM("battle");
        //��ʾս������
        //UIManager.Instance.ShowUI<FightUI>("FightUI");
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
