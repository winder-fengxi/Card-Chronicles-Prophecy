using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    //AudioClip
    public AudioClip music;//游戏音乐
    public AudioClip se;//游戏音效

    //播放器组件获取
    private AudioSource player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AudioSource>();

        //设定播放的音频片段
        player.clip = music;

        //循环播放
        player.loop = true;

        //调整音量
        player.volume = 0.1f;

        //播放
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //按Q键切换声音的播放和暂停
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (player.isPlaying)
            {
                //如果正在播放声音，则暂停播放
                player.Pause();
                //player.Stop();//停止播放
            }
            else
            {
                //继续播放
                player.UnPause();//与Pause配套使用
                //player.Play();//相当于重新播放，即从头开始，与Stop配套使用
            }
        }
    }
}
