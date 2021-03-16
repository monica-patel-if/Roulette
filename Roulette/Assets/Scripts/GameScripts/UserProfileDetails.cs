using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Michsky.UI.ModernUIPack;
using System;
using UnityEngine.Networking;
using SimpleJSON;
//using Newtonsoft.Json;

public class UserProfileDetails : MonoBehaviour
{

    public static UserProfileDetails ins;

    public UIManager uim;
    public Button userProfileToggle;
    public Image ChipImg, AvatarImg;
    public Sprite icons;
    public Sprite[] ChipsSprite;

    [Header("-- Menu Buttons--")]
    public Text WelcomeBackTxt;
    public Text UsernameTxt, titleTxt;
    public TMPro.TMP_Text UserFlNameTxt;
    public GameObject MenuObjScreen;
    [Header("-- Menu Buttons--")]
    public Button profileBtn;
    public Button howtoPlayBtn,
        GamefaQbtn,
        subscriptionBtn,
        SupportReqBtn,
        LogoutBtn;

    [Header("-- Screens --")]
    public GameObject MyProfileScreen;
    public GameObject HowtoPlayScreen,
        gameFaqsScreen,
        MySubscriptionScreen,
        SupportreqScreen;

    public GameObject BrowserWindow;

    [Header("-- Update Profile Objs --")]
    public Button UpdateImgs, SelectFromGallaryBtn;
    public Button updatePersonalInfoBtn, UpdateLoginInfoBtn;
    public TMPro.TMP_Text ScreenNameTxt, NameTxt, AddressTxt, EmailTxt, PhoneTxt;
    public GameObject MainUpdateScreen, ImgUpdatescreen, PersonalInfoUpdatescreen, LoginInfoUpdateScreen;
    public Button SaveInfoBtn;
    public TMPro.TMP_InputField ScreenNameField, fNameField, LNameField, AddressField, CityField, ZipCodeField, EmailField, PhoneField, PasswordField;
    public Image ChipsColor, MainChipselectionImg, avatarImg, UpdatedAvtarimg;
    public Button NextChipBtn, PrevChipBtn;
      public Dropdown statesList, countryList;

    [Header("-- Subscription objs --")]
    public GameObject SubPrefabObj;
    public GameObject SubParentObj;


    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    private void Start()
    {

           countryList.onValueChanged.AddListener((arg0) => getStateList());
           statesList.onValueChanged.AddListener((arg0) => Debug.Log(statesList.value));
        //Debug.Log("Chip colour : " + PlayerPrefs.GetInt("imgCount", 0));
        MainChipselectionImg.sprite = ChipsColor.sprite = ChipImg.sprite = spriteEdit.ins.ChipsSprite[PlayerPrefs.GetInt("imgCount", 0)];

        userProfileToggle.onClick.AddListener(() => OnSelectedToggleValue());
        profileBtn.onClick.AddListener(() => GetMenuOn(MyProfileScreen, "My Profile"));
        howtoPlayBtn.onClick.AddListener(() => GetMenuOn(HowtoPlayScreen, "How to Play Crapsee"));
        GamefaQbtn.onClick.AddListener(() => GetMenuOn(gameFaqsScreen, "Game FAQs"));
        SupportReqBtn.onClick.AddListener(() => GetMenuOn(SupportreqScreen, "Support Requests "));
        subscriptionBtn.onClick.AddListener(() => GetMenuOn(MySubscriptionScreen, "My Subscriptions"));
        LogoutBtn.onClick.AddListener(() => Loginmanager.ins.LogoutData());

        //SelectFromGallaryBtn.onClick.AddListener(() => PickImage());
        //UpdateImgs.onClick.AddListener(() => updateImgInfo(ImgUpdatescreen, 0));
        updatePersonalInfoBtn.onClick.AddListener(() => updateImgInfo(PersonalInfoUpdatescreen));
        UpdateLoginInfoBtn.onClick.AddListener(() => updateImgInfo(LoginInfoUpdateScreen));

        SaveInfoBtn.onClick.AddListener(() => SaveInfo());
        NextChipBtn.onClick.AddListener(() => setChip(1));
        PrevChipBtn.onClick.AddListener(() => setChip(-1));
    }
    int Count;
    void setChip(int i)
    {
        Count += i;
        if (Count >= ChipsSprite.Length)
            Count = ChipsSprite.Length-1;
        else if (Count <= 0)
            Count = 0;

        MainChipselectionImg.sprite = ChipsColor.sprite = ChipImg.sprite = ChipsSprite[Count];

        Debug.Log("Chip colour : " + PlayerPrefs.GetInt("imgCount", 0));

        StartCoroutine(setChipColour(Count));
    }
    IEnumerator setChipColour(int Count)
    {
        WWWForm form = new WWWForm();
        form.AddField("chip_color", Count);

        using (UnityWebRequest www = UnityWebRequest.Post(Links.GetUserProfileUrl, form))
        {
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            if (www.isDone)
            {
                Debug.Log(www.downloadHandler.text);
                PlayerPrefs.SetInt("imgCount", Count);
            }
        }

    }
    public void SaveInfo()
    {
        StartCoroutine(UpdateInfo());
    }


