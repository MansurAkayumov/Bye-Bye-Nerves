using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private LaserBase _laserBase;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Blocks"))
        {
            DisbleLaser();
            _animator.SetBool("isPressed", true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Blocks"))
        {
            EnableLaser();
            _animator.SetBool("isPressed", false);
        }
    }

    private void DisbleLaser()
    {
        _laserBase.Disable();
    }

    private void EnableLaser()
    {
        _laserBase.Enable();
    }
}
