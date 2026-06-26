using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private float _camHeight = 1.2f;
    [SerializeField] private float camSpeed;
    void OnEnable()
    {
        GameManager.instance.NewMaxHeight += SetNewHeight;
    }
    void OnDisable()
    {
        GameManager.instance.NewMaxHeight -= SetNewHeight;
    }
    private void FixedUpdate()
    {
        if (transform.position.y - _camHeight < 0.1)
        transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, _camHeight, camSpeed), -10);
    }

    public void SetNewHeight(float newHeight)
    {
        _camHeight = newHeight;
    }
}
