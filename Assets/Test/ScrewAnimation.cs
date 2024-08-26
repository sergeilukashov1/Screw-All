using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewAnimation : MonoBehaviour
{
    public float rotationSpeed = 100f;  // Скорость вращения
    public float screwDistance = 0.5f;  // Расстояние, на которое винт откручивается/закручивается
    private bool isScrewingIn = true;   // Направление движения (true для завинчивания, false для отвинчивания)
    private bool isMoving = false;      // Флаг, показывает, движется ли винт в данный момент

    private Vector3 initialPosition;    // Начальная позиция винта
    private Vector3 targetPosition;     // Целевая позиция винта

    void Start()
    {
        initialPosition = transform.localPosition;
        targetPosition = initialPosition + new Vector3(0f,0f, -2f);
    }

    void Update()
    {
        if (isMoving)
        {
            // Вращение винта
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, step);

            // Перемещение винта к целевой позиции
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, step * screwDistance / rotationSpeed);

            // Проверка, достиг ли винт целевой позиции
            if (transform.localPosition == targetPosition)
            {
                isMoving = false;
            }
        }
    }

    void OnMouseDown()
    {
        // Если винт не движется, начинаем движение
        if (!isMoving)
        {
            isScrewingIn = !isScrewingIn;
            isMoving = true;

            // Устанавливаем целевую позицию в зависимости от направления движения
            targetPosition = isScrewingIn ? initialPosition : initialPosition + transform.up * screwDistance;
            GameManagerJoint.Instance.weHaveScrew = !isScrewingIn;
            GameManagerJoint.Instance.screwInHand = !isScrewingIn ? gameObject : null;
        }
    }

    public void ScrewInHole()
    {
        initialPosition = transform.localPosition;
        // Вращение винта
        float step = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, step);

        // Перемещение винта к целевой позиции
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, step * screwDistance / rotationSpeed);

        // Проверка, достиг ли винт целевой позиции
        if (transform.localPosition == targetPosition)
        {
            isMoving = false;
        }
    }

}
