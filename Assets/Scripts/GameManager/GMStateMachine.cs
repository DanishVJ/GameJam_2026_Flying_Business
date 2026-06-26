public class GMStateManager
{
    private GameManager _gm;
    private IState currentState;
    public GMS_Start stateStart;
    public GMS_Playing statePlaying;
    public GMS_Lost stateLost;
    public GMS_Won stateWon;

    public GMStateManager(GameManager GM)
    {
        _gm = GM;
        stateStart = new GMS_Start(GM);
        statePlaying = new GMS_Playing(GM);
        stateWon = new GMS_Won(GM);
        stateLost = new GMS_Lost(GM);
    }

    public void Initialize(IState state)
    {
        currentState = state;
        state.Enter();
    }
    public void ChangeState(IState state)
    {
        currentState.Exit();
        currentState = state;
        state.Enter();
        
    }

    public void Execute()
    {
        currentState.Execute();
    }
    


}
