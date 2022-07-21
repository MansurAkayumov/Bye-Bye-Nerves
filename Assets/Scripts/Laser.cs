using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject _laserParticle;
    [SerializeField] private float _damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(_damage);
            Instantiate(_laserParticle, other.transform.position, Quaternion.identity);
        }
    }
}
