using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int level;

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
