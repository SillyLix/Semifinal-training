using UnityEngine;

public class PlayerDmg : MonoBehaviour
{
    [SerializeField] private float damgeAMT = -20;
    [SerializeField] private AudioClip ememyDmgAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(ememyDmgAudio);
            collision.gameObject.GetComponent<HealthManagement>().HealthChange(damgeAMT);
            Destroy(gameObject);
        }
    }
}
