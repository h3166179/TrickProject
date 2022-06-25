using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPointRegister : MonoBehaviour
{
    [SerializeField]private List<Transform> loadPoint = new List<Transform>();

    private void Start()
    {
        GameManager.Instance.loadPoint = this.loadPoint;
    }
}
