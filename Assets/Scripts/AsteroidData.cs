
using UnityEngine;

public class AsteroidData : MonoBehaviour
{
    public GameObject rock;
    public MeshRenderer tmpMesh;
    private float rotationX;
    private float rotationY;
    public Rigidbody2D rb;
    private float finalSpeedY, finalSpeedX;
    public bool freshAsteroid = true;
    private bool rendered = false;

    public void Init()
    {
        if (!GameManager.Instance.gameInProgress)
            return;
        this.gameObject.SetActive(true);
        rb.isKinematic = false;
        rb.simulated = true;

        float speedX = Random.Range(50f, 300f);
        int selectorX = Random.Range(0, 2);
        float dirX = 0;
        if (selectorX == 1) { dirX = -1; }
        else { dirX = 1; }
        finalSpeedX = speedX * dirX;
        rb.AddForce(transform.right * finalSpeedX);

        float speedY = Random.Range(50f, 300f);
        int selectorY = Random.Range(0, 2);
        float dirY = 0;
        if (selectorY == 1) { dirY = -1; }
        else { dirY = 1; }
        finalSpeedY = speedY * dirY;
        rb.AddForce(transform.up * finalSpeedY);
    }

    internal void StartRender()
    {
        //tmpMesh.enabled = false;
        if (!rendered)
        {
            float maxRotation = 35f;
            rotationX = Random.Range(-maxRotation, maxRotation);
            rotationY = Random.Range(-maxRotation, maxRotation);
            rock.GetComponent<AsteroidRock>().SetRotation(rotationX, rotationY);
            rendered = true;
        }
        rock.gameObject.SetActive(true);
    }

    internal void StopRender()
    {
        //tmpMesh.enabled = true;
        rock.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        rb.isKinematic = true;
        rock.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        GameManager.Instance.AsteroidDestroyed(this, collisionInfo.gameObject.GetComponent<Bullet>() != null);
    }
}