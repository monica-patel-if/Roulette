using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebServicesKeys 
{
    #region MAIL_CHECK
    public const string emailkey = "email";
    public const string address = "address";
    public const string city = "city";
    public const string state = "state";
    public const string country = "country";
    public const string zip_code = "zip_code";
    #endregion

    #region REGISTRATION

    public const string registerinput = "register";
    public const string namekey = "name";
    public const string fnamekey = "first_name";
    public const string lnamekey = "last_name";
    public const string usernamekey = "username";
    public const string userpasskey = "password";
    public const string usermailkey = "email";
    public const string phone = "phone";


    #endregion

    #region FORLOGIN

    public const string loginvalue = "login";
    public const string logoutvalue = "logout";
    public const string username = "username";
    public const string password = "password";
    public const string old_password = "old_password";
    public const string os = "os";
    public const string browser = "browser";
    #endregion

    #region FBLOGIN

    public const string fbkey = "facebookid";

    #endregion

    #region WEBLOGIN

    public const string webloginvalue = "check_weblogin";

    #endregion

    #region CHANGEPASSWORD

    public const string resetpasswordvalue = "resetpassword";
    public const string useridkey = "uid";
    public const string oldpasskey = "opass";

    #endregion

    #region ROLLUPLOADTOSERVER

    public const string table_id = "table_id";
    public const string historyData = "player_history";
    public const string roll_data = "roll_data";
    public const string type = "roll";



    #endregion

    #region friend
    public const string friend_id = "friend_id";

    #endregion
    #region BRINGFRIENDREFERRAL

    public const string referralinput = "referral";

    #endregion

    #region REPORTAPROBLEM

    public const string reportvalue = "report";
    public const string fidkey = "facebookid";
    public const string tidkey = "tournamentid";
    public const string rtypekey = "roomtype";
    public const string commentkey = "commentnt";

    #endregion

    #region TOURNAMENTLIST

    public const string tournamentinputvalue = "tournament_list";

    #endregion

    #region create Table 

    public const string tablename = "name";
    public const string TabLayout = "layout";
    public const string tabletype = "type";
    public const string tablestatus = "status";
    public const string tableSB = "starting_bankroll";
    public const string tableMin = "min_bet";
    public const string tableMax = "max_bet";
    public const string tablePayout = "payout_mode";
    public const string tableOdds4 = "odds_4_by_10";
    public const string tableOdds5 = "odds_5_by_9";
    public const string tableOdds6 = "odds_6_by_8";
    public const string maxOdds = "max_odds";
    public const string hopeasyBet = "hop_bet_easy_way";
    public const string maxhophardway = "max_hop";
    public const string hopHardBet = "hop_bet_hard_way";
    public const string filed12pay = "field_12_pays";
    public const string filed2pay = "fiels_2_pays";
    public const string allow5_9Buy = "allow_buy_5_by_9";
    public const string tableDP = "dont_pass";
    public const string BuyVig = "pay_vig_on_buys";
    public const string LayVig = "pay_vig_on_lays";
    public const string bonusCrap = "bonus_craps";
    public const string bonusPayout = "bonus_payout";
    public const string allowPlayerCompare = "allow_player_compare";
    public const string RollOption = "roll_options";
    public const string AutoRollSeconds = "auto_roll_seconds";
    public const string AutoROllPause = "auto_roll_pause";
    public const string put_bets = "put_bets";
    public const string start_date = "start_date";
    public const string payout_metric = "payout_metric";
    public const string payout_schedule = "payout_schedule";
    #endregion

    #region for join group table 
    public const string user_id_joined = "user_id";
    public const string table_user_id = "table_user_id";
    public const string table_password = "table_password";
    #endregion

    #region for join friend table
    public const string friendId = "username";
    public const string friendPass = "joined_password";
#endregion

}
// key":"joined_password","value":"159079","description":"","type":"text","enabled":true},{"key":"email","value":"tomy@mailinator.com