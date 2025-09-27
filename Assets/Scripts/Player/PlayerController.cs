using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private InputManager _input;
   private Rigidbody2D _rigidbody;
   
   public float moveSpeed;
   public bool isAttacking;


   private void Start()
   {
      _input = GetComponent<InputManager>();
      _rigidbody = GetComponent<Rigidbody2D>();
   }


   private void FixedUpdate()
   {
      _rigidbody.linearVelocityX = _input.Horizontal * moveSpeed;
      _rigidbody.linearVelocityY = _input.Vertical * moveSpeed;
   }
}
