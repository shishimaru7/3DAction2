using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healitem : MonoBehaviour
{
    public AudioClip audioClip;

    [Header("回復量")]
    private float heal;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.TryGetComponent(out PlayerStatus playerStatus))
        {
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            //全回復
            heal = playerStatus.MaxHP;

            playerStatus.Addheal(heal);

            Destroy(gameObject);
        }
    }
}