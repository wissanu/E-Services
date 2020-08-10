'use strict';

/* Filter */

var eComFilters = angular.module('eComFilter', []);

eComFilters.filter('abs', function ($window) {
    return function (n) {
        return $window.Math.abs($window.Number(n));
    };
});

/*
 * Ref
 * http://stackoverflow.com/questions/19415394/with-ng-bind-html-unsafe-removed-how-do-i-inject-html
 * http://stackoverflow.com/questions/16227325/how-do-i-call-an-angular-js-filter-with-multiple-arguments
 * http://stackoverflow.com/questions/149055/how-can-i-format-numbers-as-money-in-javascript
 * http://plnkr.co/edit/6X2491DK4kqj8OEAAB9B?p=preview
 */
eComFilters.filter('eComCurrency', function ($sce) {
    return function (amount, config) {
        config = {
            Currency: typeof config.Currency == "string" ? config.Currency : "THB",
            isLocal: typeof config.isLocal == "boolean" ? config.isLocal : true
        }

        var Currency = function (currency, formatString, symbol, symbolLocal) {
            this.Name = currency;
            this.Symbol = symbol;
            this.SymbolLocal = symbolLocal
            this.FormatString = formatString;
        };
        Currency.prototype.getCurrencyAndFormatString = function () {
            var formatString = this.FormatString;
            var formatStringText = formatString.toString().replace("$symbol", this.Symbol);
            var amountString = parseFloat(amount).toFixed(2).toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
            formatStringText = formatStringText.toString().replace("$amount", amountString);
            return formatStringText;
        };
        Currency.prototype.getCurrencyAndFormatStringLocal = function () {
            var formatString = this.FormatString;
            var formatStringText = formatString.toString().replace("$symbol", this.SymbolLocal);
            var amountString = parseFloat(amount).toFixed(2).toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
            formatStringText = formatStringText.toString().replace("$amount", amountString);
            return formatStringText;
        };

        var currencies = [];
        currencies.push(new Currency("USD", "<span>$symbol</span>&nbsp;<span>$amount</span>", "$", "USD"));
        currencies.push(new Currency("THB", "<span>$amount</span>&nbsp;<span>$symbol</span>", "฿", "บาท"));

        var selectCurrency;
        angular.forEach(currencies, function (currency) {
            if (currency.Name == config.Currency) {
                selectCurrency = currency;
            }
        });

        if (config.isLocal) {
            return $sce.trustAsHtml(selectCurrency.getCurrencyAndFormatStringLocal());
        } else if (!config.isLocal) {
            return $sce.trustAsHtml(selectCurrency.getCurrencyAndFormatString());
        }
    };
});

eComFilters.filter("sanitize", ['$sce', function ($sce) {
    return function (htmlCode) {
        return $sce.trustAsHtml(htmlCode);
    }
}]);