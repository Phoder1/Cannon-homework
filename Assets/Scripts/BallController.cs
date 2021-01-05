using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;
    bool rigidbodyOn;

    Vector2 velocity;
    [SerializeField] Vector2 gravity;
    [SerializeField] Material kinematicMaterial;

    public bool RigidbodyOn {
        get => rigidbodyOn;
        set {
            rigidbodyOn = value;
            Debug.Log("IsKinematic set to: " + !value);
            if (!value) {
                GetComponent<MeshRenderer>().material = kinematicMaterial;
            }
            rb.isKinematic = !value;
        }
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    public void AddForce(Vector3 force, bool rigidbodyState) {
        if (rb == null) {
            rb = GetComponent<Rigidbody2D>();
        }
        if (rigidbodyState) {
            RigidbodyOn = true;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
        else {
            RigidbodyOn = false;
            velocity = force;
        }
    }

    private void FixedUpdate() {
        if (!rigidbodyOn) {
            transform.position += (Vector3)velocity * Time.fixedDeltaTime;
            velocity += gravity * Time.fixedDeltaTime;
        }
        if (transform.position.y <= 0) {
            Destroy(gameObject);
        }
    }
}
