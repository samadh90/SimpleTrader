﻿using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.Commands
{
    public class SearchSymbolCommand : AsyncCommandBase
    {
        private readonly BuyViewModel _viewModel;
        private readonly IStockPriceService _stockPriceService;

        public SearchSymbolCommand(BuyViewModel viewModel, IStockPriceService stockPriceService)
        {
            _viewModel = viewModel;
            _stockPriceService = stockPriceService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                double stockPrice = await _stockPriceService.GetPrice(_viewModel.Symbol);
                _viewModel.SearchResultSymbol = _viewModel.Symbol.ToUpper();
                _viewModel.StockPrice = stockPrice;
            }
            catch (InvalidSymbolException)
            {
                _viewModel.ErrorMessage = "Symbol does not exist";
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Failed to get symbol.";
            }
        }
    }
}
