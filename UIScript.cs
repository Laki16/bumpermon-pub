using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
    public Text[] ValueTextArray;

    public void UpdateAll()
    {
        for(int i=0; i<CloudVariables.SystemValues.Length; i++)
        {
            ValueTextArray[i].text = CloudVariables.SystemValues[i].ToString();
        }
    }

    public void Save()
    {
        PlayGamesScript.Instance.SaveData();
    }

    public void Increment(int index)
    {
        CloudVariables.SystemValues[index]++;
        ValueTextArray[index].text = CloudVariables.SystemValues[index].ToString();
    }
}
