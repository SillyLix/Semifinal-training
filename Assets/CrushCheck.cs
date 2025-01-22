using UnityEngine;

public class CrushCheck : MonoBehaviour
{
    public int wallsTouched = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            wallsTouched++;
        }

        Debug.Log("Crushed");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            wallsTouched--;
        }
    }
}
