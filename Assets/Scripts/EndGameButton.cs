using UnityEngine;

public class EndGameButton : MonoBehaviour 
{
    public void OnClick()
    {
        GameManager.instance.QuitGame();
    }
}
