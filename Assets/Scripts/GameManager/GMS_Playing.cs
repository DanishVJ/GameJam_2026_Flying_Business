using UnityEngine;

public class GMS_Playing : IState
{
    private GameManager _gm;
    public GMS_Playing(GameManager GM)
    {
        _gm = GM;
    }
    public void Enter()
    {
        // play playing music
        InputManager.instance.ChangeState(InputStates.PLAYING);
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        // end playing music
    }
}
