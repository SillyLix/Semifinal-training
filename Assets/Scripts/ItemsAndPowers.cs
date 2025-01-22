using UnityEngine;
public class ItemsAndPowers : MonoBehaviour
{
    [Header("General")]
    public bool slowType; // Should this power slow the walls?
    public bool stopType; // Should this power stop the walls?

    [Header("Slow")]
    public float slowTime; // how many seconds it slows
    public float slowMultiplier; // for example: 0.5f

    [Header("MoveWall")]
    public float moveWallAmount;


    private GameObject gameManager;
    private WallScript wallScript;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        wallScript = gameManager.GetComponent<WallScript>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Player")
        {
            if (slowType)
            { // Activate slowing powers
                wallScript.SlowWalls(slowTime, slowMultiplier);
            }
            else if (stopType)
            { // Activate move powers
                wallScript.MoveWallsBack(moveWallAmount);
            }
            Destroy(gameObject);
        }
    }
    
}



