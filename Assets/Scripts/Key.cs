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
    /// VOg
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
    /// ®Ìæ¾tO©çÌs®ðw¦
    /// </summary>
    /// <param name="key1"></param>
    /// <param name="key2"></param>
    /// <param name="key3"></param>
    /// <returns>UIÉ\¦·éeLXg</returns>
    public string KeyFlag()
    {
        string text = "";
        if (getKey1 & getKey2 & getKey3)
        {
            text = "ÅãÌ®ðüè\n1KÌñíûÖ}°I";
        }
        else if(!getKey1 & getKey2 & getKey3)
        {
            text = "®ðüè\n1KÌ®ðT¹";
        }
        else if(!getKey2 & getKey1 & getKey3)
        {
            text = "®ðüè\n2KÌ®ðT¹";
        }
        else if(!getKey3 & getKey1 & getKey2)
        {
            text = "®ðüè\n3KÌ®ðT¹";
        }
        else if(!getKey1 & !getKey2 & getKey3)
        {
            text = "®ðüè\n1KÆ2KÌ®ðT¹";
        }
        else if(!getKey2 & !getKey3 & getKey1)
        {
            text = "®ðüè\n2KÆ3KÌ®ðT¹";
        }
        else if(!getKey1 & !getKey3 & getKey2)
        {
            text = "®ðüè\n1KÆ3KÌ®ðT¹";
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
