using UnityEngine;

namespace Game.Popup
{
    public class PopupControllerView : MonoBehaviour, IPopupControllerView
    {
        private IPopupService _service;

        public void Initialize(IPopupService service)
        {
            _service = service;
        }

        public void OnAddButtonPressed()    => _service.OnAddButton();
        public void OnRemoveButtonPressed() => _service.OnRemoveButton();
        public void OnCloseButtonPressed()  => _service.OnClose();
        public void OnActionButtonPressed() => _service.OnAction();
    }
}