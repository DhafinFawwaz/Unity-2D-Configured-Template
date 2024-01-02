using UnityEngine;

public class Main
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialization()
    {
        Singleton.Initialize();
        ResolutionManager.Initialize();
        Save.Initialize();
    }
}
