using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class spriteEdit : MonoBehaviour
{
    public static spriteEdit ins;

    private void Awake()
    {
        ins = this;
    }
    public Sprite[] ChipsSprite;
   
}
