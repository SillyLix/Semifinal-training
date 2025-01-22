using UnityEngine;

public class CrushCheck : MonoBehaviour
{
    public bool leftWallTouched;
    public bool rightWallTouched;

    private GameObject rightWall;
    private GameObject leftWall;

    [SerializeField] GameObject pauseMenu;
    void Start()
    {
        rightWall = GameObject.Find("Right Wall");
        leftWall = GameObject.Find("Left Wall");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == rightWall)
        {
            rightWallTouched = true;
        }
        if (collision.gameObject == leftWall)
        {
            leftWallTouched = true;
        }

        if (rightWallTouched && leftWallTouched)
        {
            pauseMenu.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == rightWall)
        {
            rightWallTouched = false;
        }
        if (collision.gameObject == leftWall)
        {
            leftWallTouched = false;
        }

        Debug.Log("Crushed");
    }
}
