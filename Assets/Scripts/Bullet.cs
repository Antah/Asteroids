using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * 500);
    }

    public void KillOldBullet()
    {
        Destroy(3.0f);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.GetComponent<AsteroidData>() != null)
            Destroy();
    }

    private void Destroy(float time = 0f)
    {
        StartCoroutine(DestroyWithDelay(time));
    }

    IEnumerator DestroyWithDelay(float time)
    {
        yield return new WaitForSeconds(time);
        if (GameManager.Instance.gameInProgress)
        {
            Destroy(gameObject);
            transform.SetParent(null);
        }
    }
}
