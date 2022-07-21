using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckCollider;
    [SerializeField] private Animator _cameraFadeAnimator;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private GameObject _audioObject;
    [SerializeField] private GameObject _lavaParticle;
    [SerializeField] private GameObject _appleParticle;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _health;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _groundCheckRadius = .2f;
    private float _move;

    private bool _isGrounded;
    private bool _isFacingRight = true;

    [Header("Wall Jump")]
    [SerializeField] private Transform _wallGrabPoint;
    [SerializeField] private float _wallJumpTime = 0.2f;
    private float _wallJumpCounter;
    private bool _canGrab, _isGrabbing; 
    private float _gravityStore;
    private bool _isJumping;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _gravityStore = _rigidbody.gravityScale;
    }

    private void FixedUpdate()
    {
        if(_wallJumpCounter <= 0)
        {
            Move();
            Jump();
            GroundCheck();
            WallJump();
        }
        else
        {
            _wallJumpCounter -= Time.deltaTime;
        }
    }

    private void Move()
    {
        _move = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector2(_move * _moveSpeed, _rigidbody.velocity.y);

        if(_move < 0 && _isFacingRight == true)
            Flip();
        if(_move > 0 && _isFacingRight == false)
            Flip();

        if(_move > 0 && _isGrabbing == false && _isGrounded || _move < 0 && _isGrabbing == false && _isGrounded)
            _animator.SetBool("isRun", true);
        else
            _animator.SetBool("isRun", false);
    }

    private void Jump()
    {
        if(Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            _isJumping = true;
            // SpawnAudioObject(1);a
        }
    }

    private void GroundCheck()
    {
        _isGrounded = false; 
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheckCollider.position, _groundCheckRadius, _groundLayer);
        if(colliders.Length > 0)
        {
            _isGrounded = true;
            if(_isJumping)
            {
                SpawnAudioObject(3);
                _isJumping = false;
            }
        }
    }

    private void WallJump()
    {
        _canGrab = Physics2D.OverlapCircle(_wallGrabPoint.position, .2f, _groundLayer);

        _isGrabbing = false;
        if(_canGrab && !_isGrounded)
        {
            if((_isFacingRight && _move > 0) || (!_isFacingRight && _move < 0))
            {
                _isGrabbing = true;
            }
        }

        if(_isGrabbing)
        {
            _rigidbody.gravityScale = 0f;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetBool("isWallJump", false);
            _animator.SetBool("isRun", false);
            _animator.SetBool("isWallSlide", true);
            Flip();

            if(Input.GetKey(KeyCode.Space))
            {
                _animator.SetBool("isWallJump", true);
                _wallJumpCounter = _wallJumpTime;
                _rigidbody.velocity = new Vector2(-_move * _moveSpeed , _jumpForce);
                SpawnAudioObject(1);
                _rigidbody.gravityScale = _gravityStore;
                _isGrabbing = false;
                Flip();
            }
        }
        else
        {
                _rigidbody.gravityScale = _gravityStore;
                _animator.SetBool("isWallSlide", false);
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        if(transform.GetChild(1).childCount > 1)
        {
            Vector3 theChildScale = transform.GetChild(1).GetChild(1).GetChild(1).localScale;
            theChildScale.x *= -1;
            transform.GetChild(1).GetChild(1).GetChild(1).localScale = theChildScale;   
        } 
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        UpdateHealth();
    }

    private void Lose()
    {
        _cameraFadeAnimator.SetBool("isOpen", true);
        Invoke("Restart", .2f);
    }

    private void UpdateHealth()
    {
        if(_health <= 0)
            Lose();
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Lava"))
        {
            Instantiate(_lavaParticle, transform.position, Quaternion.identity);
            TakeDamage(1f);
        }

        if(other.gameObject.CompareTag("Apple"))
        {
            Timer.Instance._timeLeft += 5f;
            Instantiate(_appleParticle, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            SpawnAudioObject(0);
        }

        if(other.gameObject.CompareTag("Invisible"))
        {
            other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Fake"))
        {
            Color platformColor = other.gameObject.GetComponent<SpriteRenderer>().color;
            platformColor.a = .2f;
            other.gameObject.GetComponent<SpriteRenderer>().color = platformColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Fake"))
        {
            Color platformColor = other.gameObject.GetComponent<SpriteRenderer>().color;
            platformColor.a = 1f;
            other.gameObject.GetComponent<SpriteRenderer>().color = platformColor;
        }
    }

    private void SpawnAudioObject(int number)
    {
        GameObject audioObject = Instantiate(_audioObject, Vector3.zero, Quaternion.identity);
        audioObject.GetComponent<AudioSource>().clip = audioObject.GetComponent<AudioPlayer>()._clips[number];
    }
}
