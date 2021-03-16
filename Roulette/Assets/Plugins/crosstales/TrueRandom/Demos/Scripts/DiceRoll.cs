using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.TrueRandom.Demo
{
   /// <summary>Simulates n random dices with the values 1-6.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/truerandom/api/class_crosstales_1_1_true_random_1_1_demo_1_1_dice_roll.html")]
   public class DiceRoll : MonoBehaviour
   {
      #region Variables

      public GameObject TextPrefab;

      public GameObject ScrollView;
      public InputField Number;
      public Text Error;
      public Text Quota;

      #endregion


      #region MonoBehaviour methods

      private void OnEnable()
      {
         TRManager.Instance.OnGenerateIntegerStart += onGenerateIntegerStart;
         TRManager.Instance.OnGenerateIntegerFinished += onGenerateIntegerFinished;
         TRManager.Instance.OnQuotaUpdate += onUpdateQuota;
         TRManager.Instance.OnErrorInfo += onError;

         if (Quota != null)
            Quota.text = "Quota: " + TRManager.Instance.CurrentQuota;
      }

      private void OnDisable()
      {
         TRManager.Instance.OnGenerateIntegerStart -= onGenerateIntegerStart;
         TRManager.Instance.OnGenerateIntegerFinished -= onGenerateIntegerFinished;
         TRManager.Instance.OnQuotaUpdate -= onUpdateQuota;
         TRManager.Instance.OnErrorInfo -= onError;
      }

      #endregion


      #region Public methods

      public void SimulateRoll()
      {
         if (Error != null)
            Error.text = string.Empty;

         if (Number != null && int.TryParse(Number.text, out int number))
         {
            TRManager.Instance.GenerateInteger(1, 6, 2);
         }
         else
         {
            if (Error != null)
               Error.text = "'Number of dices' is not a number!";
         }
      }

        public void SetRollOnClick()
        {
            TRManager.Instance.GenerateInteger(1, 6, 2);
        }
        #endregion


        #region Callbacks

        private void onGenerateIntegerStart(string id)
      {
         if (Crosstales.TrueRandom.Util.Config.DEBUG)
            Debug.Log("Start simulating dice rolls: " + id, this);
      }

      private void onGenerateIntegerFinished(List<int> e, string id)
      {
         //if (Crosstales.TrueRandom.Util.Config.DEBUG)
            Debug.Log("Finished simulating dice rolls: " + id, this);
            for(int ii = 0; ii < e.Count; ii++)
             { Debug.Log(e[ii], this); }
            /* for (int ii = ScrollView.transform.childCount - 1; ii >= 0; ii--)
             {
                Transform child = ScrollView.transform.GetChild(ii);
                child.SetParent(null);
                Destroy(child.gameObject);
             }

             ScrollView.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 80 * e.Count);

             for (int ii = 0; ii < e.Count; ii++)
             {
                if (Crosstales.TrueRandom.Util.Config.DEBUG)
                   Debug.Log(e[ii], this);

                GameObject go = Instantiate(TextPrefab, ScrollView.transform, true);

                go.transform.localScale = Vector3.one;
                go.transform.localPosition = new Vector3(10, -80 * ii, 0);
                go.GetComponent<Text>().text = e[ii].ToString();}

                */

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
// © 2016-2020 crosstales LLC (https://www.crosstales.com)