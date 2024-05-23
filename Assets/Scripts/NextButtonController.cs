using UnityEngine;

public class NextButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelDisplayManager.Instance.levelDisplayText.text == "Level 30") gameObject.SetActive(false);
    }
}
