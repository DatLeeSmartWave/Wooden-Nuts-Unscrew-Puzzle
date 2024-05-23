using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int levelInt;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
