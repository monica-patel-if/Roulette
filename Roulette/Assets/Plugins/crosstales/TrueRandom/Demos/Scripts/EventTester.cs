using UnityEngine;

namespace Crosstales.TrueRandom.Demo
{
   /// <summary>Simple test script for all UnityEvent-callbacks.</summary>
   [ExecuteInEditMode]
   [HelpURL("https://www.crosstales.com/media/data/assets/truerandom/api/class_crosstales_1_1_true_random_1_1_demo_1_1_event_tester.html")]
   public class EventTester : MonoBehaviour
   {
      public void GenerateComplete(string id, string type)
      {
         Debug.Log("GenerateComplete: " + id + " - " + type);

         switch (type)
         {
            case "int":
               Debug.Log(TRManager.Instance.AllIntegerResults[id].CTDump());
               break;
            case "float":
               Debug.Log(TRManager.Instance.AllFloatResults[id].CTDump());
               break;
            case "sequence":
               Debug.Log(TRManager.Instance.AllSequenceResults[id].CTDump());
               break;
            case "string":
               Debug.Log(TRManager.Instance.AllStringResults[id].CTDump());
               break;
            case "Vector2":
               Debug.Log(TRManager.Instance.AllVector2Results[id].CTDump());
               break;
            case "Vector3":
               Debug.Log(TRManager.Instance.AllVector3Results[id].CTDump());
               break;
            default: //case "Vector4":
               Debug.Log(TRManager.Instance.AllVector4Results[id].CTDump());
               break;
         }
      }

      public void OnQuotaUpdate(int quota)
      {
         Debug.Log("OnQuotaUpdate: " + quota);
      }

      public void OnError(string info)
      {
         Debug.LogWarning("OnError: " + info);
      }
   }
}
// © 2020 crosstales LLC (https://www.crosstales.com)