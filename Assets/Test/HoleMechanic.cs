using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HoleMechanic : MonoBehaviour
{
    private bool isBusy = false;
    private void OnMouseDown()
    {
        if (GameManagerSingleton.Instance.isScrewing && GameManagerSingleton.Instance.screwMechanics.Count > 0 && !isBusy) 
        {
            GameManagerSingleton.Instance.screwMechanics[0].SetParent();
            GameManagerSingleton.Instance.screwMechanics[0].transform.DOMove(transform.position, 1f);
            //GameManagerSingleton.Instance.screwMechanics[0].transform.DORotate(transform.rotation, 1f);
            GameManagerSingleton.Instance.screwMechanics[0].transform.parent = transform;
            GameManagerSingleton.Instance.screwMechanics.RemoveAt(0);
            GameManagerSingleton.Instance.ChangeCheck();
            isBusy = true;
        }
      
    }
}
