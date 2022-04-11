using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextShowPooling : MonoBehaviour
{
    static public TextShowPooling singleton;
    List<TextMesh> textMesh;

    void Start()
    {
        singleton = this;
        textMesh = new List<TextMesh>();
        for (int i = 0; i < 10; i++) CreateTextMesh();
    }
    private TextMesh CreateTextMesh()
    {
        GameObject abc = new GameObject();
        abc.SetActive(false);
        abc.AddComponent<TextMeshManager>();
        abc.transform.localScale = new Vector3(-1, 1, 1);
        abc.transform.SetParent(this.transform.parent);
        abc.transform.parent = this.transform;
        TextMesh _textMesh = abc.AddComponent<TextMesh>();
        _textMesh.color = Color.red;
        _textMesh.fontSize = 12;
        textMesh.Add(_textMesh);
        return _textMesh;
    }
    public TextMesh getTextMesh()
    {
        foreach (var item in textMesh)
        {
            if (!item.gameObject.activeSelf)
            {
                return item;
            }
        }
        TextMesh returnText = CreateTextMesh();
        return returnText;
    }
}
