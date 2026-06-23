using UnityEngine;

public class GMS_Start : IState
{
    private GameManager _gm;
    public GMS_Start(GameManager GM)
    {
        _gm = GM;
    }
    public void Enter()
    {
        // play intro music
        InputManager.instance.ChangeState(InputStates.START);
        InputManager.instance.StartAction += _gm.EnterPlay;
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        // end intro music
        InputManager.instance.StartAction -= _gm.EnterPlay;
    }
}
