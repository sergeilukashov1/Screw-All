using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScrewMechanic : MonoBehaviour
{
    [SerializeField] private GameObject _screwModel;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        startPosition = _screwModel.transform.position;
        startRotation = _screwModel.transform.rotation;
    }

    private void OnMouseDown()
    {
        if (GameManagerSingleton.Instance.isScrewing == false && GameManagerSingleton.Instance.screwMechanics.Count == 0)
        {
            GameManagerSingleton.Instance.ChangeCheck();
            GameManagerSingleton.Instance.AddScrewToList(gameObject.GetComponent<ScrewMechanic>());
            _screwModel.transform.parent = null;
            _screwModel.transform.DOLocalMoveZ(-1.5f, 1f).SetRelative(true);
            _screwModel.transform.DOLocalRotate(new Vector3(500f, 0f, 0f), 1f).SetRelative(true);
        }
        else
        if(GameManagerSingleton.Instance.isScrewing == true && GameManagerSingleton.Instance.screwMechanics[0] == gameObject.GetComponent<ScrewMechanic>())
        {
            _screwModel.transform.DOLocalMoveZ(1.5f, 1f).SetRelative(true);
            _screwModel.transform.DOLocalRotate(new Vector3(-500f, 0f, 0f), 1f).SetRelative(true).OnComplete(() =>
            {
                _screwModel.transform.position = startPosition;
                _screwModel.transform.rotation = startRotation;
                _screwModel.transform.parent = transform;
                GameManagerSingleton.Instance.ChangeCheck();
                GameManagerSingleton.Instance.screwMechanics.RemoveAt(0);
            });
        }
    }

    public void SetParent()
    {
        _screwModel.transform.parent = transform;
        _screwModel.transform.DOLocalMoveZ(1.5f, 1f).SetRelative(true);
        _screwModel.transform.DOLocalMove(new Vector3(0f,-1f,0f), 1f);
        _screwModel.transform.DOLocalRotate(new Vector3(0f,0f,0f), 1f);
        GetComponent<CapsuleCollider>().isTrigger = true;
    }
}
