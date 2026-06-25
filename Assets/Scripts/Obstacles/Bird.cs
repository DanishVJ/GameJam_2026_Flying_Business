using UnityEngine;

public class Bird : MonoBehaviour
{

    [SerializeField] private GameObject bird;
    [SerializeField] private int speed;
    [SerializeField] private float birdMaxDistance;
    void Update()
    {
        if (bird.transform.position.x > transform.position.x + birdMaxDistance) bird.transform.position -= birdMaxDistance * Vector3.right * Mathf.Sign(speed);
        else bird.transform.position += speed * Time.deltaTime * Vector3.right;
    }
}
