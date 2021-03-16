using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.TrueRandom.Demo
{
   /// <summary>Generate random Vector2.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/truerandom/api/class_crosstales_1_1_true_random_1_1_demo_1_1_generate_vector2.html")]
   public class GenerateVector2 : MonoBehaviour
   {
      #region Variables

      public GameObject TextPrefab;

      public GameObject ScrollView;

      public InputField Number;
      public InputField MinX;
      public InputField MinY;
      public InputField MaxX;
      public InputField MaxY;

      public Text Error;
      public Text Quota;

      public Button ButtonSave;

      #endregion


      #region MonoBehaviour methods

      private void OnEnable()
      {
         TRManager.Instance.OnGenerateVector2Start += onGenerateVector2Start;
         TRManager.Instance.OnGenerateVector2Finished += onGenerateVector2Finished;
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
         TRManager.Instance.OnGenerateVector2Start -= onGenerateVector2Start;
         TRManager.Instance.OnGenerateVector2Finished -= onGenerateVector2Finished;
         TRManager.Instance.OnQuotaUpdate -= onUpdateQuota;
         TRManager.Instance.OnErrorInfo -= onError;
      }

      #endregion


      #region Public methods

      public void GenerateVector2Numbers()
      {
         if (Error != null)
            Error.text = string.Empty;

         if (Number != null && int.TryParse(Number.text, out int number))
         {
            if (MinX != null && float.TryParse(MinX.text, out float minX))
            {
               if (MaxX != null && float.TryParse(MaxX.text, out float maxX))
               {
                  if (MinY != null && float.TryParse(MinY.text, out float minY))
                  {
                     if (MaxY != null && float.TryParse(MaxY.text, out float maxY))
                     {
                        TRManager.Instance.GenerateVector2(new Vector2(minX, minY), new Vector2(maxX, maxY), number);
                     }
                     else
                     {
                        if (Error != null)
                           Error.text = "'Max Y value' is not a number!";
                     }
                  }
                  else
                  {
                     if (Error != null)
                        Error.text = "'Min Y value' is not a number!";
                  }
               }
               else
               {
                  if (Error != null)
                     Error.text = "'Max X value' is not a number!";
               }
            }
            else
            {
               if (Error != null)
                  Error.text = "'Min X value' is not a number!";
            }
         }
         else
         {
            if (Error != null)
               Error.text = "'Number of Vector2' is not a number!";
         }
      }

      public void SaveFile()
      {
#if CT_FB
         string path = FB.FileBrowser.SaveFile("Vector2", "txt");

         if (!string.IsNullOrEmpty(path))
            Util.Helper.SaveAsText(path, TRManager.Instance.CurrentVector2);
#else
         Debug.LogWarning("'File Browser' package not installed! Please install it to use this function: " + Util.Constants.ASSET_FB, this);
#endif
      }

      #endregion


      #region Callbacks

      private void onGenerateVector2Start(string id)
      {
         if (Util.Config.DEBUG)
            Debug.Log("onGenerateVector2Start: " + id, this);
      }

      private void onGenerateVector2Finished(List<Vector2> e, string id)
      {
         if (Util.Config.DEBUG)
            Debug.Log("onGenerateVector2Finished: " + id, this);

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
            go.GetComponent<Text>().text = e[ii].x + ", " + e[ii].y;
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
// © 2017-2020 crosstales LLC (https://www.crosstales.com)