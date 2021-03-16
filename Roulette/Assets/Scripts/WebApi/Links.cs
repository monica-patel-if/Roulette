using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Links 
{

   public const string ServerURL = "http://api.crapsee.com:9001/api/";
   // public const string ServerURL = "http://172.105.35.50:9002/api/";
    public const string GetTournamentList = "http://172.105.35.50:9004/api/tournaments/list";


    public const string applestore = "https://itunes.apple.com/in/app/Crapsee/id00?mt=9";
    public const string playstore = "https://play.google.com/store/apps/details?id=com.crapsee";
    public const string webGl = "http://web.crapsee.com/";
    public const string SiteURL = "http://crapsee.stageserver.ca/";
    public const string termsUrl = "http://web.crapsee.com/panel/terms";
    public const string UserRegisterUrl = ServerURL + "auth/signup";
    public const string veryfiOTPUrl = ServerURL + "auth/verify/"; // + OTP get URL
    public const string ResendCode = ServerURL + "auth/resend-code"; // + resend mail OTP
    public const string UserLoginUrl = ServerURL +"auth/login";
    public const string ForgotPassUrl = ServerURL + "auth/forget-password";
    public const string ResetPassUrl = ServerURL + "auth/reset-password/"; //+ OTP POST

    public const string GetUserProfileUrl = ServerURL + "users/";  //Get
    public const string UpdateUserProfileUrl = ServerURL + "users/"; // PUT
    public const string ChangeProfilePicUrl = ServerURL + "users/change-profile"; //post
    public const string ChangePasswordUrl = ServerURL + "users/change-password"; //Post
    public const string PurchasedHistoryUrl = ServerURL + "purchase/history";
    public const string GetUsersTableList = ServerURL + "tables/list";
    public const string deleteUsetTable = ServerURL + "tables/";
    public const string LeaveTable = ServerURL + "tables/leave?table_id=";
    public const string addRollInDB = ServerURL +"history/add";
    public const string GetTableHistoryURL =ServerURL+ "history/getV2"; //ServerURL+"history/table"; //
    public const string GetCurrentBRurl = ServerURL + "bankroll/";
    public const string CreateTables = ServerURL + "tables";
    public const string GetVideoList = ServerURL + "video/list";
    public const string GetCoachesList = ServerURL + "coaches/list";

    public const string removefrnd = ServerURL + "friends/";
    public const string addfrnd = ServerURL + "friends/";
    public const string GetFriendList = ServerURL + "friends/my-list"; // list of user who are friends 
   
    public const string userList = ServerURL + "friends/list"; // list of user who are not friends

    public const string InviteUrl = ServerURL + "tables/invitation_mail/";
    public const string JoinTableUrl = ServerURL + "requests/join_table";


    public const string contriesUrl = ServerURL + "country/list";
    public const string statesUrl = ServerURL + "country/";

    public const string PaymentUrl = "http://api.crapsee.com:9001/api/stripe/index?priceId=";
}

