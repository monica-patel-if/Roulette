using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.TrueRandom.Demo
{
   /// <summary>Magic 8-Ball simulator.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/truerandom/api/class_crosstales_1_1_true_random_1_1_demo_1_1_magic8_ball.html")]
   public class Magic8Ball : MonoBehaviour
   {
      #region Variables

      public InputField Question;
      public Text Answer;

      private string[] answers =
      {
         // affirmative answers
         "It is certain",
         "It is decidedly so",
         "Without a doubt",
         "Yes definitely",
         "You may rely on it",
         "As I see it, yes",
         "Most likely",
         "Outlook good",
         "Yes",
         "Signs point to yes",
         // non-committal answers
         "Reply hazy try again",
         "Ask again later",
         "Better not tell you now",
         "Cannot predict now",
         "Concentrate and ask again",
         // negative answers
         "Don't count on it",
         "My reply is no",
         "My sources say no",
         "Outlook not so good",
         "Very doubtful"
      };

      public Text Error;
      public Text Quota;

      #endregion


      #region MonoBehaviour methods

      private void OnEnable()
      {
         if (Answer != null)
            Answer.text = string.Empty;

         TRManager.Instance.OnGenerateIntegerStart += onGenerateIntegerStart;
         TRManager.Instance.OnGenerateIntegerFinished += onGenerateIntegerFinished;
         TRManager.Instance.OnQuotaUpdate += onUpdateQuota;
         TRManager.Instance.OnErrorInfo += onError;

         if (Quota != null)
            Quota.text = "Quota: " + TRManager.Instance.CurrentQuota;
      }

      private void OnDisable()
      {
         TRManager.Instance.OnGenerateIntegerStart += onGenerateIntegerStart;
         TRManager.Instance.OnGenerateIntegerFinished -= onGenerateIntegerFinished;
         TRManager.Instance.OnQuotaUpdate -= onUpdateQuota;
         TRManager.Instance.OnErrorInfo -= onError;
      }

      #endregion


      #region Public methods

      public void Ask()
      {
         if (Error != null)
            Error.text = string.Empty;

         if (Question != null && !string.IsNullOrEmpty(Question.text))
         {
            TRManager.Instance.GenerateInteger(0, answers.Length - 1);
         }
         else
         {
            if (Error != null)
               Error.text = "Please enter a question!";
         }
      }

      #endregion


      #region Callbacks

      private void onGenerateIntegerStart(string id)
      {
         if (Util.Config.DEBUG)
            Debug.Log("Start simulating Magic8 ball: " + id, this);
      }

      private void onGenerateIntegerFinished(System.Collections.Generic.List<int> result, string id)
      {
         if (Util.Config.DEBUG)
            Debug.Log("Finished simulating Magic8 ball: " + id, this);

         int index = result[0];

         if (index < 10)
         {
            if (Answer != null)
               Answer.text = "<color=#00ff00ff>" + answers[index] + "</color>"; //green
         }
         else if (index > 14)
         {
            if (Answer != null)
               Answer.text = "<color=#ff0000ff>" + answers[index] + "</color>"; //red
         }
         else
         {
            if (Answer != null)
               Answer.text = "<color=#ffff00ff>" + answers[index] + "</color>"; //yellow
         }
      }

      private void onUpdateQuota(int e)
      {
         if (Quota != null)
            Quota.text = "Quota: " + e;
      }

      private void onError(string e)
      {
         if (Error != null)
            Error.text = e;
      }

      #endregion
   }
}
// © 2017-2020 crosstales LLC (https://www.crosstales.com)