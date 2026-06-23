using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    private InputStates _currentInputState;
    public System.Action StartAction;
    public System.Action RestartAction;
    public System.Action<float> FlyAction;
    public System.Action<float> TurnAction;
    public static InputManager instance;
    void Awake()
    {
        if (instance == null) { instance = this; }
        else Destroy(gameObject);
    }
    void OnEnable()
    {
        ChangeState(InputStates.PLAYING);
    }


    public void ChangeState(InputStates state)
    {
        _currentInputState = state;
    }

    public void OnSpace(InputValue value)
    {
        if (_currentInputState == InputStates.START)
        {
            StartAction?.Invoke();
        }
        else if (_currentInputState == InputStates.LOST)
        {
            RestartAction.Invoke();
        }
        else if (_currentInputState == InputStates.PLAYING)
        {
            FlyAction?.Invoke(value.Get<float>());
        }
    }
    public void OnTurn(InputValue value)
    {
        if (_currentInputState == InputStates.PLAYING)
        {
            TurnAction?.Invoke(value.Get<float>());
        }
    }

}
