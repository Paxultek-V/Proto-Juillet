using UnityEngine;

public class LevelBase : MonoBehaviour, I_Initializable
{
    

    public void Initialize()
    {
        InitializeLevel();
    }

    protected virtual void InitializeLevel()
    {
        
    }
}
