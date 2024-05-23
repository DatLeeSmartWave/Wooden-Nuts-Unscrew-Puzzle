using UnityEngine;
using CandyCoded.HapticFeedback;

public class HammerControllerScript : MonoBehaviour
{
    public static HammerControllerScript instance;
    public Animator animator;

    private void Awake() {
        instance = this;
        animator = GetComponent<Animator>();
    }
    
    public void VibrateDevice() {
        if(PlayerPrefs.GetInt(StringsTextManager.VibrateEffectKey)==1)
        HapticFeedback.HeavyFeedback();
    }
}
