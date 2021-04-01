using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class ObjectDetails : MonoBehaviour
{
    [Header("For Current Player")]
    public GameObject ParentObj;
    public int myChipValue;
    public string _chipColorPty;
    Button thisOnly;
    void Start()
    {
        thisOnly = GetComponent<Button>();
        thisOnly.onClick.RemoveAllListeners();
        myChipValue = 0;
    }

}