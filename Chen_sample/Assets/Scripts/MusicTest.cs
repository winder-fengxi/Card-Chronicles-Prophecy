using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    //AudioClip
    public AudioClip music;//
    public AudioClip se;//

    private AudioSource player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AudioSource>();

        player.clip = music;

        player.loop = true;

        player.volume = 0.1f;

        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (player.isPlaying)
            {
                player.Pause();
            }
            else
            {
                player.UnPause();
            }
        }
    }
}
