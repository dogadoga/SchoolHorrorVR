using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key2 : MonoBehaviour
{
    [SerializeField] private GameObject key2;
    public AudioSource audioSource;
    public AudioClip pickUpItem;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   
            //�t���O�����Ă�
            Key.getKey2 = true;

            //UI�\��
            UIManager.instance.ChangeUIText(Key.instance.KeyFlag(), 20f);

            //SE�Đ�
            audioSource.clip = pickUpItem;
            audioSource.Play();

            spawnPoint1.SetActive(true);
            spawnPoint2.SetActive(true);

            key2.SetActive(false);
        }
    }
}
