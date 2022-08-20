using System;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy.Model
{
    public class ShopModel : IDisposable
    {
        private readonly PlayerData _data;
        private ShopDefinition _shopData;

        public event Action OnChanged;

        public readonly StringObservableProperty InterfaceSelectedItem = new StringObservableProperty();

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        public ShopModel(PlayerData data)
        {
            _data = data;
            InterfaceSelectedItem.Value = DefinitionsFacade.I.Shop.All[0].Id;

            _trash.Retain(InterfaceSelectedItem.Subscribe((x, y) => OnChanged?.Invoke()));
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public void Buy(string id)
        {
            var def = DefinitionsFacade.I.Shop.Get(id);
            var isEnoughResources = _data.Inventory.IsEnough(def.Price);

            var currentAmount = GetCurrentAmount(id);
            if(currentAmount <= 0) return;

            if (isEnoughResources)
            {
                _data.Shops.DecreaseAmount(id);          
                _data.Inventory.Remove(def.Price.ItemId, def.Price.Count);
                _data.Inventory.Add(def.Id, 1);
            }
            OnChanged?.Invoke();
        }

        public bool CanBuy(string id)
        {
            var def = DefinitionsFacade.I.Shop.Get(id);
            return _data.Inventory.IsEnough(def.Price);
        }        

        public int GetCurrentAmount(string id) => _data.Shops.GetAmount(id);

        public void Dispose()
        {
            _trash.Dispose();
        }
    }

}
