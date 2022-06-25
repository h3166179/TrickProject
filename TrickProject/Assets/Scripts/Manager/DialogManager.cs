using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : Singleton<DialogManager>
{
    private TextAsset dialogAsset;
    private GameObject dialogPrefab;
    private CanvasGroup canvas;

    private void Start()
    {
        dialogAsset = Resources.Load<TextAsset>("Txt/Dialog");
        dialogPrefab = Resources.Load<GameObject>("UI/Prefab/DialogCanvas");
    }

    public void DialogPlay(int index)
    {
        canvas = GameObject.Instantiate(dialogPrefab).GetComponent<CanvasGroup>();
        Text dialog_text = canvas.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        string[] texts = dialogAsset.text.Split('\r');
        StartCoroutine(ShowDialog(texts[index]));
    }


    private IEnumerator ShowDialog(string txt)
    {
        yield return null;
    }
}
