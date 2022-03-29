using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    AudioSource audioSource;

    //ゲームオーバーのBGM
    public AudioClip gameoverBGM;

    //ゲームオーバー時の声
    public AudioClip gameovervoice;

    //Mainの遷移の声
    public AudioClip revengevoice;

    //Titleへの遷移の声
    public AudioClip titlemovevoice;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //ゲームオーバー時のBGM
        audioSource.PlayOneShot(gameoverBGM);

        //ゲームオーバー時の声
        audioSource.PlayOneShot(gameovervoice);

    }
    void Update()
    {
        StartCoroutine(Scene());
    }
    IEnumerator Scene()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //再戦の声
            audioSource.PlayOneShot(revengevoice);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("Main");
        }

        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(titlemovevoice);
            yield return new WaitForSeconds(3);

            SceneManager.LoadScene("Title");
        }
    }
}