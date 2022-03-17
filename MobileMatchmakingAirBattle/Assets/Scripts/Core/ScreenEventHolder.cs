using Core.Base;
using Enums;
using PlayFab;

namespace Core
{
    //TODO: Переместить. Класс удалить
    public class ScreenEventHolder : BaseInstance<ScreenEventHolder>
    {
        public void UnexpectedErrorUI(PlayFabError err) =>
            UnexpectedErrorUI(err.ErrorMessage);

        private void UnexpectedErrorUI(string err) =>
            PopupHolder.CurrentPopup(PopupType.UnexpectedError).Config(err).Show();
    }
}