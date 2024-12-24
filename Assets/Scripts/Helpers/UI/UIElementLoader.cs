using UnityEngine;

namespace Helpers
{
   public class UIElementLoader : MonoBehaviour
   {
      private const string SuccessPopupPath = "UI/SuccessInfoPopup";
      private const string FailPopupPath = "UI/FailInfoPopup";
      
      public void LoadPopup(bool isSuccess, Transform parent)
      {
         var path = isSuccess ? SuccessPopupPath : FailPopupPath;
         var popup = Resources.Load<GameObject>(path);
         popup.transform.SetParent(parent);
      }
   }
}
