using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
using System.Net.Mail;
using System;
//using Michsky.UI.ModernUIPack;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Loginmanager : MonoBehaviour
{
    //subscriptions@5000swimworkouts.com
    public const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
    public GameObject Loginscreen,
                    RegisterScreen,
                    OtpScreen,
                    forgotPasswordScreen,
                    ResetpasswordScreen,
                    MainHomeScreen,
        UserProfileScreen,
    LoadingScreen, TermsScreen;
    [Header("-- Main Game Buttons --")]
    public Button PlayOffline;
    public Button GotoRegister, GotoLogin;

    [Header("-- For Register --")]
    public TMP_InputField FirstName;
    public TMP_InputField LastName,ScreennameField,EmailId,PasswordField,ConfirmPasswordField;
    public Button RegisterSubmit,CancleRegisterBtn,
        BacktoLoginBtn;
    public Text ErrorMsg;
    public Toggle TermsToggle;
    public Button termsLoginBtn;

    [Header("-- For Login --")]
    public TMP_InputField EmailField;
     public TMP_InputField LoginPasswordField;
    public Button LoginSubmitBtn,CancelLoginBtn,
        ForgotPasswordBtn,
        RegisterBtn,
        GoogleLoginBtn,
        FbLoginBtn;
    public Button showpasswordbutton, hidepasswordbutton;

    [Header("-- for OTP --")]
    public TMP_InputField OTPfield;
    public Button OtpSubmitBtn,ResendCodeBtn;

    [Header("-- For ForgotPassword --")]
    public TMP_InputField ForgotEmailField;
    public Button SubmitEmailBtn, backloginBtn;

    [Header(" -- For reset password--")]
    public TMP_InputField OtpCodeField;
     public TMP_InputField NewPassfield,NewPassConfirmFIeld;
    public Button SubmitPassBtn,BackOnLoginBtn;

    public static Loginmanager ins;

   // public NotificationManager // NotificationPopUp;
  

    private void Awake()
    {
        ins = this;
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("fromMenu", 0);
        PlayerPrefs.SetInt("CurrentUpdateId",0);
    }
    private void Start()
    {

        // main page stuff
          PlayOffline.onClick.AddListener(() => PlayOfflineMode());
          GotoRegister.onClick.AddListener(() => GotoRegisterMode());
          GotoLogin.onClick.AddListener(() => GotoLoginMode());
          
          LoginSubmitBtn.onClick.AddListener(() => LoginToGame());
          ForgotPasswordBtn.onClick.AddListener(() => OpenForgotPasswordScreen());
          CancelLoginBtn.onClick.AddListener(() => CancleBtnClick());
          RegisterBtn.onClick.AddListener(() => GotoRegisterMode());
          showpasswordbutton.onClick.AddListener(ShowPasswordOperation);
          hidepasswordbutton.onClick.AddListener(ShowPasswordOperation);

          // register stuff
          RegisterSubmit.onClick.AddListener(() => SignUpcall());
          CancleRegisterBtn.onClick.AddListener(() => CancleBtnClick());
          BacktoLoginBtn.onClick.AddListener(() => GotoLoginMode());
          termsLoginBtn.onClick.AddListener(() => GetTermsConditonsForCrapsee());
         
         //Otp Submit stuff
          OtpSubmitBtn.onClick.AddListener(() => OtpCall());
          ResendCodeBtn.onClick.AddListener(() => ResendOtp());

          //ForgotPassword Stuff
          SubmitEmailBtn.onClick.AddListener(() => CallForgetPassword());
          backloginBtn.onClick.AddListener(() => GotoLoginMode());

          //reset password
          SubmitPassBtn.onClick.AddListener(() => submitResetPassword());
          BackOnLoginBtn.onClick.AddListener(() => GotoLoginMode());

        int IsFirst = PlayerPrefs.GetInt("IsFirst",0);
        if (IsFirst == 0)
        {
            //Do stuff on the first time
            Debug.Log("first run");
            PlayerPrefs.DeleteAll();
            Caching.ClearCache();
           
            PlayerPrefs.SetInt("IsFirst", 1);
        }
        else
        {
            Debug.Log("welcome again!");
            Caching.ClearCache();
            Caching.ClearCache();
        }

        UIManager.ins.PlayerPrivateTableList.Clear();
        for (int i=0;i<UIManager.ins.OfflineTableData.Count;i++)
        {
           //Debug.Log(UIManager.ins.OfflineTableData[i].type);
            UIManager.ins.PlayerPrivateTableList.Add(UIManager.ins.OfflineTableData[i]);
        }

        if (PlayerPrefs.GetInt("fromMenu", 0) == 0)
        {
            if (PlayerPrefs.GetInt("IsloggedInBefore",0) == 1)
            {
                EmailField.text = PlayerPrefs.GetString("Email");
                LoginPasswordField.text = PlayerPrefs.GetString("password");
                //Loading.SetActive(true);
                StartCoroutine(GetMyData()); Apifunctions.ins.GetVIdeos();
                //StartCoroutine(GetTournamentDetail());
                UserProfileScreen.SetActive(true);
                UIManager.ins.LoginScreen.transform.localScale = new Vector3(0, 1, 1);
             //   Debug.Log("called table 11 ...");
            }
            else
            {
                UserProfileScreen.SetActive(false);
                UIManager.ins.LoginScreen.transform.localScale = new Vector3(1, 1, 1);
                //Debug.Log("called table 22 ...");
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("IsloggedInBefore",0) == 1)
            {
                //Debug.Log("called table again called 44...");
                StartCoroutine(GetTableDetails());
                Apifunctions.ins.GetVIdeos();
                Apifunctions.ins.FriendList();
                UserProfileDetails.ins.getUserProfile();
             
            }
            else
            {
                Debug.Log("called table 33 ...");
                UserProfileScreen.SetActive(false);
                UIManager.ins.LoginScreen.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    int AllowEnter = 0;
    bool isEnter;
    private void Update()
    {
        /*if(Input.GetKeyUp(KeyCode.KeypadEnter) && AllowEnter == 1)
        {
            SignUpcall();
        }
        else if (Input.GetKeyUp(KeyCode.KeypadEnter) && AllowEnter == 2)
        {
            LoginToGame(); 
        }*/
         if(Input.GetKeyUp(KeyCode.KeypadEnter))
             {
            Debug.Log(AllowEnter);
             switch(AllowEnter)
             {
                 case 1:
                     SignUpcall();
                     break;
                 case 2:
                     LoginToGame();
                     break;
                 case 3:
                     CallForgetPassword();
                     break;
                 case 4:
                     OtpCall();
                     break;
                 case 5:
                     submitResetPassword();
                     break;
                 case 6:
                     BacktoHomeScreen();
                     break;
             }
         }
    }

    void PlayOfflineMode()
    {
        UIManager.ins.PlayOffline();
    }

    void CancleBtnClick()
    {
        AllowEnter = 0;
        PlayOffline.gameObject.SetActive(true);
        GotoLogin.gameObject.SetActive(true);
        GotoRegister.gameObject.SetActive(true);

        Loginscreen.SetActive(false);
        RegisterScreen.SetActive(false);
        OtpScreen.SetActive(false);
        ResetpasswordScreen.SetActive(false);
        forgotPasswordScreen.SetActive(false);

        ErrorMsg.text = "";
        ErrorMsg.transform.parent.gameObject.SetActive(false);

    }
    void GotoRegisterMode()
    {
        AllowEnter = 1;

        PlayOffline.gameObject.SetActive(false);
        GotoLogin.gameObject.SetActive(false);
        GotoRegister.gameObject.SetActive(false);

        TermsToggle.isOn = false;
        FirstName.text = "";
        LastName.text = "";
        ScreennameField.text = "";
        EmailId.text = "";
        PasswordField.text = "";
        ConfirmPasswordField.text = "";
        RegisterScreen.SetActive(true);
        Loginscreen.SetActive(false);
        OtpScreen.SetActive(false);
        ResetpasswordScreen.SetActive(false);
        forgotPasswordScreen.SetActive(false);
    }

    void GotoLoginMode()
    {
        AllowEnter = 2;
        EmailField.text = "";
        LoginPasswordField.text = "";
        PlayOffline.gameObject.SetActive(false);
        GotoLogin.gameObject.SetActive(false);
        GotoRegister.gameObject.SetActive(false);
        Loginscreen.SetActive(true);
        RegisterScreen.SetActive(false);
        OtpScreen.SetActive(false);
        ResetpasswordScreen.SetActive(false);
        forgotPasswordScreen.SetActive(false);
    }

    void OpenForgotPasswordScreen()
    {
        AllowEnter = 3;
        ForgotEmailField.text = "";
        Loginscreen.SetActive(false);
        RegisterScreen.SetActive(false);
        OtpScreen.SetActive(false);
        ResetpasswordScreen.SetActive(false);
        forgotPasswordScreen.SetActive(true);
    }

    void GetTermsConditonsForCrapsee()
    {
        Application.OpenURL(Links.SiteURL);
        /*
#if UNITY_WEBGL
        Application.ExternalEval("window.open( "+Links.termsUrl + ",\"_blank\")");
#elif !UNITY_2019
         TermsScreen.SetActive(true);
#endif*/

        // Application.OpenURL("http://dev.crapsee.com/panel/terms");
    }
    // for Register API call
    public void SignUpcall()
    {
        if ((FirstName.text != string.Empty &&
           LastName.text != string.Empty &&
           ScreennameField.text != string.Empty &&
           EmailId.text != string.Empty &&
           PasswordField.text != string.Empty &&
           ConfirmPasswordField.text != string.Empty))
        {
            if (TermsToggle.isOn) 
            {
            if (IsValid(EmailId.text))
            {
                if (PasswordField.text.Length > 5)
                {
                    if (PasswordField.text == ConfirmPasswordField.text)
                    {
                        LoadingScreen.SetActive(true);
                        StartCoroutine(SignUpUser());
                        Debug.Log("called");
                    }
                    else
                    {
                        ErrorMsg.text = "Please make sure the passwords are match";
                        ErrorMsg.transform.parent.gameObject.SetActive(true);
                            LoadingScreen.SetActive(false);
                        }
                }
                else
                {
                         ErrorMsg.text = "Enter a Password with at least 6 characters.";
                        ErrorMsg.transform.parent.gameObject.SetActive(true);
                       
                    }
            }
            else
            {
                     ErrorMsg.text = "Please enter a valid email address";
                    ErrorMsg.transform.parent.gameObject.SetActive(true);

                } 
        }
        else
        {
                 ErrorMsg.text = "Please agree to the Terms & Conditions of Crapsee";
                ErrorMsg.transform.parent.gameObject.SetActive(true);
         
            }
           
        }
        else
        {
            ErrorMsg.text = "Please Fill all fields!!";
             ErrorMsg.transform.parent.gameObject.SetActive(true);
           
        }
    }

    IEnumerator SignUpUser()
    {
        yield return true;
        WWWForm regform = new WWWForm();
        regform.AddField(WebServicesKeys.fnamekey, FirstName.text);
        regform.AddField(WebServicesKeys.lnamekey, LastName.text);
        regform.AddField(WebServicesKeys.emailkey, EmailId.text);
        regform.AddField(WebServicesKeys.username, ScreennameField.text);
        regform.AddField(WebServicesKeys.password, PasswordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post(Links.UserRegisterUrl, regform))
        {
            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                Debug.Log(www.downloadProgress);
            }
            if (www.isNetworkError)
            {
                Debug.Log("inn  Network Error");
                ErrorMsg.text = www.error;
                ErrorMsg.transform.parent.gameObject.SetActive(true);
                LoadingScreen.SetActive(false);
            }
            else if (www.isDone)
            {
                LoadingScreen.SetActive(false);
                Debug.Log(www.downloadHandler.text);
                JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                string msg = dd["messageCode"].Value; //SIGNUP_SUCCESS
                string statusCode = dd["statusCode"].Value; //200
                if(msg == "SIGNUP_SUCCESS" && statusCode == "200")
                {
                    RegisterScreen.SetActive(false);
                    OTPfield.text = "";
                    OtpScreen.SetActive(true);
                    AllowEnter = 4;
                }
                else if(msg == "USERNAME_EXISTS")
                {
                 
                    ErrorMsg.text = "That screen name is already taken. Please try again.";
                    ErrorMsg.transform.parent.gameObject.SetActive(true);
                }
                else if (msg == "USEREMAIL_EXISTS")
                {
                   
                    ErrorMsg.text = "An account with that email address is already taken. Please try again.";
                    ErrorMsg.transform.parent.gameObject.SetActive(true);
                }
                else if(statusCode == "400")
                {

                    ErrorMsg.text = "Use valid data, try later";
                    ErrorMsg.transform.parent.gameObject.SetActive(true);
                }
                else if (statusCode == "500")
                {

                    ErrorMsg.text = "Internal Server Error,try Later";
                    ErrorMsg.transform.parent.gameObject.SetActive(true);
                }
                // Debug.Log("inn  Network Error");

            }
        }
    }

  
    void OtpCall()
    {
        if (OTPfield.text != string.Empty)
            {

            LoadingScreen.SetActive(true);
            StartCoroutine(varifyOTP());
            }
        else
        {
           ErrorMsg.text = "Please fill in all fields";
             ErrorMsg.transform.parent.gameObject.SetActive(true);


        }
    }

    IEnumerator varifyOTP()
    {
        string url = Links.veryfiOTPUrl + OTPfield.text;
        Debug.Log(url);
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                Debug.Log(www.downloadProgress);
            }
            if (www.isNetworkError)
            {
                Debug.Log("inn  Network Error");
                ErrorMsg.text = www.error;
                ErrorMsg.transform.parent.gameObject.SetActive(true);
                LoadingScreen.SetActive(false);
            }
            else if (www.isDone)
            {
                LoadingScreen.SetActive(false);

                Debug.Log(www.downloadHandler.text);
                JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                string msg = dd["messageCode"].Value; //ACTIVATION_SUCCESS
                string statusCode = dd["statusCode"].Value; //200
                string message = dd["message"].Value; //200
                if (msg == "ACTIVATION_SUCCESS" && statusCode == "200")
                {

                       
                    ErrorMsg.text = "Your account is activated.Please login now.";
                    ErrorMsg.transform.parent.gameObject.SetActive(true);
                    GotoLoginMode();
                }
                else if(statusCode != "200")
                {
                    OTPfield.text = "";
                  
                    ErrorMsg.text = "Please check the code and try again";
                    ErrorMsg.transform.parent.gameObject.SetActive(true);
                }


            }
        }
    }
    void ResendOtp()
    {
        StartCoroutine(resendOtpCode());
    }
    IEnumerator resendOtpCode()
    {
        WWWForm reqform = new WWWForm();
        reqform.AddField(WebServicesKeys.username, EmailId.text);
        using (UnityWebRequest www = UnityWebRequest.Post(Links.ResendCode, reqform))
        {
            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                Debug.Log(www.downloadProgress);
            }
            if (www.isNetworkError)
            {
                Debug.Log("inn  Network Error");
                ErrorMsg.text = www.error;
                ErrorMsg.transform.parent.gameObject.SetActive(true);
                LoadingScreen.SetActive(false);
            }
           else if (www.isDone && www.error == null)
            {
                JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                string msg = dd["messageCode"].Value; //CHECK_EMAIl
                string statusCode = dd["statusCode"].Value; //200
                string message = dd["message"].Value; //200
                LoadingScreen.SetActive(false);
                if (statusCode == "200" && msg == "CHECK_EMAIl")
                {
                    OTPfield.text = "";


                    ErrorMsg.text = "Check your Mail, for New OTP.";
                    ErrorMsg.transform.parent.gameObject.SetActive(true);
                }
                else
                {
                    ErrorMsg.text = message;
                    ErrorMsg.transform.parent.gameObject.SetActive(true);
                }
            }
        }
    }

    void CallForgetPassword()
    {
        if (ForgotEmailField.text != string.Empty)
        {
            AllowEnter = 3;
            LoadingScreen.SetActive(true);
            StartCoroutine(ForgotPassword());
        }
        else
        {
              ErrorMsg.text = "Please fill in all fields";
               ErrorMsg.transform.parent.gameObject.SetActive(true);


        }
    }

    IEnumerator ForgotPassword()
    {
        WWWForm reqform = new WWWForm();
        reqform.AddField(WebServicesKeys.username, ForgotEmailField.text);
        using (UnityWebRequest www = UnityWebRequest.Post(Links.ForgotPassUrl ,reqform))
        {
            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                Debug.Log(www.downloadProgress);
            }
            if (www.isNetworkError)
            {
                Debug.Log("inn  Network Error");
                ErrorMsg.text = www.error;
                ErrorMsg.transform.parent.gameObject.SetActive(true);
                LoadingScreen.SetActive(false);

            }
           else if (www.isDone )
            {
                JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                string msg = dd["messageCode"].Value; //CHECK_EMAIl
                string statusCode = dd["statusCode"].Value; //200
                string message = dd["message"].Value; //200
                LoadingScreen.SetActive(false);
                if (statusCode == "200" && msg == "CHECK_EMAIl")
                {
                 
                    OtpCodeField.text = "";
                    NewPassfield.text = "";
                    NewPassConfirmFIeld.text = "";
                    forgotPasswordScreen.SetActive(false);
                    ResetpasswordScreen.SetActive(true);
                    AllowEnter = 5;
                }
                else
                {
                 
                    ErrorMsg.text = "We do not have that email on file. Please try again.";
                    ErrorMsg.transform.parent.gameObject.SetActive(true);
                }
            }
        }
    }

    void submitResetPassword()
    {
        if (OtpCodeField.text != string.Empty && NewPassfield.text != string.Empty && NewPassConfirmFIeld.text != string.Empty)
        {
            if (NewPassfield.text.Length > 5)
            {
                if (NewPassfield.text == NewPassConfirmFIeld.text)
                {
                    LoadingScreen.SetActive(true);
                    StartCoroutine(ResetPassword());
                    Debug.Log("called");
                }
                else
                {
                    ErrorMsg.text = "Please enter Both password Same!";
                     ErrorMsg.transform.parent.gameObject.SetActive(true);
                   
                }
            }
            else
            {

                ErrorMsg.text = "Enter a Password with at least 6 characters.";
                ErrorMsg.transform.parent.gameObject.SetActive(true);
            }
           
        }
        else
        {

           
            ErrorMsg.text = "Please fill in all fields.";
            ErrorMsg.transform.parent.gameObject.SetActive(true);

        }
    }
    IEnumerator ResetPassword()
    {
        WWWForm reqform = new WWWForm();
        reqform.AddField(WebServicesKeys.password, NewPassfield.text);
        Debug.Log(Links.ResetPassUrl + OtpCodeField.text);
        using (UnityWebRequest www = UnityWebRequest.Post(Links.ResetPassUrl+ OtpCodeField.text, reqform))
        {
            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                Debug.Log(www.downloadProgress);
            }
            if (www.isNetworkError)
            {
                Debug.Log("inn  Network Error");
                ErrorMsg.text = www.error;
                ErrorMsg.transform.parent.gameObject.SetActive(true);
                LoadingScreen.SetActive(false); 
                // isPlayerLogin = false;
            }
           else if (www.isDone )
            {
                JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                string msg = dd["messageCode"].Value; //CHECK_EMAIl
                string statusCode = dd["statusCode"].Value; //200
                string message = dd["message"].Value; //200
                LoadingScreen.SetActive(false);
                if (statusCode == "200")
                {
                    ErrorMsg.text = "Password Reset SuccessFully,You can now login";
                      ErrorMsg.transform.parent.gameObject.SetActive(true);
                  
                    GotoLoginMode();
                }
                else
                {
                    OtpCodeField.text = "";
                    LoadingScreen.SetActive(false);
                    ErrorMsg.text = "Please check the code and try again";
                    ErrorMsg.transform.parent.gameObject.SetActive(true);
                }
            }
        }
    }

    public void LogoutData()
    {
        UIManager.ins.LogoutScreen.SetActive(true);
        UIManager.ins.LogoutScreen.GetComponent<ModalWindow>().OnConfirm.onClick.RemoveAllListeners();
       UIManager.ins.LogoutScreen.GetComponent<ModalWindow>().OnConfirm.onClick.AddListener(LoggedConfirm);
    }

    void LoggedConfirm()
    {
        int a = PlayerPrefs.GetInt("imgCount", 0);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("imgCount", a);
        SceneManager.LoadScene(0);
    }

    public void LoginToGame()
    {
        LoadingScreen.SetActive(true);
        StartCoroutine(GetMyData());
    }

    private IEnumerator GetMyData()
    {
        //Debug.Log(SystemInfo.operatingSystem + "..." + SystemInfo.deviceName);
        if (EmailField.text != string.Empty && LoginPasswordField.text != string.Empty)
        {
            if (IsValid(EmailField.text))
            {
                WWWForm form = new WWWForm();
                form.AddField(WebServicesKeys.username, EmailField.text);
                form.AddField(WebServicesKeys.password, LoginPasswordField.text);
                form.AddField(WebServicesKeys.os, SystemInfo.operatingSystem);
                form.AddField(WebServicesKeys.browser, SystemInfo.deviceName);
                using (UnityWebRequest www = UnityWebRequest.Post(Links.UserLoginUrl, form))
                {
                   // Debug.Log("3333");
                    yield return www.SendWebRequest();
                    while (!www.isDone)
                    {
                        Debug.Log(www.downloadProgress);
                    }
                    if (www.isNetworkError)
                    {
                        Debug.Log("inn  Network Error");
                        ErrorMsg.text = www.error;
                        ErrorMsg.transform.parent.gameObject.SetActive(true);
                        LoadingScreen.SetActive(false);
                    }
                   else if (www.isDone)
                    {
                       // Debug.Log("444");
                        JSONNode dObj = JSONNode.Parse(www.downloadHandler.text);
                        string msg = dObj["messageCode"].Value;
                        string statusCode = dObj["statusCode"].Value;
                        string message = dObj["message"].Value; //200
                        LoadingScreen.SetActive(false);
                        //Debug.Log(msg + " ...." + statusCode + " ...." + message);
                        if (msg == "LOGIN_SUCCESS" && statusCode == "200")
                        {
                          //  ErrorMsg.text = "Login success";
                          //   ErrorMsg.transform.parent.gameObject.SetActive(true);
                            JSONNode data = dObj["data"];

                            UIManager.ins.Username.text = data["username"].Value;
                            PlayerPrefs.SetString("Email", EmailField.text);
                            PlayerPrefs.SetString("username", data["username"].Value);
                            PlayerPrefs.SetString("password", LoginPasswordField.text);
                            PlayerPrefs.SetString("token", data["token"].Value);
                            PlayerPrefs.SetString("UserID", data["id"].Value);
                            PlayerPrefs.SetString("stripe_customer_id", data["stripe_customer_id"].Value);
                            PlayerPrefs.SetString("joined_password", data["joined_password"].Value);
                            StartCoroutine(GetTableDetails());
                            UserProfileScreen.SetActive(true);
                            PlayerPrefs.SetInt("IsloggedInBefore", 1);
                            PlayerPrefs.DeleteKey("puckvalue");
                            UserProfileDetails.ins.getUserProfile();
                            Apifunctions.ins.FriendList();
                            int count = 0;
                            if (data["chip_color"].Value == "red")
                            {  count = 0; }
                            else
                            {
                                count = int.Parse(data["chip_color"].Value);
                            }
                           

                            PlayerPrefs.SetInt("imgCount", count);
                            UserProfileDetails.ins.MainChipselectionImg.sprite = UserProfileDetails.ins.ChipsColor.sprite = UserProfileDetails.ins.ChipImg.sprite = UserProfileDetails.ins.ChipsSprite[count];

                            SubscriptionPage.SubscriptionStatus = data["subscription_active_status"].Value;
                            Debug.Log("in SubscriptionPage.SubscriptionStatus value is : "+ SubscriptionPage.SubscriptionStatus);

                            UIManager.ins.LoginScreen.transform.localScale = new Vector3(0, 1, 1);
                        }
                        else if (statusCode == "404")
                        {

                            ErrorMsg.text = "That email and password combination do not match. Try again";
                            ErrorMsg.transform.parent.gameObject.SetActive(true);
                            Debug.Log("in Login "); 
                            UIManager.ins.LoginScreen.transform.localScale = new Vector3(1, 1, 1);
                        }
                        Debug.Log(statusCode +" in Login :" + message);
                    }
                }
            }
            else
            {

               ErrorMsg.text = "Please Enter Valid Email ID";
                ErrorMsg.transform.parent.gameObject.SetActive(true);
                LoadingScreen.SetActive(false);
                Debug.Log("in Login ");
            }
        }
        else
        {
            ErrorMsg.text = "Please fill all details";
             ErrorMsg.transform.parent.gameObject.SetActive(true);
            LoadingScreen.SetActive(false);
            Debug.Log("in Login ");
        }
    }

    public void GetTablesList()
    {
        PlayerPrefs.SetInt("CurrentUpdateId", 0);
        UIManager.ins.PlayerPrivateTableList.Clear();
        for (int i = 0; i < 3; i++)
        {
            UIManager.ins.PlayerPrivateTableList.Add(UIManager.ins.OfflineTableData[i]);
        }

        StartCoroutine(GetTableDetails());
    }
    IEnumerator GetTableDetails()
    {
       
        SoundManager.instance.ChipsSource.volume = 1;
        using (UnityWebRequest www = UnityWebRequest.Get(Links.GetUsersTableList))
        {
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.Send();
            if (www.isNetworkError)
            {
                ErrorMsg.text = www.error;
                ErrorMsg.transform.parent.gameObject.SetActive(true);
                LoadingScreen.SetActive(false);
            }
            else
            {
                JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                JSONNode data = dd["data"]["output"];
                string successCode = dd["messageCode"];
                //Debug.Log(data.Count);
                for (int i = 0; i < data.Count; i++)
                {
                    TableData t1 = new TableData();
                    JSONNode Tabledata = data[i];
                    t1.type = Tabledata["type"];
                    if(Tabledata["leave"])
                    {
                        continue;
                    }
                    t1.layout = Tabledata["layout"];
                    t1.payout_schedule = Tabledata["payout_schedule"];
                    t1.status = Tabledata["status"];
                    t1.payout_metric = Tabledata["payout_metric"];
                    t1.hop_bet_easy_way = Tabledata["hop_bet_easy_way"];
                    t1.hop_bet_hard_way = Tabledata["hop_bet_hard_way"];
                    t1.field_12_pays = Tabledata["field_12_pays"];
                    t1.field_2_pays = Tabledata["field_2_pays"];
                    t1.allow_buy_5_by_9 = Tabledata["allow_buy_5_by_9"];
                    t1.dont_pass = Tabledata["dont_pass"];
                    t1.pay_vig_on_buys = Tabledata["pay_vig_on_buys"];
                    t1.pay_vig_on_lays = Tabledata["pay_vig_on_lays"];
                    t1.bonus_craps = Tabledata["bonus_craps"];
                    t1.bonus_payout = Tabledata["bonus_payout"];
                    t1.allow_player_compare = Tabledata["allow_player_compare"];
                    t1.roll_options = Tabledata["roll_options"];
                    t1.auto_roll_pause = Tabledata["auto_roll_pause"];
                    t1.is_delete = Tabledata["is_delete"];
                    t1.AdminTableId = t1.id = Tabledata["_id"];
                    t1.starting_bankroll = Tabledata["starting_bankroll"];
                    t1.current_bankroll = Tabledata["current_bankroll"];
                    t1.min_bet = Tabledata["min_bet"];
                    t1.max_bet = Tabledata["max_bet"];
                    t1.max_betHopes = Tabledata["max_hop"];
                    t1.odds_4_by_10 = Tabledata["odds_4_by_10"];
                    t1.odds_5_by_9 = Tabledata["odds_5_by_9"];
                    t1.odds_6_by_8 = Tabledata["odds_6_by_8"];
                    t1.max_odds = Tabledata["max_odds"];
                    t1.auto_roll_seconds = Tabledata["auto_roll_seconds"];
                    t1.users_id = Tabledata["user_id"]["_id"];
                    t1.user_name = PlayerPrefs.GetString("username");
                    t1.socket_id = Tabledata["socket_id"];
                    t1.__v = Tabledata["__v"];
                    t1.TableName = Tabledata["name"];
                    t1.PutsBet = Tabledata["put_bets"];
                    t1.PayOutMode = Tabledata["payout_mode"];
                    t1.createdAt = Tabledata["createdAt"];
                    t1.updatedAt = Tabledata["updatedAt"];
                    t1.link = Tabledata["link"];
                    t1.Origional_link = Tabledata["original_link"];
                    t1.start_date = Tabledata["start_date"];
                    int a = Tabledata["joinedUser"].Count;
                    t1.AdminName = PlayerPrefs.GetString("username"); 
                    t1.table_user_id = Tabledata["table_user_id"];
                    t1.table_password = Tabledata["table_password"];
                    if (a > 0)
                    {
                        for (int j = 0; j < a; j++)
                        {
                            joinedUsers k = new joinedUsers();
                            k._id = Tabledata["joinedUser"][j]["_id"];
                            k.UserName = Tabledata["joinedUser"][j]["username"].Value; //+ " "+ Tabledata["joinedUser"][j]["last_name"];
                            k.Email = Tabledata["joinedUser"][j]["email"];
                            t1.joineds.Add(k);
                        }
                        //t1.joineds.Add(JsonUtility.FromJson<joinedUsers>(Tabledata["start_date"]));
                    }
                    if (t1.type == "invite")
                    {
                        //t1.type = "pub";
                        t1.AdminTableId = Tabledata["parent_id"]["_id"];
                        t1.AdminId = Tabledata["parent_id"]["user_id"]["_id"];
                        t1.AdminName = Tabledata["parent_id"]["user_id"]["username"].Value; //+" "+ Tabledata["parent_id"]["user_id"]["last_name"];

                        joinedUsers k = new joinedUsers();
                        k._id = t1.AdminId;
                        k.UserName = t1.AdminName;
                        t1.joineds.Add(k);
                    }
                    else if (t1.type == "pub")
                    {
                        joinedUsers k = new joinedUsers();
                        k._id = t1.users_id;
                        k.UserName = t1.user_name;
                        t1.AdminName = t1.user_name;
                        t1.AdminTableId = t1.id;

                        t1.joineds.Add(k);
                    }
                    if (t1.link == null) t1.link = "no_url";
                    if (t1.Origional_link == null) t1.Origional_link = "no_url";
                    if (t1.max_odds == null)
                        t1.max_odds = "0";
                    if (t1.max_betHopes == null)
                        t1.max_betHopes = "0";

                    UIManager.ins.PlayerPrivateTableList.Add(t1);

                }
            }

            UIManager.ins.GetTableList();
        }
    }


    IEnumerator GetTournamentDetail()
    {
        string WebURL1 = Links.GetTournamentList;
        using (UnityWebRequest www = UnityWebRequest.Get(WebURL1))
        {
            // www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.Send();
            if (www.isNetworkError)
            {
                ErrorMsg.text = www.error;
                ErrorMsg.transform.parent.gameObject.SetActive(true);
                LoadingScreen.SetActive(false);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                JSONNode dd = JSONNode.Parse(www.downloadHandler.text);

                JSONNode data = dd["data"]["output"];
                string successCode = dd["messageCode"];
                // Debug.Log(data.Count);
                for (int i = 0; i < data.Count; i++)
                {
                    TableData t1 = new TableData();
                    JSONNode Tabledata = data[i];
                    t1.type = Tabledata["type"];
                    t1.layout = Tabledata["layout"];
                    t1.status = Tabledata["status"];
                    t1.payout_metric = Tabledata["payout_metric"];
                    t1.hop_bet_easy_way = Tabledata["hop_bet_easy_way"];
                    t1.hop_bet_hard_way = Tabledata["hop_bet_hard_way"];
                    t1.field_12_pays = Tabledata["field_12_pays"];
                    t1.field_2_pays = Tabledata["field_2_pays"];
                    t1.allow_buy_5_by_9 = Tabledata["allow_buy_5_by_9"];
                    t1.dont_pass = Tabledata["dont_pass"];
                    t1.pay_vig_on_buys = Tabledata["pay_vig_on_buys"];
                    t1.pay_vig_on_lays = Tabledata["pay_vig_on_lays"];
                    t1.bonus_craps = Tabledata["bonus_craps"];
                    t1.bonus_payout = Tabledata["bonus_payout"];
                    t1.allow_player_compare = Tabledata["allow_player_compare"];
                    t1.roll_options = Tabledata["roll_options"];
                    t1.auto_roll_pause = Tabledata["auto_roll_pause"];
                    t1.is_delete = Tabledata["is_delete"];
                    t1.id = Tabledata["_id"];
                    t1.starting_bankroll = Tabledata["starting_bankroll"];
                    t1.min_bet = Tabledata["min_bet"];
                    t1.max_bet = Tabledata["max_bet"];
                    t1.max_betHopes = Tabledata["max_hop"];
                    t1.odds_4_by_10 = Tabledata["odds_4_by_10"];
                    t1.odds_5_by_9 = Tabledata["odds_5_by_9"];
                    t1.odds_6_by_8 = Tabledata["odds_6_by_8"];
                    t1.max_odds = Tabledata["max_odds"];
                    t1.StartDate = Tabledata["start_date"];
                    t1.EndDate = Tabledata["end_date"];
                    t1.buyin = Tabledata["buyin"];
                    t1.rebuy = Tabledata["rebuy"];
                    t1.no_of_players = Tabledata["no_of_players"];
                    t1.auto_roll_seconds = Tabledata["auto_roll_seconds"];
                    t1.socket_id = Tabledata["socket_id"];
                    t1.__v = Tabledata["__v"];
                    t1.TableName = Tabledata["name"];
                    t1.PutsBet = Tabledata["put_bets"];
                    t1.PayOutMode = Tabledata["payouts"];
                    if (t1.max_odds == null)
                        t1.max_odds = "0";
                    if (t1.max_betHopes == null)
                        t1.max_betHopes = "0";
                    if (t1.buyin == null)
                        t1.buyin = "0";
                    if (t1.rebuy == null)
                        t1.rebuy = "0";
                    t1.createdAt = Tabledata["createdAt"];
                    t1.Rolls = Tabledata["rolls"];
                    t1.Shooters = Tabledata["shooters"];
                    t1.WinnerAmt = Tabledata["winner_id"]["range"][0]["price"];
                    t1.TotalAmt = Tabledata["winner_id"]["total_price"];
                    UIManager.ins.TournamentTableList.Add(t1);
                }
            }
            Debug.Log("Tournament Added ");
            UIManager.ins.TournamentBtn.interactable = true;
        }
    }
    bool isshow = false;
   
    void ShowPasswordOperation()
    {
        //if (IsVerionMainCheck)
        {
            isshow = !isshow;
            if (isshow)
            {
                showpasswordbutton.gameObject.SetActive(true);
                hidepasswordbutton.gameObject.SetActive(false);
                if (LoginPasswordField.contentType == (TMPro.TMP_InputField.ContentType)InputField.ContentType.Password)
                {
                    LoginPasswordField.contentType = (TMPro.TMP_InputField.ContentType)InputField.ContentType.Standard;
                    LoginPasswordField.enabled = false;
                    LoginPasswordField.enabled = true;
                }
            }
            else
            {
                showpasswordbutton.gameObject.SetActive(false);
                hidepasswordbutton.gameObject.SetActive(true);
                if (LoginPasswordField.contentType == (TMPro.TMP_InputField.ContentType)InputField.ContentType.Standard)
                {
                    //                  Debug.Log ("hide password");
                    LoginPasswordField.contentType = (TMPro.TMP_InputField.ContentType)InputField.ContentType.Password;
                    LoginPasswordField.enabled = false;
                    LoginPasswordField.enabled = true;
                }
            }
        }

    }

    public static bool IsValid(string email)
    {
        if (email != null)
            return Regex.IsMatch(email, MatchEmailPattern);
        else
            return false;
    }

    public void BacktoHomeScreen()
    {
        AllowEnter = 0;
        MainHomeScreen.SetActive(true);
        PlayOffline.gameObject.SetActive(true);
        GotoLogin.gameObject.SetActive(true);
        GotoRegister.gameObject.SetActive(true);

        Loginscreen.SetActive(false);
        RegisterScreen.SetActive(false);
        OtpScreen.SetActive(false);
        forgotPasswordScreen.SetActive(false);
        ResetpasswordScreen.SetActive(false);
    }
    public void GuestAccountPanelClose()
    {
        UIManager.ins.LoginScreen.transform.localScale = new Vector3(1, 1, 1);
    }
}
