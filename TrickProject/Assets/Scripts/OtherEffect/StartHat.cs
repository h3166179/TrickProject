using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHat : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float hideSpeed = 2f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.Instance.GetPlayer().GetComponent<PlayerController>().isMove = false;
        GameManager.Instance.GetPlayer().GetComponent<PlayerController>().isState = true;
        StartCoroutine(HideHat());
    }

    private IEnumerator HideHat()
    {
        float time = 1;
        while(time>0)
        {
            time -= Time.deltaTime/hideSpeed;
            spriteRenderer.color = new Color(1, 1, 1, time);
            yield return null;
        }
        //数值修正
        spriteRenderer.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.GetPlayer().GetComponent<PlayerController>().isMove = true;
        GameManager.Instance.GetPlayer().GetComponent<PlayerController>().isState = false;
        GameManager.Instance.GetPlayer().GetComponent<PlayerController>().isJump = true;
        Destroy(this.gameObject);
    }
}
