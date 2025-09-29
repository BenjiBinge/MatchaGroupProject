using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;

    public float Horizontal;
    public float Vertical;

    public bool Attack;
    public bool ChargeAttack;
    public bool ChargeAttackRelease;

    private void Update()
    {
        Horizontal = _inputSystem.Player.Move.ReadValue<Vector2>().x;
        Vertical = _inputSystem.Player.Move.ReadValue<Vector2>().y;
        
        Attack = _inputSystem.Player.Attack.WasPressedThisFrame();
        ChargeAttack = _inputSystem.Player.Attack.IsPressed();
        ChargeAttackRelease = _inputSystem.Player.Attack.WasReleasedThisFrame();
        
    }
    
    private void Awake() { _inputSystem = new InputSystem_Actions(); }
    
    private void OnEnable() { _inputSystem.Enable(); }
    
    private void OnDisable() { _inputSystem.Disable(); }
}
