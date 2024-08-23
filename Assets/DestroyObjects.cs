using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private int count = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerScript>())
        {
            Destroy(other);
            count++;
            GameManagerSingleton.Instance.player.RemoveAt(0);
            if(count < 10)
            {
                GameManagerSingleton.Instance.OpenLoseMenu();
            }
        }
    }
}
