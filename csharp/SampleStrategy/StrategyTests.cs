namespace SampleStrategy
{
    using BlackBox;
    using System;
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using SampleStrategyStrategy;
    using System.Diagnostics;
    [TestClass]
    public class StrategyTests
    {
        // Maybe some tests here to check if your strategy is any good?

        private decimal[] _upPrices = new decimal[] { 5, 5.3M, 5.6M, 5.7M, 5.7M, 5.8M, 6M, 10M, 12M, 14M, 50M };
        private decimal[] _downPrices = new decimal[] { 50M, 14M, 12M, 10M, 6M, 5.8M, 5.7M, 5.7M, 5.6M, 5.3M, 5M };
        private decimal[] _rollerCoaster = new decimal[] { 12M, 8M, 8.6M, 9M, 6M, 5.8M, 11.7M, 2.7M, 3.6M, 4.3M, 5M, 12M, 13M, 14M, 15M, 12M, 10M };
        private decimal[] _longPriceList = new decimal[] { 9, 6, 39, 36, 25, 31, 37, 17, 34, 21, 27, 45, 26, 22, 50, 34, 38, 52, 37, 45, 51, 28, 57, 54, 48, 57, 56, 44, 56, 60, 69, 37, 57, 72, 68, 55, 44, 46, 52, 47, 46, 42, 80, 70, 49, 47, 70, 54, 70, 88, 64, 61, 88, 87, 70, 57, 65, 63, 68, 87, 95, 76, 83, 98, 77, 76, 76, 81, 82, 86, 82, 85, 93, 85, 95, 106, 84, 86, 105, 101, 89, 105, 116, 111, 102, 114, 95, 117, 112, 121, 101, 93, 130, 118, 106, 126, 115, 101, 112, 132, 132, 111, 134, 109, 132, 144, 134, 132, 122, 133, 143, 150, 131, 141, 134, 126, 136, 149, 136, 142, 123, 140, 152, 144, 131, 135, 141, 163, 167, 158, 143, 136, 167, 166, 163, 174, 168, 166, 139, 157, 142, 165, 159, 182, 182, 156, 168, 158, 154, 151, 162, 177, 178, 184, 185, 168, 176, 168, 197, 177, 185, 193, 187, 187, 176, 194, 171, 192, 206, 190, 189, 201, 204, 212, 195, 193, 179, 208, 200, 214, 207, 186, 213, 221, 201, 211, 204, 191, 191, 206, 209, 212, 221, 215, 227, 205, 235, 229, 225, 209, 206, 237, 215, 228, 226, 226, 240, 234, 218, 222, 221, 239, 240, 217, 244, 236, 217, 243, 226, 245, 232, 223, 245, 245, 226, 262, 227, 237, 235, 241, 249, 241, 237, 246, 264, 265, 267, 244, 255, 275, 271, 272, 275, 273, 250, 276, 279, 273, 280, 262, 259, 265, 277, 254, 291, 260, 270, 288, 281, 284, 280, 285, 282, 300, 298, 266, 290, 282, 282, 308, 299, 305, 282, 284, 308, 296, 284, 309, 294, 290, 284, 296, 300, 317, 288, 290, 311, 291, 304, 315, 306, 292, 315, 324, 310, 302, 307, 332, 334, 305, 307, 319, 314, 335, 327, 320, 311, 321, 314, 325, 346, 333, 327, 326, 335, 321, 323, 342, 352, 348, 336, 342, 361, 351, 347, 353, 337, 345, 363, 356, 340, 343, 368, 344, 338, 361, 339, 362, 349, 367, 341, 352, 379, 382, 373, 379, 371, 358, 358, 360, 351, 376, 353, 361, 383, 381, 375, 381, 367, 394, 364, 387, 381, 382, 370, 341, 338, 358, 353, 326, 357, 338, 343, 350, 328, 349, 331, 314, 314, 322, 347, 348, 340, 323, 319, 320, 332, 314, 321, 323, 333, 320, 322, 322, 312, 321, 303, 314, 299, 325, 321, 308, 326, 313, 317, 306, 313, 289, 303, 298, 316, 300, 293, 297, 283, 311, 296, 278, 281, 290, 297, 273, 285, 276, 277, 281, 298, 264, 280, 287, 286, 295, 292, 264, 276, 273, 255, 259, 276, 282, 275, 272, 278, 249, 271, 258, 251, 247, 260, 260, 244, 243, 251, 264, 243, 263, 237, 263, 247, 246, 256, 258, 255, 261, 256, 226, 250, 231, 261, 249, 256, 235, 239, 234, 249, 220, 227, 228, 235, 249, 220, 240, 231, 246, 228, 227, 228, 210, 241, 233, 216, 209, 229, 206, 202, 231, 229, 228, 203, 216, 216, 228, 193, 216, 208, 213, 207, 218, 216, 184, 195, 207, 186, 193, 182, 210, 180, 201, 185, 180, 187, 189, 176, 175, 179, 191, 188, 175, 174, 197, 169, 177, 162, 195, 168, 165, 180, 186, 169, 185, 188, 177, 160, 184, 163, 173, 147, 158, 146, 151, 170, 147, 161, 140, 144, 142, 165, 169, 149, 167, 134, 141, 152, 164, 160, 162, 148, 132, 127, 156, 129, 132, 129, 135, 126, 144, 122, 124, 142, 135, 133, 118, 116, 109, 112, 106, 138, 104, 132, 107, 118, 128, 115, 118, 126, 122, 98, 108, 95, 111, 94, 122, 99, 95, 87, 103, 120, 88, 114, 100, 118, 100, 85, 109, 89, 109, 78, 95, 104, 104, 73, 92, 86, 105, 83, 84, 99, 93, 90, 84, 85, 92, 66, 67, 77, 84, 92, 54, 53, 78, 54, 78, 57, 59, 53, 82, 67, 68, 50, 76, 63, 61, 60, 47, 53, 56, 50, 46, 62, 53, 53, 36, 45, 37, 59, 31, 37, 59, 34, 44, 56, 23, 40, 22, 45, 29, 29, 19, 44, 31, 46, 20, 30, 39, 45, 9, 15, 41, 35, 40, 14, 25, 8, 16, 1, 32, 6, 31, 11, 1, 14, 15, 2, 12, 24, 5, 23, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        
        [TestMethod]
        public void CanRunStrategy()
        {
            IStrategy strategy = new Strategy();
            var result = strategy.Run(10);
        }

        [TestMethod]
        public void StrategyBuysFirstWhenMarketUp()
        {
            IStrategy strategy = new Strategy();

            Assert.IsTrue(BuysFirst(strategy, _upPrices));
        }

        [TestMethod]
        public void RealTest()
        {
            Runner.run();
        }

        [TestMethod]
        public void StrategyBuysFirstWhenMarketDown()
        {
            IStrategy strategy = new Strategy();

            Assert.IsTrue(BuysFirst(strategy, _downPrices));
        }

        [TestMethod]
        public void StrategyBuysFirstWhenMarketRollerCoaster()
        {
            IStrategy strategy = new Strategy();

            Assert.IsTrue(BuysFirst(strategy, _longPriceList));
        }

        [TestMethod]
        public void StrategyBuysAndSellsAtLeastOnce()
        {
            IStrategy strategy = new Strategy();
            List<TradeAction> actions = new List<TradeAction>();

            foreach (var price in _longPriceList)
            {
                actions.Add(strategy.Run(price));
            }

            Assert.IsTrue(actions.Contains(TradeAction.Buy));
            Assert.IsTrue(actions.Contains(TradeAction.Sell));
            Assert.IsTrue(actions.IndexOf(TradeAction.Buy) < actions.LastIndexOf(TradeAction.Sell));
        }

        private bool BuysFirst(IStrategy strategy, decimal[] prices)
        {
            foreach (var price in prices)
            {
                var result = strategy.Run(price);
                switch (result)
                {
                    case TradeAction.DoNothing:
                        break;
                    case TradeAction.Buy:
                        return true;
                    case TradeAction.Sell:
                        return false;
                    default:
                        break;
                }
            }

            return true;
        }
    }
}
