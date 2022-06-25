using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : Singleton<DialogManager>
{
    private TextAsset dialogAsset;
    private GameObject dialogPrefab;
    private CanvasGroup canvas;
    [SerializeField] private float dialogSpeed = 0.5f;
    [SerializeField] private float showTime = 1f;

    private void Start()
    {
        dialogAsset = Resources.Load<TextAsset>("Txt/Dialog");
        dialogPrefab = Resources.Load<GameObject>("UI/Prefab/DialogCanvas");
    }

    public void DialogPlay(int index)
    {
        canvas = GameObject.Instantiate(dialogPrefab).GetComponent<CanvasGroup>();
        Text dialog_text = canvas.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        string[] texts = dialogAsset.text.Split('\n');
        string[] result_txt = new string[15];
        int num = 0;
        for(int i=0;i<texts.Length;i++)
        {
            if (!texts[i].Equals("\r"))
            {
                result_txt[num] += texts[i];
            }
            else
            {
                result_txt[num] = result_txt[num].Substring(0, result_txt[num].Length - 3);
                num++;
            }
        }
        StartCoroutine(ShowDialog(result_txt[index]));
    }


    private IEnumerator ShowDialog(string txt)
    {
        string[] dialog = txt.Split('\r');
        int index = 0;
        Text dialog_text = canvas.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        while (index<dialog.Length)
        {
            dialog_text.text = dialog[index];
            float time = 0;
            while (time < 1)
            {
                time += Time.deltaTime / dialogSpeed;
                canvas.alpha = time;
                yield return null;
            }
            //精度修正
            canvas.alpha = 1;
            time = 1;
            yield return new WaitForSeconds(showTime);
            while (time >0)
            {
                time -= Time.deltaTime / dialogSpeed;
                canvas.alpha = time;
                yield return null;
            }
            //精度修正
            canvas.alpha = 0;
            index++;
        }
        Destroy(canvas.gameObject);
    }
}
