using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SubscriptionPage : MonoBehaviour
{
    public static SubscriptionPage ins;
    public static string SubscriptionStatus;
    public GameObject BrowserWindow;
  //  string PaymentUrl = "http://web.crapsee.com:9001/api/stripe/index?priceId=";
    public Toggle MonthlyBtn, AnnualyBtn;
    public GameObject PaymentGatewayScreen,WebGlPaymentConfirmationScreen,AfterSubscriptionScreen;
    public Button GoForPayBtn,SubscribeNowBtn;
    public string mothlySubscriptionId = "price_1I8h2oGQqboDCdM6PFJDrf4v";
    public string AnnualySubscriptionId = "price_1I8h3MGQqboDCdM6Tb59Bdgh";
    string SelectedSubscriptionId;

    public Toggle MonthlyToggle, AnnualToggle;
    public TMPro.TMP_Text NextPayText;
    public string MonthlyTxt, AnnualTxt;
    string PayUrl = "";
   
    private void Awake()
    {
        ins = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.ExternalCall("gameFullScreen");
        MonthlyBtn.onValueChanged.AddListener((arg) => SetSub());
        AnnualyBtn.onValueChanged.AddListener((arg) => SetSub());
       GoForPayBtn.onClick.AddListener(() => OnButtonClicked());
        SubscribeNowBtn.onClick.AddListener(() => OpenScreen());
        MonthlyToggle.onValueChanged.AddListener((arg0) => SetMembership());
        AnnualToggle.onValueChanged.AddListener((arg0) => SetMembership());
        if(PlayerPrefs.GetString("sub", "monthly") == "monthly")
        {
            MonthlyToggle.isOn = true;
        }
        else
        {
            AnnualToggle.isOn = true;
        }
    }


    void SetSub()
    {
        if(MonthlyBtn.isOn)
        {
            SelectedSubscriptionId = mothlySubscriptionId;
            MonthlyToggle.isOn = true;

        }
        else if(AnnualyBtn.isOn)
        {
            SelectedSubscriptionId = AnnualySubscriptionId;
            AnnualToggle.isOn = true;
        }
    }
    void SetMembership()
    {
        if(MonthlyToggle.isOn)
        {
            NextPayText.text = MonthlyTxt;
            PlayerPrefs.SetString("sub", "monthly");
        }
        else if(AnnualToggle.isOn)
        {
            NextPayText.text = AnnualTxt;
            PlayerPrefs.SetString("sub", "annual");
        }
    }
    public void OpenScreen()
    {
        UIManager.ins.PrivateTableInfoScreen.SetActive(false);
        UIManager.ins.GroupJoinInfoScreen.SetActive(false);
        UIManager.ins.GroupSubscribeInfo.SetActive(false);
        PaymentGatewayScreen.SetActive(true);

    }
    void CallWebGl(string url)
    {
        Application.OpenURL(PayUrl);
      //  Application.ExternalEval("window.open(" + url + ",\"_blank\")");
        //PlayerPrefs.DeleteAll();
        //SceneManager.LoadScene(0);
      //  Invoke("loadScene", 1.0f);
    }
    void loadScene()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    public void OpenSetOnClick()
    {
        Application.OpenURL(PayUrl);
    }
    public void OnButtonClicked()
    {
       //if (SubscriptionPage.SubscriptionStatus == "PAID")
      //  { Debug.Log("Already subscribed .."); return; }

        string sessionId;
        PayUrl = Links.PaymentUrl + SelectedSubscriptionId + "&customerId=" + PlayerPrefs.GetString("stripe_customer_id");

#if UNITY_WEBGL
        WebGlPaymentConfirmationScreen.SetActive(true);
        ModalWindow mm = WebGlPaymentConfirmationScreen.GetComponent<ModalWindow>();
        mm.OnConfirm.onClick.RemoveAllListeners();
        mm.OnConfirm.onClick.AddListener(()=> OpenSetOnClick());
        
#elif !UNITY_WEBGL
        BrowserWindow.SetActive(true);
        UniWebView uniWeb = BrowserWindow.GetComponent<UniWebView>();
        uniWeb.urlOnStart = url;
        uniWeb.Load(url);
        uniWeb.Reload();
        uniWeb.BackgroundColor = Color.white;
        uniWeb.OnPageFinished += (view, statusCode, url1) => {
           // print(statusCode);
            print("Web view loading finished for: " + url1);
           // print("Web view loading finished for: " + view);
           
            if(url1.Contains("http://dev.crapsee.com:9001/api/stripe/success?session_id="))
            {
                string[] f1 = url1.Split('=');
                sessionId = f1[1];
                PlayerPrefs.SetString("mysessionId", sessionId);
                Debug.Log(sessionId);
              
                ((UniWebView)view).Reload();
                ((UniWebView)view).Stop();
                ((UniWebView)view).gameObject.SetActive(false);
                ((UniWebView)view).GetHTMLContent((string obj) => Debug.Log(obj));
                SubscriptionPage.SubscriptionStatus = "PAID";

                UserProfileDetails.ins.getUserProfile();
                AfterSubscriptionScreen.SetActive(true);
           //     AfterSubscriptionScreen.GetComponent<Michsky.UI.ModernUIPack.ModalWindowManager>().OpenWindow();
               // AfterSubscriptionScreen.GetComponent<Michsky.UI.ModernUIPack.ModalWindowManager>().windowDescription.text = " ";
          //      AfterSubscriptionScreen.GetComponent<Michsky.UI.ModernUIPack.ModalWindowManager>().onConfirm.AddListener(UserProfileDetails.ins.GotoSubscripitonPage);

            }
            else if(url1.Contains("http://dev.crapsee.com:9001/api/stripe/error"))
            {
                ((UniWebView)view).Stop();
                ((UniWebView)view).Reload();
                ((UniWebView)view).gameObject.SetActive(false);
                ((UniWebView)view).GetHTMLContent((string obj) => Debug.Log(obj));
               // AfterSubscriptionScreen.SetActive(true);
               //AfterSubscriptionScreen.GetComponent<Michsky.UI.ModernUIPack.ModalWindowManager>().OpenWindow();
               // AfterSubscriptionScreen.GetComponent<Michsky.UI.ModernUIPack.ModalWindowManager>().windowDescription.text = " Please Try later , Sorry for Inconnvenience.";

            }
        };

#endif

        //  GetBrowserOpen(url);


        // uniWeb.OnPageProgressChanged += (UniWebView webView, float progress) => Debug.Log(progress);
    }

}
