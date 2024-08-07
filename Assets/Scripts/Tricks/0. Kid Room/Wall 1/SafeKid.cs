using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SafeKid : NewTrick
{
#region Private Variables
#endregion

#region Public Variables
    public Sprite safeOpen;
    public TMP_Text display;
    public Button[] numberButtons;
    public Button deleteButton;
    public Button openButton;
#endregion

#region Private Methods
#endregion

#region Public Methods
    public void OnClickKeypad(GameObject keypad)
    {
        GameManager.Instance.soundManager.Play("electricButton");
        if (display.text.Length < 4) {
            display.text += keypad.name;
        }
    }
    public void OnClickDelete()
    {
        GameManager.Instance.soundManager.Play("electricButton");

        if (display.text.Length > 0) {
            display.text = display.text.Substring(0, display.text.Length - 1);
        }
    }
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
        display.text = "";
        deleteButton.onClick.AddListener(OnClickDelete);
        openButton.onClick.AddListener(() => IsComplete = CheckComplete(NewInventory.instance.GetClickedItem()));

        foreach (Button button in numberButtons) {
            button.onClick.AddListener(() => OnClickKeypad(button.gameObject));
        }
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (display.text != "0710") {
            GameManager.Instance.soundManager.Play("electricFail");
            display.text = "";
            return false;
        }

        GameManager.Instance.soundManager.Play("electricOKButton");
        NewItem password = NewInventory.instance.GetItem(Define.ItemName.Password);
        if (password != null) {
            NewInventory.instance.RemoveItem(password);
        }
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        gameObject.GetComponent<Image>().sprite = safeOpen;
        transform.SetSiblingIndex(0);
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
    }
#endregion
}