    /*
       IEnumerator ChangeDP()
       {
           string path = "";
   #if UNITY_2019_3
            path = "file://"+Application.persistentDataPath + "/icon.png";
   #elif UNITY_ANDROID
           path = iconPath;
   #endif
           Debug.Log(path);
           Texture2D texture = NativeGallery.LoadImageAtPath(iconPath, 512, false, false, false);
           byte[] Photo = texture.EncodeToPNG();

           WWWForm form = new WWWForm();

           form.AddBinaryData("file", Photo, "icon.png");

           using (UnityWebRequest www = UnityWebRequest.Post(Links.ChangeProfilePicUrl, form))
           {
               www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
               yield return www.SendWebRequest();
               if (www.isDone)
               {

                   JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                   string msg = dd["messageCode"].Value;
                   string statusCode = dd["statusCode"].Value;
                   if (msg.Contains("CHANGE_PROFILE") && statusCode == "200")
                   {
                       Debug.Log(www.downloadHandler.text);
                       Debug.Log("Profile pic Changed ");
                       UpdatedAvtarimg.sprite = avatarImg.sprite = AvatarImg.sprite = myUserDetails.ProfileImg;
                       MainUpdateScreen.SetActive(false);


                   }
               }
           }
       }*/

    IEnumerator UpdateInfo()
    {
        WWWForm form = new WWWForm();
        form.AddField(WebServicesKeys.fnamekey, fNameField.text);
        form.AddField(WebServicesKeys.lnamekey, LNameField.text);
        form.AddField(WebServicesKeys.address, AddressField.text);
        form.AddField(WebServicesKeys.city, CityField.text.Replace("\n", ""));
        form.AddField(WebServicesKeys.zip_code, ZipCodeField.text);
           form.AddField(WebServicesKeys.state, statesList.captionText.text);
           form.AddField(WebServicesKeys.country, countryList.captionText.text);
        form.AddField(WebServicesKeys.emailkey, EmailField.text);
        form.AddField(WebServicesKeys.username, ScreenNameField.text);
        form.AddField(WebServicesKeys.phone, PhoneField.text);
        if (PasswordField.text != PlayerPrefs.GetString("password"))
        { StartCoroutine(ChangePassword()); }
        using (UnityWebRequest www = UnityWebRequest.Post(Links.GetUserProfileUrl, form))
        {
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            if (www.isDone)
            {
                Debug.Log(www.downloadHandler.text);
                JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                string msg = dd["messageCode"].Value;
                string statusCode = dd["statusCode"].Value;

                if (msg.Contains("UPDATE_USER") && statusCode == "200")
                {
                    JSONNode data = dd["data"];
                    PlayerPrefs.SetInt("fromMenu", 1);


                    myUserDetails.first_name = data["first_name"];
                    myUserDetails.last_name = data["last_name"];
                    myUserDetails.balance = data["balance"];
                    myUserDetails.email = data["email"];
                    myUserDetails.subscription_id = data["subscription_id"]["_id"];
                    myUserDetails.subscription_purchase_date = data["subscription_purchase_date"];
                    myUserDetails.subscription_expire_date = data["subscription_expire_date"];
                    myUserDetails.phone = data["phone"];
                    myUserDetails.profile_pic = data["profile_pic"];
                    myUserDetails.address = data["address"];
                    myUserDetails.device_id = data["device_id"];
                    myUserDetails.device_type = data["device_type"];
                    myUserDetails.fcm_token = data["fcm_token"];
                    myUserDetails.joined_password = data["joined_password"];
                    myUserDetails.username = data["username"];
                    myUserDetails._id = data["_id"];
                    myUserDetails.city = data["city"];
                    myUserDetails.state = data["state"];
                    myUserDetails.country = data["country"];
                    myUserDetails.zip = data["zip_code"];


                    if (data["address"].Value != null)
                    {
                        //try
                        {
                            Debug.Log("Address : " + data["address"].Value);
                            Debug.Log("city : " + data["city"].Value);
                            Debug.Log("zip : " + data["zip_code"].Value);
                            Debug.Log("country : " + data["country"].Value);
                            Debug.Log("state : " + data["state"].Value);

                            AddressField.text = data["address"].Value;
                            CityField.text = data["city"].Value;
                            ZipCodeField.text = data["zip_code"].Value;
                            AddressTxt.text = data["address"] + "\n" +
                            data["city"] + ", " + data["state"].Value + "\n" + data["country"].Value + "  " + data["zip_code"].Value;

                        }
                        // catch
                        {

                        }

                    }

                    UsernameTxt.text = ScreenNameField.text = ScreenNameTxt.text = myUserDetails.username;
                    fNameField.text = myUserDetails.first_name;
                    LNameField.text = myUserDetails.last_name;
                    EmailField.text = EmailTxt.text = myUserDetails.email;
                    PhoneField.text = PhoneTxt.text = myUserDetails.phone;
                    NameTxt.text = fNameField.text + " " + LNameField.text;
                    statesList.captionText.text = myUserDetails.state;
                    countryList.captionText.text = myUserDetails.country;

                    //StartCoroutine(getUserdata());
                    MainUpdateScreen.SetActive(false);

                }
                else
                {
                    //UIManager.ins.GroupErrorMsg.SetActive(true);
                }
            }
        }
    }
    void updateImgInfo(GameObject obj)
    {
        MainUpdateScreen.SetActive(true);
        ImgUpdatescreen.SetActive(false);
        PersonalInfoUpdatescreen.SetActive(false);
        LoginInfoUpdateScreen.SetActive(false);
        obj.SetActive(true);
        // MainUpdateScreen.GetComponent<ModalWindowManager>().OpenWindow();
        SaveInfoBtn.gameObject.SetActive(true);
        Debug.Log("= called  " + obj.name);
        SoundManager.instance.playForOneShot(SoundManager.instance.ButtonClickClip);
    }

