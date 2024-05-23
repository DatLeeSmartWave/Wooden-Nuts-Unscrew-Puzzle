using UnityEngine;
using CandyCoded.HapticFeedback;

public class HammerController : MonoBehaviour
{
    public static HammerController instance;
    public Animator animator;

    private void Awake() {
        instance = this;
        animator = GetComponent<Animator>();
    }
    
    public void VibrateDevice() {
        if(PlayerPrefs.GetInt(StringsManager.VibrateKey)==1)
        HapticFeedback.HeavyFeedback();
    }
}
