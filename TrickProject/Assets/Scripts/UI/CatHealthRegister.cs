using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHealthRegister : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.RegisterCatHealth(GetComponent<Canvas>());
    }
}
