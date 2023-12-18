using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    public Vector3 pos;
    public SomeData data;

    public void SavePos()
    {
        data = new SomeData(new Vector3(12, 42, 42), new float[3] { 21f, 1f, 45f }, "Bobik");

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("JSON", json);
    }

    public void LoadPosition()
    {
        data = JsonUtility.FromJson<SomeData>(PlayerPrefs.GetString("JSON"));

        PlayerPrefs.DeleteAll();
    }
}

[Serializable]
    public class SomeData
    {
        public Vector3 Pos;
        public float[] SomeArray;
        public string SomeName;

        public SomeData() { }
        
        public SomeData(Vector3 pos, float[] someArray,string someName)
        {
            Pos = pos;
            SomeArray = someArray;
            SomeName = someName;
        }
    }

