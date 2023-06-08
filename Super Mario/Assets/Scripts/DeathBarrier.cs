using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggeredEnter2D(Collider2D other) //if mario goes underground then reset the level
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.ResetLevel(1f);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
