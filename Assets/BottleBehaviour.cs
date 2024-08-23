using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BottleBehaviour : MonoBehaviour
{
    private int counter = 0;
    [SerializeField] private TMP_Text _counterText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerScript>())
        {
            other.gameObject.SetActive(false);
            counter++;
            _counterText.text = counter + "/10";
            Debug.Log(counter);
            Destroy(other);
            GameManagerSingleton.Instance.player.RemoveAt(0);
            if (counter == 10)
            {
                GameManagerSingleton.Instance.OpenWonMenu();
            }
        }
    }
}
