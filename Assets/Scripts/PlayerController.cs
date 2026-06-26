using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float maxTurnAngle;
    [SerializeField] private float maxDistanceFromCenter;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Animator anim;
    private bool _isFlying = false;
    private float _turnDirection;
    public bool IsDead;
    private bool _won;
    private Vector3 _winPosition;

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
        if (onOff == 1f) { _isFlying = true; anim.SetBool("Idle", false); }
        else { _isFlying = false; anim.SetBool("Idle", true); }
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
        if (!_won)
        {
            if (_isFlying) { rb.linearVelocity += -speed * Time.fixedDeltaTime * new Vector2(transform.up.x, transform.up.y); }
            rb.angularVelocity = -turnSpeed * _turnDirection;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(transform.rotation.eulerAngles.z, 180 - maxTurnAngle, 180 + maxTurnAngle));
            if (transform.position.x < -maxDistanceFromCenter) transform.position += 2 * maxDistanceFromCenter * Vector3.right;
            if (transform.position.x > maxDistanceFromCenter) transform.position -= 2 * maxDistanceFromCenter * Vector3.right;
        }
        else
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, _winPosition.x,0.1f), Mathf.Lerp(transform.position.y, _winPosition.y, 0.1f),0);
        }
    }
    public void Die()
    {
        PlayerDied?.Invoke();
        speed = speed * 0.5f;
        particle.Play();
        IsDead = true;
        StartCoroutine(PlaySFXAfter(0.2f));
    }
    private IEnumerator PlaySFXAfter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        AudioManager.instance.PlaySFX("Falling");

    }

    public void GameWon(Vector3 winPosition)
    {
        rb.gravityScale = 0;
        rb.linearDamping = 0;
        _won = true;
        _winPosition = winPosition;
    }    


}
