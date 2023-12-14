using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisePoint : MonoBehaviour
{
    [SerializeField] private GameObject noisePoint;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //SEçƒê∂
            audioSource.clip = audioClip;
            audioSource.Play();

            noisePoint.SetActive(false);
        }
    }
}
