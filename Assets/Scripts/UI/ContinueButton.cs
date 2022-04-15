using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public Button continueButton;
    // Start is called before the first frame update
    private void Start()
    {
        if (SceneSystem.isSavedBefore)
        {
            continueButton.GetComponent<Image>().color = new Color32(201, 255, 206, (byte)255);
            continueButton.enabled = true;
        }
        else
        {
            continueButton.GetComponent<Image>().color = new Color32(201, 255, 206, (byte)130);
            continueButton.enabled = false;
        }
    }

}
