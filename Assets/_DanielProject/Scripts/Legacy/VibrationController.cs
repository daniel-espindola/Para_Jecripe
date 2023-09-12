using UnityEngine;

public class VibrationController : MonoBehaviour
{
    static VibrationController vibrationController;

    private void Awake()
    {
        vibrationController = this;
    }
    public void VibrateDevice()
    {
        Handheld.Vibrate();
    }
}
