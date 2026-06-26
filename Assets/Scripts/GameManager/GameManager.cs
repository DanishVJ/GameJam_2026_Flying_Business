using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] IState initalState;
    public static GameManager instance;
    public GMStateManager GMSM;
    private int floorNum = 1;
    [SerializeField] public float floorSize;
    [SerializeField] public int totalFloors;
    [SerializeField] private GameObject[] prefabFloors;
    [HideInInspector] public List<GameObject> CurrentFloors;
    [HideInInspector] public bool Finished;
    private GameObject _newFloor;
    private int _randomNum;
    [HideInInspector] public float MaxPlayerHeight;
    public System.Action<float> NewMaxHeight;
    [HideInInspector] public float SpawnDistance => (floorNum - 5) * floorSize;

    public System.Action WinEvent;
    public System.Action LoseEvent;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GMSM = new GMStateManager(this);
        if (initalState == null) initalState = GMSM.stateStart;
        GMSM.Initialize(initalState);
    }

    void Update()
    {
        GMSM.Execute();
    }
    public void EnterPlay()
    {
        SceneManager.LoadScene("SampleScene");
        GMSM.ChangeState(GMSM.statePlaying);
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has closed!");
    }

    public void SpawnANewFloor()
    {
        floorNum++;
        if (floorNum >= totalFloors) 
        {
            _newFloor = Instantiate(prefabFloors[0], new Vector3(0,floorNum * floorSize), Quaternion.identity);
            Finished = true;
        }
        else
        {
            _randomNum = Random.Range(1, prefabFloors.Length);
            _newFloor = Instantiate(prefabFloors[_randomNum], new Vector3(0, floorNum * floorSize), Quaternion.identity);
        }
        CurrentFloors.Add(_newFloor);
    }
    public void ResetFloorNum()
    {
        floorNum = 1;
        MaxPlayerHeight = 1.2f;
    }
}
