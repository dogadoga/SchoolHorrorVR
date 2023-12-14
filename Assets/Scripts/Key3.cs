using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key3 : MonoBehaviour
{
    [SerializeField] private GameObject key3;
    [SerializeField] private GameObject guideMessageToGetOut;
    public AudioSource audioSource;
    public AudioClip pickUpItem;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //�t���O�����Ă�
            Key.getKey3 = true;

            //UI�\��
            UIManager.instance.ChangeUIText(Key.instance.KeyFlag(), 20f);

            //SE�Đ�
            audioSource.clip = pickUpItem;
            audioSource.Play();

            spawnPoint1.SetActive(true);
            spawnPoint2.SetActive(true);
            spawnPoint3.SetActive(true);
            guideMessageToGetOut.SetActive(true);

            key3.SetActive(false);
        }
    }
}
