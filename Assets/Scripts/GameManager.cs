using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Converters;
using UnityEngine.Networking;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject panel, panelText;
    [SerializeField] private string path;
    [SerializeField] public TMP_Text outputText;
    [SerializeField] private int x, y, width, height;

    public void Start()
    {
        outputText.text = SceneChanger.userInput;
        Load();
        StartCoroutine(DownloadedImage(path));
        panelText.GetComponent<RectTransform>().localPosition = new Vector3((float)x, (float)y);
        panelText.GetComponent<RectTransform>().sizeDelta = new Vector3((float)width, (float)height);
        
    }


    public bool Load()
    {
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            string playerData = File.ReadAllText(Application.dataPath + "/saveFile.json");

            Player mPlayer = JsonConvert.DeserializeObject<Player>(playerData);
            Debug.Log($"Layers: { playerData}");
            var converter = new ExpandoObjectConverter();
            JObject data = JObject.Parse(File.ReadAllText(Application.dataPath + "/saveFile.json"));
            JArray layers = (JArray)data.SelectToken("layers");
            foreach(JToken layer in layers)
            {
                Debug.Log(layer["type"].ToString());
                Debug.Log(layer["path"].ToString());
                path = (string)layer["path"];
                JArray placements = (JArray)layer.SelectToken("placement");
                foreach (JToken placement in placements)
                {
                    Debug.Log(placement["position"].ToString());
                    JObject position = JObject.Parse(placement["position"].ToString());
                    Debug.Log((int)position["x"]);
                    x = (int)position["x"];
                    Debug.Log((int)position["y"]);
                    y = (int)position["y"];
                    Debug.Log((int)position["width"]);
                    width = (int)position["width"];
                    Debug.Log((int)position["height"]);
                    height = (int)position["height"];
                }
            }
            //Debug.Log(data.SelectToken("layers").ToString());
            

        }
        return true;
    }

    IEnumerator DownloadedImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if(request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Sprite sprite = Sprite.Create(((DownloadHandlerTexture)request.downloadHandler).texture, new Rect(x, y, width, height), new Vector2(width/2, height/2));
            panel.GetComponent<Image>().overrideSprite = sprite;
        }
    }

}