using UnityEngine;

public class LaserBase : MonoBehaviour
{
    [SerializeField] private GameObject[] _lasers;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private bool _isRotate;
    
    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        if(_isRotate)
            transform.Rotate(0.0f, 0.0f, _rotateSpeed, Space.Self);
    }

    public void Disable()
    {
        foreach(GameObject laser in _lasers)
        {
            laser.SetActive(false);
        }
    }

    public void Enable()
    {
        foreach(GameObject laser in _lasers)
        {
            laser.SetActive(true);
        }
    }
}
