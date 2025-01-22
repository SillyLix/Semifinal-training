using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class ShootProjectiles : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    private bool onCooldown;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !onCooldown) {
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        onCooldown = true;
        Instantiate(projectile, transform.position + new Vector3(0, 0.5f), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        onCooldown = false;
    }
}
