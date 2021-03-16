using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.TrueRandom.Demo
{
   /// <summary>Generate random Vector3.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/truerandom/api/class_crosstales_1_1_true_random_1_1_demo_1_1_generate_vector3.html")]
   public class GenerateVector3 : MonoBehaviour
   {
      #region Variables

      public GameObject TextPrefab;

      public GameObject ScrollView;

      public InputField Number;
      public InputField MinX;
      public InputField MinY;
      public InputField MinZ;
      public InputField MaxX;
      public InputField MaxY;
      public InputField MaxZ;

      public Text Error;
      public Text Quota;

      public Button ButtonSave;

      #endregion


      #region MonoBehaviour methods

      private void OnEnable()
      {
         TRManager.Instance.OnGenerateVector3Start += onGenerateVector3Start;
         TRManager.Instance.OnGenerateVector3Finished += onGenerateVector3Finished;
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
         TRManager.Instance.OnGenerateVector3Start -= onGenerateVector3Start;
         TRManager.Instance.OnGenerateVector3Finished -= onGenerateVector3Finished;
         TRManager.Instance.OnQuotaUpdate -= onUpdateQuota;
         TRManager.Instance.OnErrorInfo -= onError;
      }

      #endregion


      #region Public methods

      public void GenerateVector3Numbers()
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
                        if (MinZ != null && float.TryParse(MinZ.text, out float minZ))
                        {
                           if (MaxZ != null && float.TryParse(MaxZ.text, out float maxZ))
                           {
                              TRManager.Instance.GenerateVector3(new Vector3(minX, minY, minZ), new Vector3(maxX, maxY, maxZ), number);
                           }
                           else
                           {
                              if (Error != null)
                                 Error.text = "'Max Z value' is not a number!";
                           }
                        }
                        else
                        {
                           if (Error != null)
                              Error.text = "'Min Z value' is not a number!";
                        }
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
               Error.text = "'Number of Vector3' is not a number!";
         }
      }

      public void SaveFile()
      {
#if CT_FB
         string path = FB.FileBrowser.SaveFile("Vector3", "txt");

         if (!string.IsNullOrEmpty(path))
            Util.Helper.SaveAsText(path, TRManager.Instance.CurrentVector3);
#else
         Debug.LogWarning("'File Browser' package not installed! Please install it to use this function: " + Util.Constants.ASSET_FB, this);
#endif
      }

      #endregion


      #region Callbacks

      private void onGenerateVector3Start(string id)
      {
         if (Util.Config.DEBUG)
            Debug.Log("onGenerateVector3Start: " + id, this);
      }

      private void onGenerateVector3Finished(List<Vector3> e, string id)
      {
         if (Util.Config.DEBUG)
            Debug.Log("onGenerateVector3Finished: " + id, this);

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
            go.GetComponent<Text>().text = e[ii].x + ", " + e[ii].y + ", " + e[ii].z;
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