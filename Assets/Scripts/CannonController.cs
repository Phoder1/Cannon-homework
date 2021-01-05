using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] GameObject shaftEnd;
    [SerializeField] GameObject cannonBall;
    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;
    [SerializeField] private float maxHeight;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;

    private float angle;
    private float height;
    private float force;
    private bool rigidbodyState = true;
    Vector3 startPosition;

    public float Height { 
        get => height;
        set {
            height = value;
            transform.position = startPosition + Vector3.up * value * maxHeight;
        } 
    }

    public float Angle { 
        get => angle;
        set {
            angle = value;
            transform.rotation = Quaternion.Euler(Vector3.forward * ((1-value) * (maxAngle + minAngle) - minAngle));

        }
    }
    #region Buttons
    public void SetHeight(float value) {
        Height = value;
    }
    public void SetForce(float value) => force = minForce + value * (maxForce - minForce);
    public void SetAngle(float value) => Angle = value;
    public void SHOOTTTT() {
        GameObject ball = Instantiate(cannonBall,shaftEnd.transform.position,Quaternion.identity);
        Vector3 forceDirection = (shaftEnd.transform.position - transform.position).normalized * force;
        ball.GetComponent<BallController>().AddForce(forceDirection, rigidbodyState);
    }
    public void SetRigidbodyState(bool state) {
        rigidbodyState = state;
    }
    #endregion
    private void Start() {
        force = minForce;
        startPosition = transform.position;
    }
}
