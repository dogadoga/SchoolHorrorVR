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
    /// ƒVƒ“ƒOƒ‹ƒgƒ“
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
    /// Œ®‚Ìæ“¾ƒtƒ‰ƒO‚©‚çŸ‚Ìs“®‚ğw¦
    /// </summary>
    /// <param name="key1"></param>
    /// <param name="key2"></param>
    /// <param name="key3"></param>
    /// <returns>UI‚É•\¦‚·‚éƒeƒLƒXƒg</returns>
    public string KeyFlag()
    {
        string text = "";
        if (getKey1 & getKey2 & getKey3)
        {
            text = "ÅŒã‚ÌŒ®‚ğ“üè\n1ŠK‚Ì”ñíŒû‚Ö‹}‚°I";
        }
        else if(!getKey1 & getKey2 & getKey3)
        {
            text = "Œ®‚ğ“üè\n1ŠK‚ÌŒ®‚ğ’T‚¹";
        }
        else if(!getKey2 & getKey1 & getKey3)
        {
            text = "Œ®‚ğ“üè\n2ŠK‚ÌŒ®‚ğ’T‚¹";
        }
        else if(!getKey3 & getKey1 & getKey2)
        {
            text = "Œ®‚ğ“üè\n3ŠK‚ÌŒ®‚ğ’T‚¹";
        }
        else if(!getKey1 & !getKey2 & getKey3)
        {
            text = "Œ®‚ğ“üè\n1ŠK‚Æ2ŠK‚ÌŒ®‚ğ’T‚¹";
        }
        else if(!getKey2 & !getKey3 & getKey1)
        {
            text = "Œ®‚ğ“üè\n2ŠK‚Æ3ŠK‚ÌŒ®‚ğ’T‚¹";
        }
        else if(!getKey1 & !getKey3 & getKey2)
        {
            text = "Œ®‚ğ“üè\n1ŠK‚Æ3ŠK‚ÌŒ®‚ğ’T‚¹";
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
