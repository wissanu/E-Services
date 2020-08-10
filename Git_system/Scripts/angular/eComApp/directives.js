'use strict';

/* Directives */

var eComDirectives = angular.module('eComDirectives', []);

eComDirectives.directive('productThumbnail', [
    function () {
        return {
            restrict: 'A',
            scope: {
                product: '=',
                mainUrl: '='
            },
            templateUrl: '../Scripts/angular/eComApp/views/productThumbnail.html'
        }
    }
]);

eComDirectives.directive('productShow', ['Category_CRUD_API',
    function (Category_CRUD_API) {
        return {
            restrict: 'A',
            template: '<div ng-repeat-start="product in products" class="col-xs-24 col-sm-12 col-md-6">' +
                        '   <div product-thumbnail product="product" mainUrl="' + defineMainUrl + '">' +
                        '   </div>' +
                        '</div>' +
                        '<div ng-repeat-end>' +
                        '   <div class="clearfix hidden-sm hidden-xs" ng-if="((($index+1) % 4) == 0) && ($index != 0)"></div>' +
                        '   <div class="clearfix visible-sm" ng-if="((($index+1) % 2) == 0) && ($index != 0)"></div>' +
                        '</div>',
            link: function (scope, el, attrs) {
                var limit = 0;
                if (attrs.limit != void 0)
                    if (attrs.limit > 0)
                        limit = attrs.limit;
                Category_CRUD_API.get({ id: attrs.category, limit: limit },
                    function success(response) {
                        angular.forEach(response.data, function (product) {
                            var Stock = function () { this._data = product.Stock }
                            Stock.prototype.getInStock = function () {
                                return this._data.InStock;
                            }
                            Stock.prototype.getQuantity = function () {
                                return this._data.Quantity;
                            }
                            Stock.prototype.CheckStatus = function () {
                                if (this.getInStock() == false)
                                    return true;
                                else if ((this.getInStock() == true) && (this.getQuantity() > 0))
                                    return true;
                                else
                                    return false;
                            }
                            var rProduct = (product.Stock._data === void 0) ? product["Stock"] = new Stock : product;
                            return rProduct;
                        });
                        scope.products = response.data;
                        console.log(scope.products);
                    },
                    function error(errorResponse) {
                        console.error("Error:" + angular.toJson(errorResponse));
                    });
            }
        };
    }]);

eComDirectives.directive('productShowMd8',
    function () {
        return {
            restrict: 'A',
            template: '<div ng-repeat-start="product in products" class="col-xs-24 col-sm-12 col-md-8">' +
                        '   <div product-thumbnail product="product" mainUrl="' + defineMainUrl + '">' +
                        '   </div>' +
                        '</div>' +
                        '<div ng-repeat-end>' +
                        '   <div class="clearfix hidden-sm hidden-xs" ng-if="((($index+1) % 3) == 0) && ($index != 0)"></div>' +
                        '   <div class="clearfix visible-sm" ng-if="((($index+1) % 2) == 0) && ($index != 0)"></div>' +
                        '</div>',
            link: function (scope, element, attrs) {
                scope.$watch(attrs.productShowMd8, function (value) {
                    //scope.products = value;
                    console.log(value);
                    angular.forEach(value, function (product) {
                        var Stock = function () {
                            this._data = product.Stock
                        }
                        Stock.prototype.getInStock = function () {
                            return this._data.InStock;
                        }
                        Stock.prototype.getQuantity = function () {
                            return this._data.Quantity;
                        }
                        Stock.prototype.CheckStatus = function () {
                            if (this.getInStock() == false)
                                return true;
                            else if ((this.getInStock() == true) && (this.getQuantity() > 0))
                                return true;
                            else
                                return false;
                        }
                        var rProduct = (product.Stock._data === void 0) ? product["Stock"] = new Stock : product;
                        return rProduct;
                    });
                    scope.products = value;
                    console.log(scope.products);
                });
            }
        };
    });

eComDirectives.directive('fileModel', ['$parse',
    function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var model = $parse(attrs.fileModel);
                var modelSetter = model.assign;

                element.bind('change', function () {
                    scope.$apply(function () {
                        modelSetter(scope, element[0].files[0]);
                    });
                });
            }
        };
    }]);

eComDirectives.directive('ngBlurModel', ['$parse',
    function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var model = $parse(attrs.ngBlurModel);
                var modelSetter = model.assign;

                element.bind('blur', function () {
                    scope.$apply(function () {
                        modelSetter(scope, element[0].value);
                        console.log(element[0].value);
                    });
                });
            }
        };
    }]);