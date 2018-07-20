using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AccessoryUI : MonoBehaviour {
    public GameObject enhancePanel;
    public GameObject detailPanel;
    Accessory myInfo;

    private void Start()
    {
        ApplyEnhance();
    }

    private void ApplyEnhance()
    {
        enhancePanel.GetComponent<TextMeshProUGUI>().text = "" + myInfo.enhance;
    }
    private void ItemOnClick()
    {
        detailPanel.SetActive(true);
    }
}
