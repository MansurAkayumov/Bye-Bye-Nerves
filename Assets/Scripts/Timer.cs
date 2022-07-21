using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    [SerializeField] private GameObject _loseMenu;
    public float _timeLeft;
    private Slider _slider;

    private bool _overed;

    private void Start()
    {
        _slider = GetComponent<Slider>();

        if(Instance == null)
            Instance = this;
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;
        _slider.value = _timeLeft; 

        if(_timeLeft <= 0 && !_overed && FindObjectOfType<LiftStart>()._liftActivated == false)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        _overed = true;
        _loseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

}
