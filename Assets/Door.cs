using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void Update()
    {
        if(Movement.key >= 10)
        {
            this.gameObject.SetActive(false);
        }
    }
}
