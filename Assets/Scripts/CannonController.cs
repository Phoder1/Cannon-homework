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
    [SerializeField] private float minFireRate;
    [SerializeField] private float maxFireRate;
    [SerializeField] private float minGravityScale;
    [SerializeField] private float maxGravityScale;
    [SerializeField] private float maxDrag;

    private float angle;
    private float height;
    private float force;
    private bool rigidbodyState = true;
    private bool autoFire = false;
    private float autoFireRemainingTime;
    private float fireRate;
    private float dragCo;
    private float gravityScale;
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
            transform.rotation = Quaternion.Euler(Vector3.forward * ((1 - value) * (maxAngle + minAngle) - minAngle));

        }
    }
    #region Buttons
    public void SetHeight(float value) {
        Height = value;
    }
    public void SetForce(float value) => force = minForce + value * (maxForce - minForce);
    public void SetAngle(float value) => Angle = value;
    public void FireButton() {
        if (!autoFire) {
            Fire();
        }
    }

    public void SetRigidbodyState(bool state) {
        rigidbodyState = state;
    }
    public void SetAutoFire(bool state) {
        autoFire = state;
        if (autoFire) {
            Fire();
            autoFireRemainingTime = 1 / fireRate;
        }
    }
    public void SetFireRate(float rate) {
        fireRate = minFireRate + rate * (maxFireRate - minFireRate);
    }
    public void SetDrag(float drag) {
        dragCo = drag * maxDrag;
    }
    public void SetGravityScale(float gravity) {
        gravityScale = minGravityScale + (1 - gravity) * (maxGravityScale - minGravityScale);
    }
    #endregion

    private void Fire() {
        GameObject ball = Instantiate(cannonBall, shaftEnd.transform.position, Quaternion.identity);
        Vector3 forceDirection = (shaftEnd.transform.position - transform.position).normalized * force;
        ball.GetComponent<BallController>().AddForce(forceDirection, rigidbodyState, dragCo, gravityScale);

    }
    private void Start() {
        gravityScale = minGravityScale + 0.75f * (maxGravityScale - minGravityScale);
        force = minForce;
        fireRate = minFireRate;
        startPosition = transform.position;
    }
    private void Update() {
        if (autoFire) {
            autoFireRemainingTime -= Time.deltaTime;
            if (autoFireRemainingTime <= 0) {
                Fire();
                autoFireRemainingTime = 1 / fireRate;
            }
        }
    }
}
