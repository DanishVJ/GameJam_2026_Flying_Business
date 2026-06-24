using UnityEngine;

public class ObstacleCollider : MonoBehaviour
{
    private PlayerController _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<PlayerController>();
        if ( _player != null ) _player.Die();
    }
}
