using UnityEngine;

public class Bird : MonoBehaviour
{

    [SerializeField] private GameObject bird;
    [SerializeField] private float speed;
    [SerializeField] private float birdMaxDistance;
    private void Start()
    {
        if (speed < 0) bird.GetComponent<SpriteRenderer>().flipX = true;
    }
    void Update()
    {
        if (bird.transform.position.x * Mathf.Sign(speed) > transform.position.x * Mathf.Sign(speed) + birdMaxDistance) bird.transform.position -= birdMaxDistance * Vector3.right * Mathf.Sign(speed);
        else bird.transform.position += speed * Time.deltaTime * Vector3.right;
    }
}
