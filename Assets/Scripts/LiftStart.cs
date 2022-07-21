using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftStart : MonoBehaviour
{
    [SerializeField] private GameObject _audioObject;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _cameraFadeAnimator;
    [SerializeField] private GameObject _cameraFade;
    [SerializeField] private GameObject _liftParticle;

    [HideInInspector] public bool _liftActivated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Invoke("NextLevel", 2f);
            Instantiate(_liftParticle, other.gameObject.transform.position, Quaternion.identity);
            _liftActivated = true;
            CameraFade();
            _animator.SetBool("isStart", true);
            GameObject audioObject = Instantiate(_audioObject, Vector3.zero, Quaternion.identity);
            audioObject.GetComponent<AudioSource>().clip = audioObject.GetComponent<AudioPlayer>()._clips[4];
            MusicScenes.Instance.StopMusic();
        }
    }

    private void NextLevel()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex+1);
    }

    private void CameraFade()
    {
        _cameraFade.SetActive(true);
        _cameraFadeAnimator.SetBool("isOpen", true);
    }
}
