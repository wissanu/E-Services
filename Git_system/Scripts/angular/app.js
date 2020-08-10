Array.prototype.sum = function (prop) {
    var total = 0
    for (var i = 0, _len = this.length; i < _len; i++) {
        total += this[i][prop]
    }
    return total
}

var app = angular.module('Product', []);
app.factory('apiFactory', function ($http, $q, $location) {
    return {
        getProductDetailById: function (productId) {
            var deferred = $q.defer(); //เริ่มทำงาน
            $http.get("/EComAPI/Product?id=" + productId)
                .success(function (response) {
                    deferred.resolve(response.data) //เมื่อทำงานจนจบโปรเซส ให้ทำการส่งค่าไป โดยใช้ฟังก์ชั่น then() ในการรับ
                });
            return deferred.promise; //กำหนดให้ทำการรอจนกว่าจะจบโปรเซส
        },
        getProductRelatedItem: function (productId) {
            var deferred = $q.defer();
            $http.get("/EComAPI/RelatedItem?id=" + productId)
                .success(function (response) {
                    deferred.resolve(response.data)
                });
            return deferred.promise;
        },
        getProductNewArrival: function () {
            var deferred = $q.defer();
            $http.get("/EComAPI/NewArrival")
                .success(function (response) {
                    deferred.resolve(response.data)
                });
            return deferred.promise;
        },
        getProductHotDeals: function () {
            var deferred = $q.defer();
            $http.get("/EComAPI/HotDeals")
                .success(function (response) {
                    deferred.resolve(response.data)
                });
            return deferred.promise;
        },
        getProductBestSeller: function () {
            var deferred = $q.defer();
            $http.get("/EComAPI/BestSeller")
                .success(function (response) {
                    deferred.resolve(response.data)
                });
            return deferred.promise;
        },
        getProductInCategory: function (categoryId) {
            var deferred = $q.defer();
            $http.get("/EComAPI/Product?Category=" + categoryId)
                .success(function (response) {
                    deferred.resolve(response.data)
                });
            return deferred.promise;
        },
        getCategoryList: function () {
            var deferred = $q.defer();
            $http.get("/EComAPI/Category")
                .success(function (response) {
                    deferred.resolve(response.data)
                });
            return deferred.promise;
        },
        getProductId: function () {
            var deferred = $q.defer();
            deferred.resolve($location.search().ProductId);
            return deferred.promise;
        }
    };
});
app.run(['$rootScope', 'ngCart', 'ngCartItem', 'store', 'apiFactory', function ($rootScope, ngCart, ngCartItem, store, apiFactory) {
    $rootScope.$on('ngCart:change', function () {
        ngCart.$save();
        $rootScope.$broadcast('changeQuantity', {});
        $rootScope.$broadcast('checkCartList', {});
    });

    $rootScope.$on('changeQuantity', function () {
        $rootScope.totalItems = ngCart.getTotalItems();
    });

    $rootScope.$on('checkCartList', function () {
        var allitem = {
            items: []
        };
        angular.forEach(ngCart.$cart.items, function (item) {
            apiFactory.getProductDetailById(item._id).then(function (data) {
                var object = {
                    Id: item._id,
                    Name: data.Name,
                    PricePerUnit: (data.Price),
                    Quantity: (item._quantity),
                    PriceTotal: (item._quantity * data.Price),
                    Images: data.ImageFiles[0].FilePath.replace('~', '')
                };
                allitem.items.push(object);
            });
        });
        $rootScope.shoppingCart = allitem;
    });

    if (angular.isObject(store.get('cart'))) {
        ngCart.$restore(store.get('cart'));
    } else {
        ngCart.init();
    }
}]);
app.service('ngCart', function ($rootScope, store, ngCartItem) {
    this.init = function () {
        this.$cart = {
            items: []
        };
    };

    this.addItem = function (id, quantity) {
        var inCart = this.getItemById(id);

        if (typeof inCart === 'object') {
            //Update quantity of an item if it's already in the cart
            inCart.setQuantity(quantity, false);
        } else {
            var newItem = new ngCartItem(id, quantity);
            this.$cart.items.push(newItem);
            $rootScope.$broadcast('ngCart:itemAdded', newItem);
        }

        $rootScope.$broadcast('ngCart:change', {});
    };

    this.getItemById = function (itemId) {
        var items = this.getCart().items;
        var build = false;

        angular.forEach(items, function (item) {
            if (item.getId() === itemId) {
                build = item;
            }
        });
        return build;
    };

    this.setCart = function (cart) {
        this.$cart = cart;
        return this.getCart();
    };

    this.getCart = function () {
        return this.$cart;
    };

    this.getItems = function () {
        return this.getCart().items;
    };

    this.getTotalItems = function () {
        var count = 0;
        var items = this.getItems();
        angular.forEach(items, function (item) {
            count += item.getQuantity();
        });
        return count;
    };

    this.getTotalUniqueItems = function () {
        return this.getCart().items.length;
    };

    this.removeItem = function (index) {
        this.$cart.items.splice(index, 1);
        $rootScope.$broadcast('ngCart:itemRemoved', {});
        $rootScope.$broadcast('ngCart:change', {});
    };

    this.removeItemById = function (id) {
        var cart = this.getCart();
        angular.forEach(cart.items, function (item, index) {
            if (item.getId() === id) {
                cart.items.splice(index, 1);
            }
        });
        this.setCart(cart);
        $rootScope.$broadcast('ngCart:itemRemoved', {});
        $rootScope.$broadcast('ngCart:change', {});
    };

    this.empty = function () {
        $rootScope.$broadcast('ngCart:change', {});
        this.$cart.items = [];
        localStorage.removeItem('cart');
    };

    this.isEmpty = function () {
        return (this.$cart.items.length > 0 ? false : true);
    };

    this.toObject = function () {
        if (this.getItems().length === 0) return false;

        var items = [];
        angular.forEach(this.getItems(), function (item) {
            items.push(item.toObject());
        });

        return {
            items: items
        }
    };

    this.$restore = function (storedCart) {
        var _self = this;
        _self.init();

        angular.forEach(storedCart.items, function (item) {
            _self.$cart.items.push(new ngCartItem(item._id, item._quantity));
        });
        this.$save();
    };

    this.$save = function () {
        return store.set('cart', JSON.stringify(this.getCart()));
    }
});
app.factory('ngCartItem', ['$rootScope', '$log', function ($rootScope, $log) {
    var item = function (id, quantity) {
        this.setId(id);
        this.setQuantity(quantity);
    };

    item.prototype.setId = function (id) {
        if (id) this._id = id;
        else {
            $log.error('An ID must be provided');
        }
    };
    item.prototype.getId = function () {
        return this._id;
    };

    item.prototype.setQuantity = function (quantity, relative) {
        var quantityInt = parseInt(quantity);
        if (quantityInt % 1 === 0) {
            if (relative === true) {
                this._quantity += quantityInt;
            } else {
                this._quantity = quantityInt;
            }
            if (this._quantity < 1) this._quantity = 1;
        } else {
            this._quantity = 1;
            $log.info('Quantity must be an integer and was defaulted to 1');
        }
        $rootScope.$broadcast('ngCart:change', {});
    };
    item.prototype.getQuantity = function () {
        return this._quantity;
    };

    return item;
}]);
app.service('store', ['$window', function ($window) {
    return {
        get: function (key) {
            if ($window.localStorage[key]) {
                var cart = angular.fromJson($window.localStorage[key]);
                return JSON.parse(cart);
            }
            return false;
        },

        set: function (key, val) {
            if (val === undefined) {
                $window.localStorage.removeItem(key);
            } else {
                $window.localStorage[key] = angular.toJson(val);
            }
            return $window.localStorage[key];
        }
    }
}]);

