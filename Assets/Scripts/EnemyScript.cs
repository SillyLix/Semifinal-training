using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.ScrollRect;

public class EnemyScript : MonoBehaviour
{
    public float speed = 3, rotationSpeed, shootSpeed, range;

    [Header("Debug")]
    public int moveType;

    private GameObject player;

    Rigidbody2D rb;
    [SerializeField] private GameObject animaiton;

    void Start()
    {
        moveType = Random.Range(0, 2);
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, -speed); // Move down

        Rotate();
    }

    void Rotate()
    {
        Vector3 direction = player.transform.position - new Vector3(transform.position.x + 0.49f, transform.position.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}