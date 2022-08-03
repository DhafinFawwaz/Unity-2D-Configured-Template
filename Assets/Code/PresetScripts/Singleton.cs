using UnityEngine;
using UnityEngine.Audio;

public class Singleton : MonoBehaviour
{
    public TransitionManager transition;
    new public AudioManager audio;//'new': Hide inherited member
    public SceneLoader loader;
    public ResolutionManager resolution;
    public SaveManager save;
    public static Singleton Instance;
    
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
