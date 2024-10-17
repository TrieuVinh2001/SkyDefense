﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = 0.2f;//thời gian kéo dài laser
    [SerializeField] private float laserRange;
    public int damage;
    private bool isGrow = true;

    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        //LaserFaceMouse();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (!collision.isTrigger && collision.TryGetComponent<Enemy>(out Enemy enemy))
        //{
        //    isGrow = false;
        //    enemy.TakeDame(damage);
        //}

        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            isGrow = false;
            enemy.TakeDame(damage);
        }
    }

    public void UpdateLaserRange(int getDamage)
    {
        damage = getDamage;
        StartCoroutine(IncreaseLaserLenghtRoutine());
    }

    private IEnumerator IncreaseLaserLenghtRoutine()//Tăng độ dài laser
    {
        float timePassed = 0f;
        while (spriteRenderer.size.x < laserRange && isGrow)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime;

            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);

            capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), capsuleCollider2D.size.y);
            capsuleCollider2D.offset = new Vector2(Mathf.Lerp(1f, laserRange, linearT) / 2, capsuleCollider2D.offset.y);
            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    private void LaserFaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;
    }
}