app.controller('ProductViewDetail', ["$rootScope", "$http", "$location", "apiFactory", "ngCart", function ($rootScope, $http, $location, apiFactory, ngCart) {
    //Product by Id
    $rootScope.productId = $location.search().ProductId;
    $rootScope.ngCart = ngCart;
    $rootScope.$broadcast('changeQuantity', {});
    var inCart = eval("ngCart.getItemById(" + $rootScope.productId + ")");
    $rootScope.product_quantity = 0;
    if (typeof inCart === 'object') {
        $rootScope.product_quantity = inCart.getQuantity();
    }

    $rootScope.product = [];
    apiFactory.getProductDetailById($rootScope.productId).then(function (data) { // รับค่าจาก deferred.resolve(data)
        $rootScope.product = data;
    });
    //RalateItem
    apiFactory.getProductRelatedItem($rootScope.productId).then(function (data) {
        $rootScope.relatedItem_Products = data;
    });

    //Change Product
    $rootScope.changeProduct = function (productId) {
        var inCart = eval("ngCart.getItemById(" + productId + ")");
        $rootScope.product_quantity = 0;
        if (typeof inCart === 'object') {
            $rootScope.product_quantity = inCart.getQuantity();
        }

        apiFactory.getProductDetailById(productId).then(function (data) {
            $rootScope.product = data;
        });
        apiFactory.getProductRelatedItem(productId).then(function (data) {
            $rootScope.relatedItem_Products = data;
        });
    };

    //All category list
    apiFactory.getCategoryList().then(function (data) {
        $rootScope.categorys = data;
    });
}]);

app.controller('ProductViewAll', ["$rootScope", "$http", "apiFactory", function ($rootScope, $http, apiFactory) {
    //ProductNewArrival
    apiFactory.getProductNewArrival().then(function (data) {
        $rootScope.productNewArrival = data;
    });

    //ProductHotDeals
    apiFactory.getProductHotDeals().then(function (data) {
        $rootScope.productHotDeals = data;
    });

    //ProductBestSeller
    apiFactory.getProductBestSeller().then(function (data) {
        $rootScope.productBestSeller = data;
    });

    //All category list
    apiFactory.getCategoryList().then(function (data) {
        $rootScope.categorys = data;
    });
}]);

app.controller('ProductCategoryView', ["$scope", "$http", "apiFactory", function ($scope, $http, apiFactory) {
    $scope.init = function (categoryId) {
        //This function is sort of private constructor for controller
        $scope.categoryId = categoryId;
        apiFactory.getProductInCategory($scope.categoryId).then(function (data) {
            $scope.productInCategory = data;
        });
    };
}]);

app.controller('ProductShoppingCart', ["$rootScope", "$http", "$location", "apiFactory", "ngCart", function ($rootScope, $http, $location, apiFactory, ngCart) {
    $rootScope.ngCart = ngCart;

    $rootScope.$broadcast('checkCartList', {});
}]);