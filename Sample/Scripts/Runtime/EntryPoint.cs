﻿using mvvm.unity.Core;
using System.Collections.Generic;
using UnityEngine;

namespace mvvm.unity.Samples
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private View _coinsView;

        private ViewsManager<SampleViewType> _viewsManager;

        private async void Start()
        {
            var viewsMap = new Dictionary<SampleViewType, IView>()
            {
                { SampleViewType.Coin, _coinsView },
            };

            var viewsProvider = new ViewsProvider<SampleViewType>(viewsMap);
            _viewsManager = new ViewsManager<SampleViewType>(viewsProvider);

            var coinCounterViewModel = new CoinCounterViewModel(new CoinsCounterModel());
            await _viewsManager.BindAndShowAsync(SampleViewType.Coin, coinCounterViewModel);
        }

        private async void OnDestroy()
        {
            if (_viewsManager == null)
                return;

            await _viewsManager.HideAndUnbindAsync(SampleViewType.Coin);
        }
    }
}