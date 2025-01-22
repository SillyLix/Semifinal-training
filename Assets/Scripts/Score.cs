using TMPro;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class Score : MonoBehaviour
{

    private static Score _instance;

    public static Score Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<Score>();

                if (_instance == null)
                {
                    Debug.LogError("Score instance not found, creating new instance.");
                    GameObject singletonObject = new GameObject(typeof(Score).Name);
                    _instance = singletonObject.AddComponent<Score>();
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;

        }
    }

    [SerializeField] private int __score;
    [SerializeField] private int __time;

    [SerializeField] private TextMeshProUGUI __scoreText;
    private void Start()
    {
        __score = 0;
        __scoreText.text = $"kills: {__score} Time {__time}";
        StartCoroutine(TimeScore());


    }
    public void AddScore()
    {
        __score++;
        __scoreText.text = $"kills: {__score} Time {__time}";
    }

    IEnumerator TimeScore()
    {
        while (true)
        {
             __scoreText.text = $"kills: {__score} Time {__time}";
            yield return new WaitForSeconds(1);
            __time++;

        }
    }

}
