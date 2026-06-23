using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float maxTurnAngle;
    [SerializeField] private float maxDistanceFromCenter;
    private bool _isFlying = false;
    private float _turnDirection;

    public System.Action PlayerDied;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        if (InputManager.instance != null)
        {
            InputManager.instance.FlyAction += Fly;
            InputManager.instance.TurnAction += Turn;
        }
        else StartCoroutine(EnableAfterAFrame());
    }

    void OnDisable()
    {
        InputManager.instance.FlyAction -= Fly;
        InputManager.instance.TurnAction -= Turn;
    }

    public void Fly(float onOff)
    {
        if (onOff == 1f) _isFlying = true;
        else _isFlying = false;
    }
    public void Turn(float dir)
    {
        _turnDirection = dir;
    }
    private IEnumerator EnableAfterAFrame()
    {
        yield return new WaitForEndOfFrame();
        InputManager.instance.FlyAction += Fly;
        InputManager.instance.TurnAction += Turn;
    }

    void FixedUpdate()
    {
        if (_isFlying) { rb.linearVelocity += -speed * Time.fixedDeltaTime * new Vector2(transform.up.x, transform.up.y); }
        rb.angularVelocity = -turnSpeed * _turnDirection;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(transform.rotation.eulerAngles.z, 180-maxTurnAngle, 180+maxTurnAngle));
        if (transform.position.x < -maxDistanceFromCenter) transform.position += 2*maxDistanceFromCenter * Vector3.right;
        if (transform.position.x > maxDistanceFromCenter) transform.position -= 2*maxDistanceFromCenter * Vector3.right;
    }
    public void Die()
    {
        PlayerDied?.Invoke();
    }
    

}
