using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button fetchButton,
                                    showButton,
                                    nextButton,
                                    previousButton;
    [SerializeField] private RectTransform showDataPanel;
    [SerializeField] private TextMeshProUGUI nameText,
                                             displayNameText,
                                             languageText,
                                             interestIDText;
    [SerializeField] private Image picture;

    private InterestList data;
    private int i = 0;
    private int dataLength;

    private void Start()
    {
        fetchButton.onClick.AddListener(FetchJsonData);
        showButton.onClick.AddListener(ShowJsonData);
        nextButton.onClick.AddListener(PreviewNext);
        previousButton.onClick.AddListener(PreviewPrevious);
        JSonManager.Instance.SaveData();
    }

    private void FetchJsonData()
    {
        data = JSonManager.Instance.interestData;
        dataLength = data.interest.Length;
        ColorBlock c = fetchButton.colors;
        c.normalColor = Color.green;
        c.selectedColor = Color.green;
        fetchButton.colors = c;
        fetchButton.onClick.RemoveListener(FetchJsonData);
    }

    private void ShowJsonData()
    {
        showDataPanel.gameObject.SetActive(true);
        UpdateDataOnCanvas();
    }

    private void PreviewNext()
    {
        if (i < dataLength - 1 && i > -1)
            ++i;
        else
            i = 0;

        UpdateDataOnCanvas();
    }

    private IEnumerator SetPic(string url)
    {
        WWW web = new WWW(url);
        yield return web;
        if (web.error == null && web.texture != null)
        {
            picture.sprite = Sprite.Create(web.texture, new Rect(0, 0, web.texture.width, web.texture.height), new Vector2(0, 0));
        }
    }

    private void PreviewPrevious()
    {
        if(i > 1 && i < dataLength)
            --i;
        else
            i = dataLength - 1;

        UpdateDataOnCanvas();
    }
    
    private void UpdateDataOnCanvas()
    {
        nameText.SetText(data.interest[i].Name);
        displayNameText.SetText(data.interest[i].DisplayName);
        languageText.SetText(data.interest[i].Language);
        interestIDText.SetText(data.interest[i].InterestID.ToString());
        StartCoroutine(SetPic(data.interest[i].PictureUrl));
    }
}
