using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class MoveToUp : MonoBehaviour
{
    [SerializeField] float force = 10;

    bool burst = false;

    Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
    }

    // Update is called once per frame
    void Update() => burst = Input.GetButton("Fire1");

    private void FixedUpdate()
    {
        if (burst)
        {
            body.AddForce(transform.up * force);
            body.velocity = new Vector3(body.velocity.x, body.velocity.y, 0);
        }
    }

    public void InverceForce(Vector3 pos) => body.velocity = (Vector2)pos - body.position;
}
