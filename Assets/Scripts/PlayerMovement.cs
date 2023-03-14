using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D _rigidbody2D;
    private PlayerAnimation _playerAnimation;
    private Vector2 _movement;
    
    [SyncVar] private float _movementX;
    [SyncVar] private float _movementY;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        if (isOwned)
        {
            _movementX = Input.GetAxis("Horizontal");
            _movementY = Input.GetAxis("Vertical");            
        }

        CmdSendMovementToServer(_movementX , _movementY);

        _movement = new Vector2(_movementX, _movementY);
        _playerAnimation.ChangeState(_movement);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _movement.normalized * moveSpeed;
    }
    
    [Command]
    void CmdSendMovementToServer(float movementX, float movementY)
    {
        _movementX = movementX;
        _movementY = movementY;
    }
}
