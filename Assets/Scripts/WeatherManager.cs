using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class WeatherManager : MonoBehaviour {
    void Start() {
        StartCoroutine(ReadWeather());
    }

    IEnumerator ReadWeather() {
        string url = "api.openweathermap.org/data/2.5/weather?id=5946768&APPID=0922c1242b70fddf73ab54572cdb8df4";
        UnityWebRequest www = UnityWebRequest.Get(url);
        
        yield return www.SendWebRequest();

        if (www.isNetworkError) {
            Debug.Log("ERROR: " + www.error);
        } else if (www.downloadHandler == null) {
            Debug.Log("ERROR: downloadHandler is null");
        } else {
            Debug.Log("Loaded following XML " + www.downloadHandler.text);
            JObject obj = JObject.Parse(www.downloadHandler.text);
            Debug.Log("Loaded following Element " + obj["main"]);
            
            // XmlDocument xmlDoc = new XmlDocument();
            // xmlDoc.LoadXml(www.downloadHandler.text);
            // Debug.Log("City: " + xmlDoc.SelectSingleNode("cities/list/item/city/@name").InnerText);
            // Debug.Log("Temperature: " + xmlDoc.SelectSingleNode("cities/list/item/temperature/@value").InnerText);
            // Debug.Log("humidity: " + xmlDoc.SelectSingleNode("cities/list/item/humidity /@value").InnerText);
            // Debug.Log("Cloud : " + xmlDoc.SelectSingleNode("cities/list/item/clouds/@value").InnerText);
            // Debug.Log("Title: " + xmlDoc.SelectSingleNode("cities /list/item/weather/@value").InnerText);
        }
    }
}
