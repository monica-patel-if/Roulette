using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ModalWindow : MonoBehaviour
{

    public Button OnConfirm, OnCancle;
    // Start is called before the first frame update
    void Start()
    {
        OnCancle.onClick.AddListener(() => SetDisable());
        OnConfirm.onClick.AddListener(() => Invoke("SetDisable",4.0f));
    }

    void SetDisable()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
