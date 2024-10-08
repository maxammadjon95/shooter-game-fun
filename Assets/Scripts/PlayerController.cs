using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private float _speed;
    [SerializeField] private float _gravityModifier;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _runSpeed;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private bool _invertX;
    [SerializeField] private bool _invertY;
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Animator _animator;
    [SerializeField] private GunShooter _gunShooter;
    [SerializeField] private PlayerHealthController _healthSystem;

    private Vector3 _moveInput;

    private bool _canJump, _canDoubleJump;

    public Transform CameraTransform => _cameraTransform;

    public PlayerHealthController HealthSystem => _healthSystem;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //OLD moving logic
        //_moveInput.x = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        //_moveInput.z = Input.GetAxis("Vertical") * _speed * Time.deltaTime;

        //store y velocity
        float yStore = _moveInput.y;

        Vector3 vectorMoveVertical = transform.forward * Input.GetAxis("Vertical");
        Vector3 vectorMoveHorizontal = transform.right * Input.GetAxis("Horizontal");

        _moveInput = vectorMoveVertical + vectorMoveHorizontal;
        _moveInput.Normalize();
        
        //RUN and MOVE logic
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _moveInput *= _runSpeed;
        }
        else
        {
            _moveInput *= _speed;
        }

        //Gravity logic
        _moveInput.y = yStore;
        _moveInput.y += Physics.gravity.y * _gravityModifier * Time.deltaTime;

        //JUMP logic
        //if player is on ground, gravity should not be stored
        if (_characterController.isGrounded)
        {
            _moveInput.y = Physics.gravity.y * _gravityModifier * Time.deltaTime;
        }

        _canJump = Physics.OverlapSphere(_groundCheckPoint.position,
            0.25f, _whatIsGround).Length > 0;

        //Handle Jump
        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            _moveInput.y = _jumpPower;

            //if we jumped, we can double jump
            _canDoubleJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
        {
            _moveInput.y = _jumpPower;

            //if we double jumped, we can't jump anymore
            _canDoubleJump = false;
        }

        //CHARACTER MOVE
        _characterController.Move(_moveInput * Time.deltaTime);

        //camera part
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * _mouseSensitivity;

        if (_invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (_invertY)
        {
            mouseInput.y = -mouseInput.y;
        }

        //for player rotation on Mouse
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + mouseInput.x,
            transform.rotation.eulerAngles.z);

        //for camera rotation on Mouse
        _cameraTransform.rotation = Quaternion.Euler(_cameraTransform.rotation.eulerAngles
                                                    + new Vector3(-mouseInput.y, 0f, 0f));

        //Shoot logic
        if(Input.GetMouseButtonDown(0))
        {
            _gunShooter.TryFire();
        }


        //Walking animation logic
        _animator.SetFloat("moveSpeed", _moveInput.magnitude);
        _animator.SetBool("isOnGround", _canJump);
    }
}
