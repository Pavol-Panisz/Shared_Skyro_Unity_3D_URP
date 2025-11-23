using Game.Items;
using Game.UI;

namespace Game.Installers
{
    public class ItemsInstaller
    {
        public IItemsService Install(IItemsView view)
        {
            return new ItemsService(
                (name, value) => view.AddUI(name, value),
                (name, value) => view.ChangeUI(name, value),
                (name) => view.RemoveUI(name)
            );
        }
    }
}