using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoleBehaviourJoint : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (GameManagerJoint.Instance.weHaveScrew)
        {
            GameManagerJoint.Instance.screwInHand.transform.position = transform.position;
            GameManagerJoint.Instance.screwInHand.GetComponent<RaycastJoint>().DestroyJoint();
            GameManagerJoint.Instance.screwInHand.GetComponent<ScrewAnimation>().ScrewInHole();
        }
    }
}
