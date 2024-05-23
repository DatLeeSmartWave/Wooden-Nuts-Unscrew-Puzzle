using UnityEngine;

public class NextButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelDisplay.Instance.levelText.text == "Level 30") gameObject.SetActive(false);
    }
}
