using System.Collections;
using UnityEngine;

public class ObjectThrower : MonoBehaviour
{

    [SerializeField] private GameObject throwableObject;
    private GameObject _thrownObject;
    [SerializeField] private float timeBeforeThrowing;
    [SerializeField] private float timeoutTime;
    [SerializeField] private float throwSpeed;
    [SerializeField] private bool targetsPlayer;
    [SerializeField] private float distanceOffset;
    [SerializeField] private string ThrowSoundName;


    void OnEnable()
    {
        StartCoroutine(ThrowObject());
    }

    private IEnumerator ThrowObject()
    {
        _thrownObject = Instantiate(throwableObject, transform);
        _thrownObject.SetActive(true);
        yield return new WaitForSeconds(timeBeforeThrowing);
        _thrownObject.GetComponent<ObstacleCollider>().enabled = true;
        _thrownObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        AudioManager.instance.PlaySFX(ThrowSoundName);
        if (targetsPlayer)
        {
            _thrownObject.GetComponent<Rigidbody2D>().linearVelocity = throwSpeed * (GameObject.FindFirstObjectByType<PlayerController>().transform.position - transform.position + distanceOffset * Vector3.up).normalized;
        }
        else
        {
            _thrownObject.GetComponent<Rigidbody2D>().linearVelocity = throwSpeed * Vector3.down;
        }
        yield return new WaitForSeconds(timeoutTime);
        Destroy(_thrownObject);
    }
}
