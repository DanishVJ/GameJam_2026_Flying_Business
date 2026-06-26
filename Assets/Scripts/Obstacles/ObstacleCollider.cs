using UnityEngine;

public class ObstacleCollider : MonoBehaviour
{
    private PlayerController _player;
    [SerializeField] private string sfxName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<PlayerController>();
        if (_player != null) { if (!_player.IsDead) { _player.Die(); AudioManager.instance.PlaySFX(sfxName); } }
    }
}
