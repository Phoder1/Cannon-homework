using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Vector2 gravity;
    [SerializeField] Material kinematicMaterial;
    
    Rigidbody2D rb;
    public Rigidbody2D RB {
        get {
        if(rb == null) {
                rb = GetComponent<Rigidbody2D>();
            }
            return rb;
        }
    }
    bool rigidbodyOn;
    public bool RigidbodyOn {

        get => rigidbodyOn;
        set {
            rigidbodyOn = value;
            Debug.Log("IsKinematic set to: " + !value);
            if (!value) {
                GetComponent<MeshRenderer>().material = kinematicMaterial;
            }
            RB.isKinematic = !value;
        }
    }

    float dragCo;
    public float DragCo { 
        get => dragCo;
        set {
            dragCo = value;
            if (rigidbodyOn) {
                RB.drag = dragCo;
            }
        } 
    }


    Vector2 velocity;
    public void AddForce(Vector3 force, bool rigidbodyState, float dragCo) {
        if (rigidbodyState) {
            RigidbodyOn = true;
            RB.AddForce(force, ForceMode2D.Impulse);
        }
        else {
            RigidbodyOn = false;
            velocity = force;
        }
        DragCo = dragCo;
    }

    private void FixedUpdate() {
        if (!rigidbodyOn) {
            transform.position += (Vector3)velocity * Time.fixedDeltaTime;
            velocity += gravity * Time.fixedDeltaTime;
            velocity *= (1 - Time.fixedDeltaTime * DragCo);
        }
        if (transform.position.y <= 0) {
            Destroy(gameObject);
        }
    }
}
