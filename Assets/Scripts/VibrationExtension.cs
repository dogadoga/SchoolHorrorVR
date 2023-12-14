using System.Collections;
using UnityEngine;

/// <summary>
/// U“®ˆ—Šg’£ƒNƒ‰ƒX
/// </summary>
public class VibrationExtension : MonoBehaviour
{
    // Singleton‰»
    private static VibrationExtension _instance;
    public static VibrationExtension Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void VibrateController(float duration, float frequency, float amplitude, OVRInput.Controller controller)
    {
        StartCoroutine(VibrateForSeconds(duration, frequency, amplitude, controller));
    }

    IEnumerator VibrateForSeconds(float duration, float frequency, float amplitude, OVRInput.Controller controller)
    {
        // U“®ŠJn
        OVRInput.SetControllerVibration(frequency, amplitude, controller);

        // U“®ŠÔŠu•ª‘Ò‚Â
        yield return new WaitForSeconds(duration);

        // U“®I—¹
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}