using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Interest
{
    public string Name;
    public string PictureUrl;
    public string DisplayName;
    public string Language;
    public int InterestID;
}

[System.Serializable]
public class InterestList
{
    public Interest[] interest;
}

public class JSonManager : MonoSingletonGeneric<JSonManager>
{
    public string jsonURL;
    private string path;
    public InterestList interestData;

    private void Start()
    {
        path = Application.dataPath;
    }

    public void SaveData()
    {
        StartCoroutine(GetData());
    }

    private IEnumerator GetData()
    {
        WWW web = new WWW(jsonURL);
        yield return web;
        if (web.error == null)
        {
            SaveData(web.text);
        }
        else
        {
            Debug.Log("Error");
        }
    }

    private void SaveData(string jsonData)
    {
        interestData = JsonUtility.FromJson<InterestList>("{\"interest\":" + jsonData + "}");
        string dataToSave = JsonUtility.ToJson(interestData);
        File.WriteAllText(path + "/JSONs/Interests.json", dataToSave);
    }
}