    //Set menu Header 
    void GetMenuOn(GameObject obj, string title)
    {

        profileBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        howtoPlayBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        GamefaQbtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        subscriptionBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        SupportReqBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;


        profileBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        howtoPlayBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        GamefaQbtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        subscriptionBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        SupportReqBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

        MyProfileScreen.SetActive(false);
        HowtoPlayScreen.SetActive(false);
        gameFaqsScreen.SetActive(false);
        MySubscriptionScreen.SetActive(false);
        SupportreqScreen.SetActive(false);


        obj.SetActive(true);//Profile Crapsee Game Support Subscriptions
        if (title.Contains("Profile"))
        {
            profileBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            profileBtn.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            BrowserWindow.SetActive(false);
        }
        else if (title.Contains("Crapsee"))
        {
            howtoPlayBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            howtoPlayBtn.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            // GetBrowserOpen("http://dev.crapsee.com/video/howtoplay");
        }
        else if (title.Contains("Game"))
        {
            GamefaQbtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            GamefaQbtn.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            //GetBrowserOpen("http://dev.crapsee.com/video/gamefaqs");
        }
        else if (title.Contains("Support"))
        {
            SupportReqBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            SupportReqBtn.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            //GetBrowserOpen("http://dev.crapsee.com/video/supportrequests");
        }
        else if (title.Contains("Subscriptions"))
        {
            subscriptionBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            subscriptionBtn.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            BrowserWindow.SetActive(false);
            StartCoroutine(GetPurchasedHistory());
        }
        titleTxt.text = title;
    }

