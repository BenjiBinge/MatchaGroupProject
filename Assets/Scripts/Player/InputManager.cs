using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;

    public float Horizontal;
    public float Vertical;

    public bool Attack;
    public bool ChargeAttack;

    private void Update()
    {
        Horizontal = _inputSystem.Player.Move.ReadValue<Vector2>().x;
        Vertical = _inputSystem.Player.Move.ReadValue<Vector2>().y;
        Attack = _inputSystem.Player.Attack.WasPressedThisFrame();
        ChargeAttack = _inputSystem.Player.Attack.IsPressed();
    }
    
    private void Awake() { _inputSystem = new InputSystem_Actions(); }
    
    private void OnEnable() { _inputSystem.Enable(); }
    
    private void OnDisable() { _inputSystem.Disable(); }
}
