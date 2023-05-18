using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;

public class SettingScript : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown dropdownLanguage;
    bool active;
    // Start is called before the first frame update
    void Start()
    {
        dropdownLanguage.ClearOptions();
        int id = 0;
        foreach (var item in LocalizationSettings.AvailableLocales.Locales)
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
            data.text = item.LocaleName;
            dropdownLanguage.options.Add(data);
            if(item.Identifier == LocalizationSettings.SelectedLocale.Identifier){
                dropdownLanguage.value = id;
                dropdownLanguage.captionText.text = item.LocaleName.ToString();
            }
            id++;
        }
    }
    public void UpdateLanguage(){
        if(active)
            return;
        
        StartCoroutine(ChangingLang(dropdownLanguage.value));
    }
    IEnumerator ChangingLang(int id){
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
        active = false;
    }
}
