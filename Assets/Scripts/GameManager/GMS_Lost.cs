
public class GMS_Lost : IState
{
    private GameManager _gm;
    public GMS_Lost(GameManager GM)
    {
        _gm = GM;
    }
    public void Enter()
    {
        // play lost tune
        InputManager.instance.ChangeState(InputStates.LOST);
        InputManager.instance.RestartAction += _gm.EnterPlay;
        _gm.LoseEvent?.Invoke();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        // if lost tune is still running, kill it
        InputManager.instance.RestartAction -= _gm.EnterPlay;
        AudioManager.instance.SFXSource.Stop();
    }
}
