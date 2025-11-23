namespace Game.Popup
{
    public interface IPopupService
    {
        void OnAddButton();
        void OnRemoveButton();
        void OnClose();
        void OnAction();
    }

    public interface IPopupView
    {
        string GetName();
        string GetAmount();
        PopupVersion GetVersion();

        void SetVersion(PopupVersion version);
        void SetEnabled(bool enabled);
    }

    public interface IPopupControllerView
    {
        public void Initialize(IPopupService service);
        void OnAddButtonPressed();
        void OnRemoveButtonPressed();
        void OnCloseButtonPressed();
        void OnActionButtonPressed();
    }
}