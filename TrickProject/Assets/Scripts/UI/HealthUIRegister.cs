using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIRegister : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.RegisterHealthUI(this.GetComponent<Canvas>());
    }
}
