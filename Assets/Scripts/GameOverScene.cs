using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScene : MonoBehaviour
{
    public float timeTrigger=5;
    float timeElapsed = 0;
    public GameObject PressRightTrigger;
    [SerializeField] private TextMeshProUGUI GameOverText;
    [SerializeField] private TextMeshProUGUI zombieDefeated;
    [SerializeField] private TextMeshProUGUI playTimeText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gameClearMusic;
    [SerializeField] private AudioClip gameOverMusic;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        PressRightTrigger = GameObject.Find("PressRightTrigger");
        PressRightTrigger.SetActive(false);
        if (GameManager.gameClear)
        {
            GameOverText.text = "You Escaped! \n\n‚ ‚È‚½‚Í’Eo‚µ‚Ü‚µ‚½I";
            audioSource.clip = gameClearMusic;
            audioSource.Play();
        }
        else if(GameManager.GameOver)
        {
            GameOverText.text = "GAME OVER";
            audioSource.clip = gameOverMusic;
            audioSource.Play();
        }

        // ƒvƒŒƒCƒ^ƒCƒ€
        playTimeText.text = "ƒvƒŒƒCŽžŠÔ : " + ((int)(GameManager.playTime / 60f)).ToString("00")+" min " + ((int)(GameManager.playTime % 60f)).ToString("00") +" sec";

        // “|‚µ‚½ƒ]ƒ“ƒr‚Ì”
        zombieDefeated.text = "“|‚µ‚½ƒ]ƒ“ƒr‚Ì” : " + GameManager.zombiesDefeated.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeTrigger)
        {
            PressRightTrigger.SetActive(true);
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                SceneManager.LoadScene("StartScene");
            }
        }
    }
}
