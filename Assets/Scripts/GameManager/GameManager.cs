using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] IState initalState;
    [HideInInspector] public GameManager instance;
    public GMStateManager GMSM;
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
}
