using System.Collections;
using UnityEngine;

public class EndGameUIChanger : MonoBehaviour
{
    [SerializeField] private GameObject winPannel;
    [SerializeField] private GameObject losePannel;
    [SerializeField] private float winSpeed, loseSpeed;



    void OnEnable()
    {
        GameManager.instance.WinEvent += ShowWin;
        GameManager.instance.LoseEvent += ShowLose;
    }
    void OnDisable()
    {
        GameManager.instance.WinEvent -= ShowWin;
        GameManager.instance.LoseEvent -= ShowLose;
    }
    void Start()
    {
        winPannel.SetActive(false);
        losePannel.SetActive(false);
    }
    public void ShowWin()
    {
        StartCoroutine(Win());
    }
    public void ShowLose()
    {
        StartCoroutine(Lose());
    }
    private IEnumerator Win()
    {
        yield return new WaitForSeconds(winSpeed);
        winPannel.SetActive(true);
    }
    private IEnumerator Lose()
    {
        yield return new WaitForSeconds(loseSpeed);
        losePannel.SetActive(true);
    }
}
