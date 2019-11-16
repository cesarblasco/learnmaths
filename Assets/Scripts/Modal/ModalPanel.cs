using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModalPanel : MonoBehaviour
{
    public Text question;
    public Image iconImage;
    public Button yesButton;
    public Button noButton;
    //public Button cancelButton;
    public GameObject modalPanelObject;

    private static ModalPanel modalPanel;

    public static ModalPanel Instance ()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel)
            {
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene");
            }

        }
        return modalPanel;
    }

    // Yes / no
    public void Choice(string question, UnityAction yesEvent, UnityAction noEvent)
    {
        modalPanelObject.SetActive(true);
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(yesEvent);
        yesButton.onClick.AddListener(ClosePanel);

        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(noEvent);
        noButton.onClick.AddListener(ClosePanel);

        this.question.text = question;

        this.iconImage.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);

    }

    public void ClosePanel()
    {
        modalPanelObject.SetActive(false);
    }


}
