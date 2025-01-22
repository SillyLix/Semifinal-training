using System.Collections;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    [Header("Values")]
    public float closingSpeed;
    public float closingWaitTime;
    public float closingMoveTime;
    public float maxWallDistance;
    public bool wallsMoving;

    [Header("Other")]

    public GameObject rightWall;
    public GameObject leftWall;

    void Start()
    {
        StartCoroutine(Wait());
    }

    void FixedUpdate()
    {
        if (wallsMoving)
        {
            leftWall.transform.position += new Vector3(closingSpeed * Time.deltaTime, 0, 0);
            rightWall.transform.position -= new Vector3(closingSpeed * Time.deltaTime, 0, 0);
        }
    }

    public IEnumerator Wait()
    {
        wallsMoving = false;
        while  (true)
        {
            yield return new WaitForSeconds(closingWaitTime);
            wallsMoving = true;
            yield return new WaitForSeconds(closingMoveTime);
            wallsMoving = false;
        }
    }

    public void MoveWallsBack(float moveAmount)
    {
        if (rightWall.transform.position.x + moveAmount > maxWallDistance)
        {
            leftWall.transform.position = new Vector3(-maxWallDistance, 0, 0);
            rightWall.transform.position = new Vector3(maxWallDistance, 0, 0);
        }
        else
        {
            leftWall.transform.position -= new Vector3(moveAmount, 0, 0);
            rightWall.transform.position += new Vector3(moveAmount, 0, 0);
        }

    }

    public IEnumerator SlowWalls(float slowTime, float slowMultiplier)
    {
        float originalSpeed = closingSpeed;
        closingSpeed *= slowMultiplier;
        yield return new WaitForSeconds(slowTime);
        closingSpeed = originalSpeed;
    }
}
