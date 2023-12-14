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
    /// �o�ߎ���
    /// </summary>
    public static float seconds;
    /// <summary>
    /// �v���C����
    /// </summary>
    public static float playTime;
    /// <summary>
    /// �|�����]���r�̐�
    /// </summary>
    public static int zombiesDefeated=0;
    public static bool gameClear;

    // Start is called before the first frame update
    void Start()
    {
        // ������
        playerHP = 100;
        seconds = 0;
        zombiesDefeated = 0;
        GameOver = false;
        gameClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԃ��Ǘ�
        seconds += Time.deltaTime;

        // �Q�[���N���A��
        if (gameClear) 
        {
            playTime = seconds;
            SceneManager.LoadScene("GameOver");  
        }
        // �Q�[���I�[�o�[��
        if (GameOver)
        {
            playTime = seconds;
            SceneManager.LoadScene("GameOver");
        }

    }

    // �_���[�W���󂯂�Ƃ�
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
