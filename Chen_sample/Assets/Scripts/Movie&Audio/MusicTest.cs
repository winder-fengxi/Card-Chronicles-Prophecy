using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    //AudioClip
    public AudioClip music;//��Ϸ����
    public AudioClip se;//��Ϸ��Ч

    //�����������ȡ
    private AudioSource player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AudioSource>();

        //�趨���ŵ���ƵƬ��
        player.clip = music;

        //ѭ������
        player.loop = true;

        //��������
        player.volume = 0.1f;

        //����
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //��Q���л������Ĳ��ź���ͣ
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (player.isPlaying)
            {
                //������ڲ�������������ͣ����
                player.Pause();
                //player.Stop();//ֹͣ����
            }
            else
            {
                //��������
                player.UnPause();//��Pause����ʹ��
                //player.Play();//�൱�����²��ţ�����ͷ��ʼ����Stop����ʹ��
            }
        }
    }
}
