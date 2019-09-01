using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.Instance.gameInProgress)
            return;
        try
        {
            var asteroid = collision.GetComponent<AsteroidData>();
            asteroid.StartRender();
        }
        catch { }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!GameManager.Instance.gameInProgress)
            return;
        try
        {
            var asteroid = collision.GetComponent<AsteroidData>();
            asteroid.StopRender();
        }
        catch { }
    }
}
