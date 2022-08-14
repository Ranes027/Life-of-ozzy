using System;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy.Model
{
    public class PerksModel : IDisposable
    {
        private readonly PlayerData _data;
        public readonly StringObservableProperty InterfaceSelection = new StringObservableProperty();

        public event Action OnChanged;

        public string Used => _data.Perks.Used.Value;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        public bool IsJumpBuffSupported => _data.Perks.Used.Value == "japan_soda";
        public bool IsHealBuffSupported => _data.Perks.Used.Value == "japan_tea";
        public bool IsDamageBuffSupported => _data.Perks.Used.Value == "japan_beer";       

        public PerksModel(PlayerData data)
        {
            _data = data;
            InterfaceSelection.Value = DefinitionsFacade.I.Perks.All[0].Id;

            _trash.Retain(_data.Perks.Used.Subscribe((x, y) => OnChanged?.Invoke()));
            _trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }        

        public void Unlock(string id)
        {
            var def = DefinitionsFacade.I.Perks.Get(id);
            var isEnoughResources = _data.Inventory.IsEnough(def.Price);

            if (isEnoughResources)
            {
                _data.Inventory.Remove(def.Price.ItemId, def.Price.Count);
                _data.Perks.AddPerk(id);
            }
            OnChanged?.Invoke();
        }

        public void UsePerk(string selected)
        {
            _data.Perks.Used.Value = selected;
        }

        public bool IsUsed(string perkId)
        {
            return _data.Perks.Used.Value == perkId;
        }

        public bool IsUnlocked(string perkId)
        {
            return _data.Perks.IsUnlocked(perkId);
        }

        public bool CanBuy(string perkId)
        {
            var def = DefinitionsFacade.I.Perks.Get(perkId);
            return _data.Inventory.IsEnough(def.Price);
        }

        public void Dispose()
        {
            _trash.Dispose();
        }
    }

}
