'use strict';

/* Services */

var helperServices = angular.module('helperServices', []);

helperServices.service('webStorage', ['$window', function ($window) {
    return {
        get: function (key) {
            if ($window.localStorage[key]) {
                var val = angular.fromJson($window.localStorage[key]);
                return val;
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
        },

        remove: function (key) {
            $window.localStorage.removeItem(key);
            return true;
        }
    };
}]);

helperServices.service('UpdataCart', ['Product_CRUD_API', 'DeliverType_CRUD_API',
    function (Product_CRUD_API, DeliverType_CRUD_API) {
        return {
            updateProduct: function (Cart) {
                Product_CRUD_API.query({},
                    function success(response) {
                        if (response.status == 1) {
                            Cart.updateByArrayProductObject(response.data);
                            Cart.$save();
                        }
                        else {
                            console.error("Error:" + angular.toJson(response));
                            return false;
                        }
                    },
                    function error(errorResponse) {
                        console.error("Error:" + angular.toJson(errorResponse));
                        return false;
                    });
                return true;
            },
            updateDeliverType: function (Cart) {
                DeliverType_CRUD_API.get({ id: Cart.Cart.DeliverType },
                    function success(response) {
                        if (response.status == 1) {
                            Cart.Cart.Deliver = response.data;
                        }
                        else {
                            console.error("Error:" + angular.toJson(response));
                            return false;
                        }
                    },
                    function error(errorResponse) {
                        console.error("Error:" + angular.toJson(errorResponse));
                        return false;
                    });
                return true;
            }
        };
    }]
);