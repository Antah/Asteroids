using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform asteroidsParent;
    public GameObject asteroidData;
    public Ship rocket;
    public GameObject edges;
    public int width = 160, height = 160;
    public Camera mainCam;
    public TMP_InputField widthInput, heightInput;
    public GameObject startMenu, gameOverMenu, score;

    private int scoreNum;
    public bool gameInProgress;

    public static GameManager Instance;

    private void Start()
    {
        startMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        score.SetActive(false);
        Instance = this;
    }

    public void CreateAsteroid(float x, float y)
    {
        if (CheckPlayerProximity(x, y))
            return;
        GameObject AsteroidClone = Instantiate(asteroidData, new Vector2(x, y), transform.rotation);
        AsteroidClone.transform.SetParent(asteroidsParent);
    }

    private bool CheckPlayerProximity(float x, float y)
    {
        float deltaX = Mathf.Abs(rocket.transform.position.x - x);
        float deltaY = Mathf.Abs(rocket.transform.position.y - y);
        float renderRadius = rocket.renderZone.GetComponent<CircleCollider2D>().radius * rocket.renderZone.transform.lossyScale.x;

        return (deltaX + deltaY < 2 * renderRadius);
    }

    private void Update()
    {
        mainCam.transform.position = new Vector3(rocket.transform.position.x, rocket.transform.position.y, mainCam.transform.position.z);
    }

    public void GameOver()
    {
        foreach (Transform child in asteroidsParent)
        {
            child.GetComponent<AsteroidData>().rb.simulated = false;
        }
        gameInProgress = false;
        gameOverMenu.SetActive(true);
    }

    public void AsteroidDestroyed(AsteroidData asteroid, bool addPoints)
    {
        if (addPoints)
        {
            scoreNum += 100;
            score.GetComponent<TextMeshProUGUI>().text = "Score: " + scoreNum;
        }
        float x = (Random.Range(0, width) - width / 2) * 40f;
        float y = (Random.Range(0, height) - height / 2) * 40f;
        while (CheckPlayerProximity(x, y))
        {
            x = (Random.Range(0, width) - width / 2) * 40f;
            y = (Random.Range(0, height) - height / 2) * 40f;
        }
        asteroid.transform.position = new Vector3(x, y, 0);
        asteroid.Init();
    }

    public void SetWidth()
    {
        width = System.Int32.Parse(widthInput.text);
    }

    public void SetHeight()
    {
        height = System.Int32.Parse(heightInput.text);
    }

    public void StartGame()
    {
        gameInProgress = true;
        startMenu.SetActive(false);
        rocket.gameObject.SetActive(true);
        score.GetComponent<TextMeshProUGUI>().text = "Score: " + scoreNum;
        score.SetActive(true);

        foreach (Transform child in edges.transform)
        {
            child.GetComponent<Edge>().SetUpEdge();
        }

        for (int i = -(width / 2); i < width / 2; i++)
        {
            float xPos = i * 40f;
            for (int j = -(height / 2); j < height / 2; j++)
            {
                float yPos = j * 40f;
                CreateAsteroid(xPos, yPos);
            }
        }
        foreach (Transform child in asteroidsParent)
        {
            child.GetComponent<AsteroidData>().Init();
        }
    }

    public void RestartGame()
    {
        while (asteroidsParent.childCount > 0)
        {
            var child = asteroidsParent.GetChild(0);
            Destroy(child.gameObject);
            child.SetParent(null);
        }

        scoreNum = 0;
        gameOverMenu.SetActive(false);
        startMenu.SetActive(true);
        rocket.ResetRocket();
        rocket.gameObject.SetActive(false);
        score.SetActive(false);
    }
}