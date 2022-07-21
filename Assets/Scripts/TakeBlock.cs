using UnityEngine;

public class TakeBlock : MonoBehaviour
{
    [SerializeField] private Transform _grabPoint;
    [SerializeField] private Transform _rayPoint;
    [SerializeField] private float _rayDistance;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Blocks"))
        {
            if(Input.GetKey(KeyCode.F))
            {
                other.gameObject.transform.parent = _grabPoint;
                other.gameObject.transform.position = _grabPoint.position;
                other.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                other.gameObject.transform.GetChild(0).gameObject.GetComponent<ShowHint>()._isTaked = true;
            }
            else
            {
                other.gameObject.transform.parent = null;
                other.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                other.gameObject.transform.GetChild(0).gameObject.GetComponent<ShowHint>()._isTaked = false;
            }
        }
    }
}
