using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.TrueRandom.Demo
{
   /// <summary>Generate random integers.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/truerandom/api/class_crosstales_1_1_true_random_1_1_demo_1_1_generate_integer.html")]
   public class GenerateInteger : MonoBehaviour
   {
      #region Variables

      public GameObject TextPrefab;

      public GameObject ScrollView;

      public InputField Number;
      public InputField Min;
      public InputField Max;
      public Text Error;
      public Text Quota;

      public Button ButtonSave;

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

         ButtonSave.interactable = false;

#if !CT_FB
         Debug.LogWarning("'File Browser' package not installed! Please install it to save text-files: " + Util.Constants.ASSET_FB, this);
#endif
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

      public void GenerateInt()
      {
         if (Quota != null)
            Error.text = string.Empty;

         if (Number != null && int.TryParse(Number.text, out int number))
         {
            if (Min != null && int.TryParse(Min.text, out int min))
            {
               if (Max != null && int.TryParse(Max.text, out int max))
               {
                  TRManager.Instance.GenerateInteger(min, max, number);
               }
               else
               {
                  if (Quota != null)
                     Error.text = "'Max value' is not a number!";
               }
            }
            else
            {
               if (Quota != null)
                  Error.text = "'Min value' is not a number!";
            }
         }
         else
         {
            if (Quota != null)
               Error.text = "'Number of integers' is not a number!";
         }
      }

      public void SaveFile()
      {
#if CT_FB
         string path = FB.FileBrowser.SaveFile("Integers", "txt");

         if (!string.IsNullOrEmpty(path))
            Util.Helper.SaveAsText(path, TRManager.Instance.CurrentIntegers);
#else
         Debug.LogWarning("'File Browser' package not installed! Please install it to use this function: " + Util.Constants.ASSET_FB, this);
#endif
      }

      #endregion


      #region Callbacks

      private void onGenerateIntegerStart(string id)
      {
         if (Util.Config.DEBUG)
            Debug.Log("Start generating numbers: " + id, this);
      }

      private void onGenerateIntegerFinished(List<int> e, string id)
      {
         if (Util.Config.DEBUG)
            Debug.Log("Finished generating numbers: " + id, this);

         for (int ii = ScrollView.transform.childCount - 1; ii >= 0; ii--)
         {
            Transform child = ScrollView.transform.GetChild(ii);
            child.SetParent(null);
            Destroy(child.gameObject);
         }

         ScrollView.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 80 * e.Count);

         for (int ii = 0; ii < e.Count; ii++)
         {
            if (Util.Config.DEBUG)
               Debug.Log(e[ii], this);

            GameObject go = Instantiate(TextPrefab, ScrollView.transform, true);

            go.transform.localScale = Vector3.one;
            go.transform.localPosition = new Vector3(10, -80 * ii, 0);
            go.GetComponent<Text>().text = e[ii].ToString();
         }

         ButtonSave.interactable = true;
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