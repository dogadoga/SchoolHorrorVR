using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public AudioSource audioSource;
    private bool haveKey;


    // Start is called before the first frame update
    void Start()
    {
        haveKey = false;

        audioSource = GetComponent<AudioSource>();
    }

    public void getKey()
    {
        haveKey = true;
        Debug.Log("You got the Key!");

    }

    /// <summary>
    /// ƒS[ƒ‹‚Ìˆ—
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag=="Player")
        {   
            if (Key.getKey1 & Key.getKey2 & Key.getKey3)
            {
                audioSource.Play();
                Key.getKey1 = false;
                Key.getKey2 = false;
                Key.getKey3 = false;
                GameManager.gameClear = true;
            }
            else
            {
                audioSource.Play();
                UIManager.instance.ChangeUIText("Œ®‚ª•Â‚Ü‚Á‚Ä‚¢‚é", 5f);
            }
        }
    }
}
