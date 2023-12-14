using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// UI��ύX���邽�߂̃N���X
/// </summary>
public class UIManager : MonoBehaviour
{
    public static GameObject UICanvas;
    [SerializeField] private TextMeshProUGUI UIText;
    [SerializeField] private TextMeshProUGUI HPText;
    [SerializeField] private TextMeshProUGUI HitText;
    public static UIManager instance;

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

    // Start is called before the first frame update
    void Start()
    {
        UIText.text = "";
        HitText.text = "";
    }

    private void Update()
    {
        // HP��\��
        HPText.text = "HP: "+GameManager.playerHP.ToString();
    }

    /// <summary>
    /// UIText��text���w��b���̊ԕς���
    /// </summary>
    /// <param name="text"></param>
    /// <param name="time"></param>
    public void ChangeUIText(string text, float time=2f)
    {
        StartCoroutine(DisplayText(UIText, text, time));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="time"></param>
    public void ChangeHitText(string text, float time = 0.5f)
    {
        StartCoroutine(DisplayText(HitText, text, time));
    }

    /// <summary>
    /// ��莞�Ԃ�UI�̃e�L�X�g������
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private IEnumerator DisplayText(TextMeshProUGUI tmp, string text, float time)
    {
        tmp.text = text;
        yield return new WaitForSeconds(time);
        tmp.text = "";
        Debug.Log("DisplayText");
    }
}
