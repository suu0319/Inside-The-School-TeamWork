using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordPuzzle : MonoBehaviour
{
    public InputField password;
    public GameObject ui,error,successful;

    // Update is called once per frame
    void Update()
    {
        OriginMode.God = true;

        if(ui.activeInHierarchy == false)
        {
            ui.SetActive(true);
        }
        
        if(password.IsActive())
        {
            password.ActivateInputField();
        }

        GameObject.Find("EventSystem").GetComponent<DeviceSelect>().enabled = false;
        
        if(Input.GetKeyDown(KeyCode.Return) && PuzzleObjController.PasswordCorrect == false)
        {
            if(password.text == "89221")
            {
                successful.SetActive(true);
                GameObject.Find("EventSystem").GetComponent<DeviceSelect>().enabled = true;
                PuzzleObjController.PasswordCorrect = true;
            }
            else
            {
                error.SetActive(true);
                StartCoroutine(textCD());
            }
        }
        else if(PuzzleObjController.PasswordCorrect == true)
        {
            successful.SetActive(true);
        }
    }

    //文字動畫協程
    IEnumerator textCD()
    {
        yield return new WaitForSeconds(2.5f);
        error.SetActive(false);
    }
}
