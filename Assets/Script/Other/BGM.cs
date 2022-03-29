using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip bossclip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //Playerが入ってきたら音楽変わる
        if (other.gameObject.CompareTag("Player"))
        {
            //normalfield止まる
            audioSource.Stop();

            //boss曲再生
            audioSource.clip = bossclip;

            //ボス曲再生（ループあり）
            audioSource.Play();
        }
    }
}