    /*
     * Add to GitHUb
Denis Betsi
denisbetsi@gmail.com   

    */
    void OnSelectedToggleValue()
    {
        if (PlayerPrefs.GetInt("IsloggedInBefore") != 1)
            uim.LoginScreen.transform.localScale = new Vector3(1, 1, 1);
        else
            uim.LoginScreen.transform.localScale = new Vector3(0, 1, 1);

        uim.searchFrnd.text = "";
        uim.PlayNowBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        uim.FriendsBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        uim.VideoBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        uim.CoachesBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        uim.CoachesBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);
        uim.FriendsBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);
        uim.VideoBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);
        uim.PlayNowBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);


        uim.PlayNowPanel.SetActive(false);
        uim.FriendsPanel.SetActive(false);
        uim.VideoPanel.SetActive(false);
        uim.CoachesPanel.SetActive(false);
        MenuObjScreen.SetActive(true);

        userProfileToggle.gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 255);

        GetMenuOn(MyProfileScreen, "My Profile");

    }

    public void getUserProfile()
    {
        StartCoroutine(getUserdata());
    }
    public void GetSubscription()
    {
        StartCoroutine(GetPurchasedHistory());
    }
    public void GotoSubscripitonPage()
    {
        // SubscriptionPage.ins.AfterSubscriptionScreen.GetComponent<ModalWindowManager>().CloseWindow();
        SubscriptionPage.ins.AfterSubscriptionScreen.SetActive(false);
        OnSelectedToggleValue();
        GetMenuOn(MySubscriptionScreen, "My Subscriptions");

    }
    IEnumerator GetPurchasedHistory()
    {
       //Debug.Log( " last : 11" );
        using (UnityWebRequest www = UnityWebRequest.Get(Links.PurchasedHistoryUrl))
        {
          // Debug.Log(" last : 1122");
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            if (www.isDone)
            {
                for (int i = 2; i < SubParentObj.transform.childCount; i++)
                {
                    Destroy(SubParentObj.transform.GetChild(i).gameObject);
                }
              
                JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                string msg = dd["messageCode"].Value;
                string statusCode = dd["statusCode"].Value;
                JSONNode data = dd["data"];
                int PurchasedCOunt = data.Count;
               // Debug.Log(" last : 222  " + PurchasedCOunt);

               // Debug.Log(" last : 222  " + www.downloadHandler.text);
                for (int i = 0; i < PurchasedCOunt; i++)
                {
                   
                //    Debug.Log(data[i]["subscription"]["current_period_end"].Value);
                //    Debug.Log(int.Parse(data[i]["subscription"]["current_period_end"].Value));
                    int RenewOnDate = int.Parse(data[i]["subscription"]["current_period_end"].Value);
                    int LastPayTxtDate = int.Parse(data[i]["subscription"]["current_period_start"].Value);
                    string interval = data[i]["subscription"]["items"]["data"][0]["plan"]["interval"].Value;
                    string Amount = data[i]["subscription"]["items"]["data"][0]["plan"]["amount"].Value;

                   // Debug.Log(RenewOnDate + " last : " + LastPayTxtDate + " interval : " + interval + " amount : " + Amount);

                    GameObject SubscribeObj = Instantiate(SubPrefabObj);
                    //SubscribeObj.transform.GetChild(1).GetComponent<Text>().text = "..";
                    SubscribeObj.name = "..";
                    SubscribeObj.transform.SetParent(SubParentObj.transform);
                    SubscribeObj.transform.SetSiblingIndex(2);
                    SubscribeObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                    SubscribeObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
                    SubscribeObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(SubscribeObj.transform.GetComponent<RectTransform>().localPosition.x, SubscribeObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
                    SubscriptionDetail detail = SubscribeObj.GetComponent<SubscriptionDetail>();
                    detail.GameTxt.text = "Crapsee";

                    string createdDate = data[i]["createdAt"].Value;
                    string[] date2 = createdDate.Split('T');
                    string[] currDate = date2[0].Split('-');
                    int date = int.Parse(currDate[2]);
                    int month = int.Parse(currDate[1]);
                    int year = int.Parse(currDate[0]);

                    if (interval == "month")
                    {
                        detail.MembershipTxt.text = "Monthly Membership";
                        detail.AmtTxt.text = "$4.95 US";
                        detail.NextAmtTxt.text = "$4.95 US";
                        SubscriptionPage.ins.MonthlyToggle.isOn = true;
                        detail.LastPayTxt.text = passUtc(data[i]["createdAt"].Value); //passUtc(myUserDetails.subscription_purchase_date);

                        DateTime t = new DateTime(year, month, date);
                       t=t.AddMonths(1);
                        string fdate = t.ToString("yyyy-MM-ddTHH");


                        detail.RenewTxt.text = passUtc(fdate);
                    }
                    else if (interval == "year")
                    {
                        detail.MembershipTxt.text = "Annual Membership";
                        detail.AmtTxt.text = "$49.95 US";
                        detail.NextAmtTxt.text = "$49.95 US";
                        SubscriptionPage.ins.AnnualToggle.isOn = true;
                        detail.LastPayTxt.text = passUtc(data[i]["createdAt"].Value); //passUtc(myUserDetails.subscription_purchase_date);

                        DateTime t = new DateTime(year, month, date);
                         t = t.AddYears(1);
                        string fdate = t.ToString("yyyy-MM-ddTHH");
                        Debug.Log("Fdate : "+fdate);
                        detail.RenewTxt.text = passUtc(fdate);
                    }
                    //Debug.Log(data[i]["createdAt"].Value);
                    SubscriptionPage.ins.NextPayText = detail.NextAmtTxt;
                }
            }

        }
    }

    private string epoch2string(int epoch)
    {
        string d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch).ToShortDateString();
        Debug.Log(d + " <> d date :");
        string[] currDate = d.Split('-');
        Debug.Log(currDate + " <> currDate date :");
        string month = getMonthStringFromNumber(int.Parse(currDate[1]));
        string FinalDate = month + " " + currDate[2] + ", " + currDate[0];
       // Debug.Log(FinalDate + " <> Final date :" );
        return FinalDate;
        // return d;
    }
    string passUtc(string datee)
    {
        string[] data = datee.Split('T');
        string[] currDate = data[0].Split('-');
        string month = getMonthStringFromNumber(int.Parse(currDate[1]));
        string FinalDate = month + " " + currDate[2] + ", " + currDate[0];
       // Debug.Log(FinalDate + " <> Utc Final date :");
        return FinalDate;
    }
    public string getMonthStringFromNumber(int month_number)
    {
        string month = "";

        if (month_number == 1) month = "January";
        if (month_number == 2) month = "February";
        if (month_number == 3) month = "March";
        if (month_number == 4) month = "April";
        if (month_number == 5) month = "May";
        if (month_number == 6) month = "June";
        if (month_number == 7) month = "July";
        if (month_number == 8) month = "August";
        if (month_number == 9) month = "September";
        if (month_number == 10) month = "October";
        if (month_number == 11) month = "November";
        if (month_number == 12) month = "December";

        return month;
    }

    public ProfileData myUserDetails;
    IEnumerator getUserdata()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Links.GetUserProfileUrl))
        {
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            if (www.isDone)
            {
                //Debug.Log(www.downloadHandler.text);
                JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                string msg = dd["messageCode"].Value;
                string statusCode = dd["statusCode"].Value;

                if (msg.Contains("GET_PROFILE") && statusCode == "200")
                {
                    JSONNode data = dd["data"];
                    PlayerPrefs.SetInt("fromMenu", 1);
                    //subscriptionDetail = dd["data"]["subscription"];
                    myUserDetails.first_name = data["first_name"];
                    myUserDetails.last_name = data["last_name"];
                    myUserDetails.balance = data["balance"];
                    myUserDetails.email = data["email"];
                    myUserDetails.subscription_id = data["subscription_id"]["_id"];
                    myUserDetails.subscription_purchase_date = data["subscription_purchase_date"];
                    myUserDetails.subscription_expire_date = data["subscription_expire_date"];
                    myUserDetails.phone = data["phone"];
                    myUserDetails.profile_pic = data["profile_pic"];
                    myUserDetails.address = data["address"];
                    myUserDetails.device_id = data["device_id"];
                    myUserDetails.device_type = data["device_type"];
                    myUserDetails.fcm_token = data["fcm_token"];
                    myUserDetails.joined_password = data["joined_password"];
                    myUserDetails.username = data["username"];
                    myUserDetails._id = data["_id"];
                    myUserDetails.city = data["city"];
                    myUserDetails.state = data["state"];
                    myUserDetails.country = data["country"];
                    myUserDetails.zip = data["zip_code"];


                    if (data["address"].Value != null)
                    {
                     
                        AddressField.text = data["address"].Value;
                        CityField.text = data["city"].Value;
                        ZipCodeField.text = data["zip_code"].Value;
                                      countryList.captionText.text = data["country"].Value;
                                       statesList.captionText.text = data["state"].Value;
                        AddressTxt.text = data["address"] + "\n" +
                        data["city"] + ", " + data["state"].Value + "\n" + data["country"].Value + "  " + data["zip_code"].Value;

                    }
                    string fn = data["first_name"].Value;
                    string ln = data["last_name"].Value;
                    UserFlNameTxt.text = (fn.Substring(0, 1) + ln.Substring(0, 1)).ToString().ToUpper();
                    UsernameTxt.text = ScreenNameField.text = ScreenNameTxt.text = myUserDetails.username;
                    fNameField.text = myUserDetails.first_name;
                    LNameField.text = myUserDetails.last_name;
                    EmailField.text = EmailTxt.text = myUserDetails.email;
                    PhoneField.text = PhoneTxt.text = myUserDetails.phone;
                    NameTxt.text = fNameField.text + " " + LNameField.text;
                    PasswordField.text = PlayerPrefs.GetString("password");
                    // Debug.Log(data["profile_pic"].Value + " " + myUserDetails.profile_pic);
                    if (SubscriptionPage.SubscriptionStatus == "PAID")
                        StartCoroutine(GetPurchasedHistory());
                }
                else
                {
                    //UIManager.ins.GroupErrorMsg.SetActive(true);
                }
            }
        }
    }

    public void GetCountrylist()
    {
        //  StartCoroutine(GetContries());
    }
    //public List<Contries> ContryList = new List<Contries>();

    public List<StatesList> StatesList = new List<StatesList>();
    public void getStateList()
    { StartCoroutine(GetStates()); }
    IEnumerator GetStates()
    {
        yield return true;

       if(countryList.value == 3)
       {
            statesList.ClearOptions();
            statesList.options.Add(new Dropdown.OptionData() { text = "No States" });
            statesList.RefreshShownValue();
            statesList.gameObject.SetActive(true);
            
        }
        else
        {
            using (UnityWebRequest www = UnityWebRequest.Get(Links.statesUrl + countryList.value + "/state"))
            {
                // www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
                yield return www.SendWebRequest();
                if (www.isDone)
                {
                    Debug.Log(www.downloadHandler.text);
                    JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                    string msg = dd["messageCode"].Value;
                    string statusCode = dd["statusCode"].Value;
                    Debug.Log(dd["data"].Count);
                    int count = dd["data"].Count;
                    if (statusCode == "200")
                    {
                      
                        StatesList.Clear();
                        StatesList = new List<StatesList>();
                        if (count == 0)
                        {
                            statesList.ClearOptions();
                            statesList.options.Add(new Dropdown.OptionData() { text = "No States" });
                            statesList.RefreshShownValue();
                        }
                        else
                        {
                            for (int i = 0; i < count; i++)
                            {
                                JSONNode c = dd["data"][i];
                                StatesList c1 = new StatesList();
                               c1._id = c["_id"];
                               c1.state_id = c["state_id"];
                               c1.state_name = c["state_name"];
                                StatesList.Add(c1);
                            }

                          

                        }

                       statesList.ClearOptions();              
                        statesList.options.Add(new Dropdown.OptionData() { text = "Select States" });
                        foreach(StatesList a in StatesList)
                        { statesList.options.Add(new Dropdown.OptionData() { text = a.state_name }); }
                        statesList.RefreshShownValue();
                        statesList.gameObject.SetActive(true);
                    }
                }
            }
        }

    }


    IEnumerator ChangePassword()
    {
        WWWForm form = new WWWForm();
        form.AddField(WebServicesKeys.old_password, PlayerPrefs.GetString("password"));
        form.AddField(WebServicesKeys.password, PasswordField.text);
        using (UnityWebRequest www = UnityWebRequest.Post(Links.ChangePasswordUrl, form))
        {
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            if (www.isDone)
            {
                Debug.Log(www.downloadHandler.text);
                JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                string msg = dd["messageCode"].Value;
                string statusCode = dd["statusCode"].Value;

                if (msg.Contains("CHANGE_PASSWORD") && statusCode == "200")
                {
                    // Loginmanager.ins.NotificationPopUp.description = " Password Change Successfully.";
                    //  Loginmanager.ins.NotificationPopUp.OpenNotification();
                }
            }
        }
    }
}


[System.Serializable]
public class Contries
{
    public string _id;
    public string country_name;
    public string country_id;
}
[System.Serializable]
public class StatesList
{
    public string _id;
    public string state_name;
    public string state_id;

}
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
[System.Serializable]
public class ProfileData
{
    // public string Uid;
    public string profile_pic;
    public Sprite ProfileImg;
    public string device_type;
    public string fcm_token;
    public string device_id;
    public int balance;
    public string subscription_purchase_date;
    public string subscription_expire_date;
    public string phone;
    public string address;
    public string city;
    public string state;
    public string country;
    public string zip;
    public string _id;
    public string first_name;
    public string last_name;
    public string email;
    public string username;
    public string joined_password;
    public string subscription_id;
    public string subscription_active_status;
    public string subscription_is_active;
    public string stripe_customer_id;
}

/*
Monthly = $4.95
Annually = $49.95
*/

