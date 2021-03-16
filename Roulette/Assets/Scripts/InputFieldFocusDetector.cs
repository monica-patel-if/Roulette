using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputFieldFocusDetector : MonoBehaviour, ISelectHandler
{
    public InputNavigator InputNavigator;

    /// <summary>
    /// The index of this InputField according to its order in the Selectables List in its <see cref="InputNavigator"/> instance
    /// </summary>
    public int InputIndex;

    public void OnSelect(BaseEventData data)
    {
        InputNavigator.CurrentSelectableIndex = InputIndex;
    }
}
