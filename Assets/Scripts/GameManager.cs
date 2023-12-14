using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // HP
    public static int playerHP = 100;
    public static bool canShoot;
    public static bool GameOver = false;
    /// <summary>
    /// 経過時間
    /// </summary>
    public static float seconds;
    /// <summary>
    /// プレイ時間
    /// </summary>
    public static float playTime;
    /// <summary>
    /// 倒したゾンビの数
    /// </summary>
    public static int zombiesDefeated=0;
    public static bool gameClear;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        playerHP = 100;
        seconds = 0;
        zombiesDefeated = 0;
        GameOver = false;
        gameClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        //時間を管理
        seconds += Time.deltaTime;

        // ゲームクリア時
        if (gameClear) 
        {
            playTime = seconds;
            SceneManager.LoadScene("GameOver");  
        }
        // ゲームオーバー時
        if (GameOver)
        {
            playTime = seconds;
            SceneManager.LoadScene("GameOver");
        }

    }

    // ダメージを受けるとき
    public void TakeHit(float damage)
    {
        playerHP = (int)Mathf.Clamp(playerHP - damage, 0, playerHP);
        if (playerHP <= 0 && !GameOver)
        {
            Key.getKey1 = false;
            Key.getKey2 = false;
            Key.getKey3 = false;
            GameOver = true;
        }
    }
}
