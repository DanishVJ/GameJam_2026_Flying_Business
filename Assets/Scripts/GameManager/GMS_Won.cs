using UnityEngine;

public class GMS_Won : IState
{
    private GameManager _gm;
    public GMS_Won(GameManager GM)
    {
        _gm = GM;
    }
    public void Enter()
    {
        // play win tune
        InputManager.instance.ChangeState(InputStates.WON);
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        // if win tune is still running kill it
    }
}
