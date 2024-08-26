using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastJoint : MonoBehaviour
{
    public float rayDistance = 100f;
    [SerializeField] private List<HingeJoint> hingeJoints = new List<HingeJoint>();
    [SerializeField] private BoxCollider boxCollider;
    void Start()
    {   
        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance);
        System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));
        
        foreach (RaycastHit hit in hits)
        {
                bool jointCreated = false;
                // Проверяем, есть ли у объекта Rigidbody
                Rigidbody hitRigidbody = hit.collider.GetComponent<Rigidbody>();
                if (hitRigidbody != null)
                {
                    // Проверяем, есть ли уже HingeJoint
                    if (jointCreated == false)
                    {
                        
                        
                        // Создаем HingeJoint на объекте
                        var hingeJoint = hitRigidbody.gameObject.AddComponent<HingeJoint>();
                        
                        // Устанавливаем якорь в месте соприкосновения
                        hingeJoint.anchor = hit.transform.InverseTransformPoint(hit.point);
                        
                        // Устанавливаем ось вращения (например, вертикальную)
                        hingeJoint.axis = new Vector3(0,0,1f);
                        
                        // Настройки hinge joint
                        hingeJoint.useMotor = false;  // Можно включить мотор для управления вращением
                        
                        // Отключаем автоматическое конфигурирование якоря
                        hingeJoint.autoConfigureConnectedAnchor = false;
                        hingeJoint.connectedAnchor = hit.point;
                        
                        hingeJoints.Add(hingeJoint);
                        jointCreated = true;
                    }
                }   
        }

        // Для визуализации луча в редакторе и во время игры
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
    }

   public void DestroyJoint()
    {
        foreach (HingeJoint hingeJoint in hingeJoints)
        {
            Destroy(hingeJoint);
        }
        hingeJoints.Clear();
        boxCollider.isTrigger = false;
    }
   
}
