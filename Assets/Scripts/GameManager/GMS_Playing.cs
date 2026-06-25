using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GMS_Playing : IState
{
    private GameManager _gm;
    private GameObject _player;
    public GMS_Playing(GameManager GM)
    {
        _gm = GM;
    }
    public void Enter()
    {
        // play playing music
        InputManager.instance.ChangeState(InputStates.PLAYING);
        _gm.CurrentFloors.Clear();
        SceneManager.sceneLoaded += EnterAfterLoad;
        _gm.Finished = false;
        
    }

    public void Execute()
    {
        if (_player != null && !_gm.Finished) 
        {
            if (_player.transform.position.y > _gm.SpawnDistance)
            {
                _gm.SpawnANewFloor();
            }
        }
    }

    public void Exit()
    {
        // end playing music
        SceneManager.sceneLoaded -= EnterAfterLoad;
        _player.GetComponent<PlayerController>().PlayerDied -= PlayerDied;
    }
    public void EnterAfterLoad(Scene scene, LoadSceneMode mode)
    {
        _gm.SpawnANewFloor();
        _gm.SpawnANewFloor();
        _player = GameObject.FindWithTag("Player");
        _player.GetComponent<PlayerController>().PlayerDied += PlayerDied;
    }

    public void PlayerDied()
    {
        _gm.GMSM.ChangeState(_gm.GMSM.stateLost);
    }
}
