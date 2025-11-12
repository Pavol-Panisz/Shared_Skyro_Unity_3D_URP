using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sklad : MonoBehaviour
{
    public List<string> userInputList = new List<string>();
    string userInputText;
    bool isItemIncluded;

    [SerializeField] TMP_InputField IF;
    

    public void ClickedAdd()
    {
        if (userInputList.Contains(userInputText) == false)
        {
            userInputList.Add(userInputText);
        }
    }

    public void ClickedRemove()
    {
        if(userInputList.Contains(userInputText) == true)
        {
            int whatToRemove = userInputList.IndexOf(userInputText);
            userInputList.RemoveAt(whatToRemove);
        }
    }
    
    public void EditedText()
    {
        Debug.Log("Preslo to sem");
        userInputText = IF.text.ToString();
        Debug.Log(userInputText);
        //if (userInputList.Contains(userInputText) == false)
        //{
            //isItemIncluded = false;
        //}
        //else
        //{
            //isItemIncluded = true;
        //}
        
    }
}
