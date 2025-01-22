using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.ScrollRect;

public class EnemyScript : MonoBehaviour
{
    public float speed = 3, bulletSpeed, rotationSpeed, shootSpeed, range, spawnBulletDistance = 0.5f;

    [Header("Waves")]
    public float minWaveAmplitude, maxWaveAmplitude, minWaveFrequency, maxWaveFrequency;

    [Header("Debug")]
    public int moveType;

    public GameObject bullet;


    private GameObject player;

    Rigidbody2D rb;

    void Start()
    {
        moveType = Random.Range(0, 2);
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Shoot());
        if (moveType == 1)
        {
            StartCoroutine(Wave());
        }
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

    private IEnumerator Shoot()
    {
        while (true)
        {

            yield return new WaitForSeconds(shootSpeed);

            bullet = Instantiate(bullet, transform.position + transform.up * spawnBulletDistance, transform.rotation * Quaternion.Euler(0, 0, -90));
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = transform.right * bulletSpeed;
        }
    }

    IEnumerator Wave()
    {
        float startX = transform.position.x;
        float waveFrequency = Random.Range(minWaveFrequency, maxWaveFrequency);
        float waveAmplitude = Random.Range(minWaveAmplitude, maxWaveAmplitude);
        while (true)
        {
            float horizontalOffset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;

            transform.position = new Vector3(startX + horizontalOffset, transform.position.y, transform.position.z);

            yield return null;
        }
    }
}