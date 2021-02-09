using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] BoxSprites;
    private SpriteRenderer render;
    [SerializeField]
    private Vector2 startPosition;


    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        ReDraw();
    }


    public void ReDraw()
    {
        render.sprite = BoxSprites[Random.Range(0, BoxSprites.Length + 1)];
    }

    public void Reposition()
    {
        transform.position = startPosition;
    }
}
