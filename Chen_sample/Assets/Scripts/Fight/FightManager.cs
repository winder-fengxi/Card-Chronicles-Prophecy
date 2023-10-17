using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ս��ö��
public enum FightType
{
    None,
    Init,
    Player,//��һغ�
    Enemy,//���˻غ�
    Win,
    Fail
}
//ս��������
public class FightManager : MonoBehaviour
{
    public int MaxHp;//�������ֵ
    public int CurHp;//��ǰ����ֵ
    public int MaxEnergyCount;//�����ֵ������ʹ�û����ľ�����
    public int CurEnergyCount;//��ǰ����
    public int MaxAttack;//��󹥻�ֵ
    public int CurAttack;//��ǰ����ֵ
    public int MaxSteps;//�����ж�����
    public int CurSteps;//��ǰ���ж�����
    public int MaxDefend;//��������λ��
    public int CurDefend;//��ǰ�����λ��

    public void Init()
    {
        MaxHp = 40;
        CurHp = 40;
        MaxEnergyCount = 10;
        CurEnergyCount = 10;
        MaxAttack = 10;
        CurAttack = 3;
        MaxSteps = 10;
        CurSteps = 10;
        MaxDefend = 3;
        CurDefend = 3;
    }

    public static FightManager Instance;
    public FightUnit fightUnit;//ս����Ԫ
    private void Awake()
    {

        Instance = this;
    }

    //�л�ս������
    public void ChangeType(FightType type)
    {
        switch (type)
        {
            case FightType.None:
                break;
            case FightType.Init:
                fightUnit = new FightInit();
                break;
            case FightType.Player:
                fightUnit = new Fight_PlayerTurn();
                break;
            case FightType.Enemy:
                fightUnit = new Fight_EnemyTurn();
                break;
            case FightType.Win:
                fightUnit = new Fight_Win();
                break;
            case FightType.Fail:
                fightUnit = new Fight_Fail();
                break;
        }
        fightUnit.Init();// ��ʼ��
    }
    private void Update()
    {
        if (fightUnit != null)
        {
            fightUnit.OnUpdate();
        }
    }

}
