﻿/*
MIT LICENSE

Copyright 2017 Digital Ruby, LLC - http://www.digitalruby.com

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace ExchangeSharp
{
    public static partial class ExchangeSharpConsole
    {
        public static void RunShowExchangeStats(Dictionary<string, string> dict)
        {
            string symbol = "XRP";

            List<IExchangeAPI> apis = new List<IExchangeAPI>();
            apis.Add(new ExchangeGdaxAPI());
            apis.Add(new ExchangeGeminiAPI());
            apis.Add(new ExchangeKrakenAPI());
            apis.Add(new ExchangeBitfinexAPI());
            apis.Add(new ExchangeBithumbAPI());

            while (true)
            {
                StringBuilder builder = new StringBuilder();

                foreach (IExchangeAPI api in apis)
                {
                    try
                    {
                        ExchangeTicker ticker = api.GetTicker(symbol);
                        ExchangeOrderBook orders = api.GetOrderBook(symbol);

                        decimal askAmountSum = orders.Asks.Sum(o => o.Amount);
                        decimal askPriceSum = orders.Asks.Sum(o => o.Price);
                        decimal bidAmountSum = orders.Bids.Sum(o => o.Amount);
                        decimal bidPriceSum = orders.Bids.Sum(o => o.Price);
                        
                        string line = String.Format("{0}\n\t LastPrice: {1:0.00}\n\t PriceAmount: {2:0.00}\n\t AskAmountSum: {3:0.00}\n\t AskPriceSum: {4:0.00}\n\t BidAmountSum: {5:0.00}\n\t BidPriceSum: {6:0.00}", api.Name, ticker.Last, ticker.Volume.PriceAmount, askAmountSum, askPriceSum, bidAmountSum, bidPriceSum);

                        builder.AppendLine(line);

                    }catch(Exception e)
                    {
                    }
                }

                Console.Clear();
                Console.Write(builder.ToString());

                Thread.Sleep(5000);
            }
        }
    }
}
