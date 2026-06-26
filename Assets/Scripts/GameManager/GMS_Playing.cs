using UnityEngine;
using UnityEngine.SceneManagement;

public class GMS_Playing : IState
{
    private GameManager _gm;
    private GameObject _player;
    private bool _sceneHasLoaded = false;
    public GMS_Playing(GameManager GM)
    {
        _gm = GM;
    }
    public void Enter()
    {
        // play playing music
        InputManager.instance.ChangeState(InputStates.PLAYING);
        
        SceneManager.sceneLoaded += EnterAfterLoad;
        AudioManager.instance.PlayMusic("Music");
        _sceneHasLoaded = false;


    }

    public void Execute()
    {
        if (_player != null && _sceneHasLoaded) 
        {
            if (_player.transform.position.y > _gm.SpawnDistance && !_gm.Finished)
            {
                _gm.SpawnANewFloor();
            }
            if (_player.transform.position.y > _gm.MaxPlayerHeight)
            {
                _gm.MaxPlayerHeight = _player.transform.position.y;
                _gm.NewMaxHeight?.Invoke(_gm.MaxPlayerHeight);
            }
            if (_player.transform.position.y > _gm.floorSize * _gm.totalFloors && _gm.Finished)
            {
                _gm.GMSM.ChangeState(_gm.GMSM.stateWon);
                _player.GetComponent<PlayerController>().GameWon(_gm.CurrentFloors[_gm.CurrentFloors.Count -1].transform.position);
            }
        }
    }

    public void Exit()
    {
        // end playing music
        SceneManager.sceneLoaded -= EnterAfterLoad;
        _player.GetComponent<PlayerController>().PlayerDied -= PlayerDied;
        AudioManager.instance.SFXSource.Stop();
        _gm.Finished = false;
    }
    public void EnterAfterLoad(Scene scene, LoadSceneMode mode)
    {
        _gm.Finished = false;
        _gm.CurrentFloors.Clear();
        _gm.ResetFloorNum();
        _gm.SpawnANewFloor();
        _gm.SpawnANewFloor();
        _player = GameObject.FindWithTag("Player");
        _player.GetComponent<PlayerController>().PlayerDied += PlayerDied;
        _sceneHasLoaded = true;
        
    }

    public void PlayerDied()
    {
        _gm.GMSM.ChangeState(_gm.GMSM.stateLost);
    }
}
