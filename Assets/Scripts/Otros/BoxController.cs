using System;
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

    private void Start()
    {
        GameManager.Instance.LevelRestarted += ReSet;
    }

    private void ReSet()
    {
        ReDraw();
        Reposition();
    }

    public void ReDraw()
    {
        render.sprite = BoxSprites[UnityEngine.Random.Range(0, BoxSprites.Length)];
    }

    public void Reposition()
    {
        transform.position = startPosition;
    }
}
