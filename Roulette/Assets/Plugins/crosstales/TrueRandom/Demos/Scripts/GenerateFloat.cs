using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.TrueRandom.Demo
{
   /// <summary>Generate random floats.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/truerandom/api/class_crosstales_1_1_true_random_1_1_demo_1_1_generate_float.html")]
   public class GenerateFloat : MonoBehaviour
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
         TRManager.Instance.OnGenerateFloatStart += onGenerateFloatStart;
         TRManager.Instance.OnGenerateFloatFinished += onGenerateFloatFinished;
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
         TRManager.Instance.OnGenerateFloatStart -= onGenerateFloatStart;
         TRManager.Instance.OnGenerateFloatFinished -= onGenerateFloatFinished;
         TRManager.Instance.OnQuotaUpdate -= onUpdateQuota;
         TRManager.Instance.OnErrorInfo -= onError;
      }

      #endregion


      #region Public methods

      public void GenerateFloatNumbers()
      {
         if (Error != null)
            Error.text = string.Empty;

         if (Number != null && int.TryParse(Number.text, out int number))
         {
            if (Min != null && float.TryParse(Min.text, out float min))
            {
               if (Max != null && float.TryParse(Max.text, out float max))
               {
                  TRManager.Instance.GenerateFloat(min, max, number);
               }
               else
               {
                  if (Error != null)
                     Error.text = "'Max value' is not a number!";
               }
            }
            else
            {
               if (Error != null)
                  Error.text = "'Min value' is not a number!";
            }
         }
         else
         {
            if (Error != null)
               Error.text = "'Number of floats' is not a number!";
         }
      }

      public void SaveFile()
      {
#if CT_FB
         string path = FB.FileBrowser.SaveFile("Floats", "txt");

         if (!string.IsNullOrEmpty(path))
            Util.Helper.SaveAsText(path, TRManager.Instance.CurrentFloats);
#else
         Debug.LogWarning("'File Browser' package not installed! Please install it to use this function: " + Util.Constants.ASSET_FB, this);
#endif
      }

      #endregion


      #region Callbacks

      private void onGenerateFloatStart(string id)
      {
         if (Util.Config.DEBUG)
            Debug.Log("onGenerateFloatStart: " + id, this);
      }

      private void onGenerateFloatFinished(List<float> e, string id)
      {
         if (Util.Config.DEBUG)
            Debug.Log("onGenerateFloatFinished: " + id, this);

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