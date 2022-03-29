using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip titleBGM;
    public AudioClip titlevoice;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //TitleBGM
        audioSource.PlayOneShot(titleBGM);
    }
    void Update()
    {
        StartCoroutine(Titlevoice());
    }
    IEnumerator Titlevoice()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ボタン押すと声
            audioSource.PlayOneShot(titlevoice);

            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("Main");
        }
    }
}