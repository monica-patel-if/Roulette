﻿#if UNITY_EDITOR
using UnityEditor;

namespace Crosstales.TrueRandom.EditorTask
{
   /// <summary>Adds the given define symbols to PlayerSettings define symbols.</summary>
   [InitializeOnLoad]
   public class CompileDefines : Common.EditorTask.BaseCompileDefines
   {
      private const string symbol = "CT_TR";

      static CompileDefines()
      {
         if (EditorUtil.EditorConfig.COMPILE_DEFINES)
         {
            addSymbolsToAllTargets(symbol);
         }
         else
         {
            removeSymbolsFromAllTargets(symbol);
         }
      }
   }
}
#endif
// © 2017-2020 crosstales LLC (https://www.crosstales.com)