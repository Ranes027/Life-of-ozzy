using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using LifeOfOzzy.Utils;


namespace LifeOfOzzy.Model
{
    public class ShopModel : IDisposable
    {
        private readonly PlayerData _data;
        public readonly StringObservableProperty InterfaceSelectedItem = new StringObservableProperty();

        public event Action OnChanged;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        public ShopModel(PlayerData data)
        {
            _data = data;
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

            if (isEnoughResources)
            {
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

        

        public void Dispose()
        {
            _trash.Dispose();
        }
    }

}
