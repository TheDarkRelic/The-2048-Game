using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;


    public int Level; 

    private void OnEnable()
    {
        if (instance == null) { instance = this; }
        else Destroy(this);
        DontDestroyOnLoad(gameObject);
    }

    public void IncrimentLevel() { Level++; }

    public int GetLevel()
    {
        return Level;
    }
}
