using UnityEngine;

public class ShowHint : MonoBehaviour
{
    [SerializeField] private GameObject _hint;
    [HideInInspector] public bool _isTaked;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && _isTaked == false)
        {
            Show();
        }
        else if(_isTaked)
            Hide();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Hide();
        }
    }

    private void Show()
    {
        _hint.SetActive(true);
    }

    private void Hide()
    {
        _hint.SetActive(false);
    }
}
