using Unity.VisualScripting;
using UnityEngine;

public class DealDamge : MonoBehaviour
{
    [SerializeField] private float damgeAMT = -100;
    [SerializeField] private AudioClip ememyDmgAudio;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(ememyDmgAudio);
            collision.gameObject.GetComponent<HealthManagement>().HealthChange(damgeAMT);
        }
    }
}
