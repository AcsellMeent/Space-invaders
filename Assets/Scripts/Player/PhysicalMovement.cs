using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicalMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private float _moveSpeed;

    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _inputService.MoveInput() * _moveSpeed;
    }
}
