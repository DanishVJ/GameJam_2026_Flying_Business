using UnityEngine;

public class RestartGameButton : MonoBehaviour 
{
    public void OnClick()
    {
        GameManager.instance.EnterPlay();
    }
    
}
