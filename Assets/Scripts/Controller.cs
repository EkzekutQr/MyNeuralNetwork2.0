using UnityEngine;

public class Controller : MonoBehaviour
{
    //public Vector2 direction;
    Rigidbody2D rb;
    [SerializeField] float speed = 1f;
    [SerializeField] float slowMultiply = 0.05f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    //private void FixedUpdate()
    //{
    //    rb.velocity = direction;
    //}
    public void Move(Vector2 direction)
    {
        rb.AddForce(direction.normalized /** Time.fixedDeltaTime*/ * speed, ForceMode2D.Force);
        rb.AddForce(-rb.velocity * slowMultiply);
    }
}
