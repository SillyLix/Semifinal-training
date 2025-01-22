using UnityEngine;
using UnityEngine.Audio;

public class HealthManagement : MonoBehaviour
{
    [SerializeField] private float health = 100;
    private float minHealth = 0;
    [SerializeField] private float maxHealth = 100;

    [SerializeField] private GameObject diedMenu;
    private AudioSource audioSource;
    private AudioClip deadMetallic;
    private void Start()
    {
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void HealthChange(float amtChanged)
    {


        if (health + amtChanged > maxHealth)
        {
            health = maxHealth;
        }
        else {
            health += amtChanged;
        }

        if (health <= minHealth)
        {
            if (!gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                Score.Instance.AddScore();
            }
            else
            {
                diedMenu.SetActive(true);
                Time.timeScale = 0;
            }
            if (deadMetallic != null)
            {
                audioSource.PlayOneShot(deadMetallic);
            }

        }
        else print("player is dead");



        print(health);
    }
}
