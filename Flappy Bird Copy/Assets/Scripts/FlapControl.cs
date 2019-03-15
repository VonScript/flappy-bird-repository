using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlapControl : MonoBehaviour
{
    [Tooltip("The vertical force by which the player will be pushed.")]
    public float force = 18f;

    [Tooltip("The rotation multiplier, to make the turning more accurate")]
    public float rotationMultiplier = 5f;

    [Tooltip("The minimum angle for this bird to turn")]
    public float minAngle = -90f;

    [Tooltip("The maximum angle for this bird to turn")]
    public float maxAngle = 45f;

    //Store the Rigidbody2D component in memory for easier access
    private Rigidbody2D _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        float angle = Mathf.Clamp(_rigidbody.velocity.y * rotationMultiplier, minAngle, maxAngle);
        _rigidbody.MoveRotation(angle);
    }

    //Handle all control on update for snappier responses
    private void Update() {
        if(Input.GetButtonDown("Jump")){
            Flap();
        }
    }

    public void Flap(){
        //We cannot change the velocity directly but we can replace it as a vector
        Vector2 v = _rigidbody.velocity;

        //Change the speed on the Y axis only
        v.y = force;

        //Write the vector back to the rigidbody
        _rigidbody.velocity = v;
    }
}
