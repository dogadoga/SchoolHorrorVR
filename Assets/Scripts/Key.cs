using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static bool getKey1;
    public static bool getKey2;
    public static bool getKey3;
    public static Key instance;

    /// <summary>
    /// �V���O���g��
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ���̎擾�t���O���玟�̍s�����w��
    /// </summary>
    /// <param name="key1"></param>
    /// <param name="key2"></param>
    /// <param name="key3"></param>
    /// <returns>UI�ɕ\������e�L�X�g</returns>
    public string KeyFlag()
    {
        string text = "";
        if (getKey1 & getKey2 & getKey3)
        {
            text = "�Ō�̌������\n1�K�̔����֋}���I";
        }
        else if(!getKey1 & getKey2 & getKey3)
        {
            text = "�������\n1�K�̌���T��";
        }
        else if(!getKey2 & getKey1 & getKey3)
        {
            text = "�������\n2�K�̌���T��";
        }
        else if(!getKey3 & getKey1 & getKey2)
        {
            text = "�������\n3�K�̌���T��";
        }
        else if(!getKey1 & !getKey2 & getKey3)
        {
            text = "�������\n1�K��2�K�̌���T��";
        }
        else if(!getKey2 & !getKey3 & getKey1)
        {
            text = "�������\n2�K��3�K�̌���T��";
        }
        else if(!getKey1 & !getKey3 & getKey2)
        {
            text = "�������\n1�K��3�K�̌���T��";
        }
        return text;
    }

    // Start is called before the first frame update
    void Start()
    {
        getKey1 = false;
        getKey2 = false;
        getKey3 = false;
    }
}
