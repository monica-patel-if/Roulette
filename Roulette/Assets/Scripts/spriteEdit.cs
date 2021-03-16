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
    public Sprite bar_12,bar_2;
    public Sprite UpArrow;
    public Sprite DownArrow;
    public Sprite PuckOn;
    public Sprite PuckOff;
    public Sprite RedDot;
    public Sprite GreenDot;
    public Sprite[] ChipsSprite;
    public Sprite[] White_Dices;
    public Sprite[] Red_Dices;

    
    public List<Sprite> TowbBetsFilledImgs = new List<Sprite>();
    public List<Sprite> TownbetsSimpleImgs = new List<Sprite>();
    public Color greenColor;
    public Color redColor;
    public Color whiteColor;
    public Color PointColor;

    [Header("-- Crapless Images --")]
    public Sprite Crapless0;
     public Sprite Crapless1;
    
    [Header("-- compact Images --")]
    public Sprite FullViewSprite;
    public Sprite CompactViewSprite;
}
