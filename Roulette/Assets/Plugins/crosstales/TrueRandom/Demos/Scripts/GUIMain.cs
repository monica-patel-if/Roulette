﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Crosstales.TrueRandom.Demo
{
   /// <summary>Main GUI component for all demo scenes.</summary>
   [HelpURL("https://www.crosstales.com/media/data/assets/truerandom/api/class_crosstales_1_1_true_random_1_1_demo_1_1_g_u_i_main.html")]
   public class GUIMain : MonoBehaviour
   {
      #region Variables

      public Text Name;
      public Text Version;
      public Text Scene;

      #endregion


      #region MonoBehaviour methods

      private void Start()
      {
         if (Name != null)
            Name.text = Util.Constants.ASSET_NAME;

         if (Version != null)
            Version.text = Util.Constants.ASSET_VERSION;

         if (Scene != null)
            Scene.text = SceneManager.GetActiveScene().name;
      }

      #endregion


      #region Public methods

      public void OpenAssetURL()
      {
         Util.Helper.OpenURL(Util.Constants.ASSET_CT_URL);
      }

      public void OpenCTURL()
      {
         Util.Helper.OpenURL(Util.Constants.ASSET_AUTHOR_URL);
      }

      public void Quit()
      {
         if (Application.isEditor)
         {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
         }
         else
         {
            Application.Quit();
         }
      }

      #endregion
   }
}
// © 2016-2020 crosstales LLC (https://www.crosstales.com)