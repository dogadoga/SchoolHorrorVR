using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key1 : MonoBehaviour
{
    [SerializeField] private GameObject key1;
    public AudioSource audioSource;
    public AudioClip pickUpItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //ÉtÉâÉOÇÇΩÇƒÇÈ
            Key.getKey1 = true;

            //UIï\é¶
            UIManager.instance.ChangeUIText(Key.instance.KeyFlag(), 20f);

            //SEçƒê∂
            audioSource.clip = pickUpItem;
            audioSource.Play();

            key1.SetActive(false);
        }
    }
}

