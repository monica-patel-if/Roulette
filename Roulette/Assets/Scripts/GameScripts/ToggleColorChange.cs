using UnityEngine;
using UnityEngine.UI;
public class ToggleColorChange : MonoBehaviour
{

    Toggle thisToggle;
    Text LableTxt;
    Image checkMark;
    // Start is called before the first frame update
    void Awake()
    {
        thisToggle = transform.GetComponent<Toggle>();
       
        LableTxt = transform.GetChild(2).GetComponent<Text>();
        checkMark = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        thisToggle.onValueChanged.AddListener((arg0) => toggleFun());
        //OffLable = transform.GetChild(2).GetComponent<Text>();
    }

    void toggleFun()
    {
      // Debug.Log("this called + "+thisToggle.name);
        if(thisToggle.isOn)
        {
            LableTxt.color = Color.black;
            checkMark.gameObject.SetActive(true);
        }
        else
        {
            LableTxt.color = Color.white;
           //checkMark.gameObject.SetActive(false);
        }
    }

}
