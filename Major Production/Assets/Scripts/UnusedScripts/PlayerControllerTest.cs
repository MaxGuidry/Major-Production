using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerTest : MonoBehaviour
{
    public GameObject UiInventory;
    private Transform _camTransform;
    private float _currentSpeed;
    public float RunSpeed;

    public float SpeedSmoothTime;
    private float _speedSmoothVelocity;

    public float TurnSmoothTime;
    private float _turnSmoothVelocity;

    public float WalkSpeed;

    // Use this for initialization
    private void Start()
    {
        WalkSpeed = 2;
        RunSpeed = 6;

        TurnSmoothTime = 0.2f;
        SpeedSmoothTime = 0.1f;
        _camTransform = Camera.main.transform;
        UiInventory.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        UiInventory.SetActive(Input.GetKey(KeyCode.Tab));
        var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        var inputDir = input.normalized;

        if (inputDir != Vector3.zero)
        {
            var targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + _camTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation,
                                        ref _turnSmoothVelocity, TurnSmoothTime);
        }

        var running = Input.GetKey(KeyCode.LeftShift);
        var targetSpeed = (running ? RunSpeed : WalkSpeed) * inputDir.magnitude;
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref _speedSmoothVelocity, SpeedSmoothTime);

        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime, Space.World);
    }
}