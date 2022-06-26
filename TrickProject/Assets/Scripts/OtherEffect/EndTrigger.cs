using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    private SceneFader endfader;
    [SerializeField] private Canvas healthUI;
    [SerializeField] private Canvas endUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GameManager.Instance.GetPlayer().GetComponent<PlayerController>().isMove = false;
        StartCoroutine(Show());
    }


    private　IEnumerator Show()
    {
        GameManager.Instance.GetPlayer().gameObject.SetActive(false);
        yield return new WaitForSeconds(5f);
        endfader = Instantiate(Resources.Load<GameObject>("UI/Prefab/SceneFaderCanvas")).GetComponent<SceneFader>();
        endfader.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        endfader.CanvasFadeIn();
        yield return new WaitForSeconds(1f);
        healthUI.gameObject.SetActive(false);
        endUI.gameObject.SetActive(true);
        endfader.CanvasFadeOut();
    }
}
