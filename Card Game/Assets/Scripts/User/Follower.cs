using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//随从脚本
public class Follower : MonoBehaviour
{
    protected Dictionary<string, string> data;//随从数据表信息
    public ActionType type;

    //public GameObject hpItemObj;

    public bool SeparationCheck = false;//检测是否为分身

    ////UI相关
    //public Text defendTxt;
    //public Text hpTxt;
    //public Image hpImg;

    ////数值相关
    //public int Defend;
    //public int Attack;
    //public int MaxHp;
    //public int CurHp;

    ////血量、防御UI的检测
    //public bool Defendcheck = false;
    //public bool Attackcheck = false;

    SkinnedMeshRenderer _meshrenderer;

    //public Animator ani;

    public string IdName;
    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    void Start()
    {
        //ani = transform.GetComponent<Animator>();
        _meshrenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();

        //type = ActionType.None;
        //hpItemObj = UIManager.Instance.CreateHpItem();
        //defendTxt = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        //hpTxt = hpItemObj.transform.Find("hpTxt").GetComponent<Text>();
        //hpImg = hpItemObj.transform.Find("fill").GetComponent<Image>();
        //// 设置血条位置
        //hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("head").position);
        //hpItemObj.SetActive(false);
        ////初始化数值
        //Attack = int.Parse(data["Attack"]);
        //CurHp = int.Parse(data["Hp"]);
        //MaxHp = CurHp;
        //Defend = int.Parse(data["Defend"]);

        //UpdateHp();
        //UpdateDefend();
    }

    //更新血量信息
    //public void UpdateHp()
    //{
    //    hpTxt.text = CurHp + "/" + MaxHp;
    //    hpImg.fillAmount = (float)CurHp / (float)MaxHp;
    //}

    ////更新防御信息
    //public void UpdateDefend()
    //{
    //    defendTxt.text = Defend.ToString();
    //}

    //被攻击卡选中，显示红边
    public void OnSelect()
    {
        _meshrenderer.material.SetColor("_OtlColor", Color.red);
    }

    //未选中
    public void OnUnSelect()
    {
        _meshrenderer.material.SetColor("_OtlColor", Color.black);
    }

    ////受伤
    //public void Hit(int val)
    //{
    //    //先扣护盾
    //    if (Defend > val)
    //    {
    //        //扣护盾
    //        Defend -= val;
    //        //播放受伤
    //        ani.Play("hit", 0, 0);
    //    }
    //    else
    //    {
    //        val = val - Defend;
    //        Defend = 0;
    //        CurHp -= val;
    //        if (CurHp <= 0)
    //        {
    //            CurHp = 0;
    //            // 播放死亡
    //            //ani.Play("die");
    //            //敌人从列表中移除
    //            FollowerManeger.Instance.DeleteFollower(this);
    //            Destroy(gameObject, 1);
    //            Destroy(hpItemObj);
    //        }
    //        else
    //        {
    //            //受伤
    //            //ani.Play("hit", 0, 0);
    //        }
    //    }
    //    //刷新血量等ui
    //    UpdateDefend();
    //    UpdateHp();
    //}

    ////执行随从行动
    //public IEnumerator DoAction()
    //{
    //    //播放对应的动画（可以配置到excel表这里都默认播放攻击）
    //    ani.Play("attack");
    //    //等待某一时间的后执行对应的行为（也可以配置到excel表）
    //    yield return new WaitForSeconds(0.5f);//这里我写死了
    //    switch (type)
    //    {
    //        case ActionType.None:
    //            break;
    //        case ActionType.Attack:
    //            // 敌人扣血
    //            FightManager.Instance.GetEnemyHit(Attack);
    //            //摄像机可以抖一抖
    //            Camera.main.DOShakePosition(0.1f, 0.2f, 5, 45);
    //            break;
    //    }
    //    //等待动画播放完（这里的时长也可以配置）
    //    yield return new WaitForSeconds(1);
    //    //播放待机
    //    ani.Play("idle");
    //}

    ////隐藏随从的UI
    //public void HideUI()
    //{
    //    hpItemObj.gameObject.SetActive(false);
    //}

    ////显示随从的UI
    //public void DisplayUI()
    //{
    //    hpItemObj.gameObject.SetActive(true);
    //}
}