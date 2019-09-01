using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private Vector3 baseScale;
    private Vector3 basePosition;

    private void Start()
    {
        baseScale = transform.localScale;
        basePosition = transform.position;
    }
    public void SetUpEdge()
    {
        int width = GameManager.Instance.width;
        int height = GameManager.Instance.height;
        transform.localScale = new Vector3(width * baseScale.x * 2, height * baseScale.y * 2, 1f);
        transform.position = new Vector3((width+4)/2 * basePosition.x, (height+4)/2 * basePosition.y, 0f);
        gameObject.SetActive(true);
    }
}
