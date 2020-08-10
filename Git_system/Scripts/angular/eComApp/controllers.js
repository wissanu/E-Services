'use strict';

/* Controllers */

var eComControllers = angular.module('eComControllers', []);

eComControllers.controller('MainCtrl', ['$scope', '$resource', 'Category_CRUD_API', 'CategoryHomePage_CRUD_API', 'PromiseWithFunctionCheckStatus', 'eComCarf', 'Slideshow_CRUD_API',
    function ($scope, $resource, Category_CRUD_API, CategoryHomePage_CRUD_API, PromiseWithFunctionCheckStatus, eComCarf, Slideshow_CRUD_API) {
        var promiseWithFunction = new PromiseWithFunctionCheckStatus();
        console.log(promiseWithFunction);
        promiseWithFunction.addPromiseWithTimeout(
            30000,
            Category_CRUD_API.query({}).$promise,
            function success(response) {
                $scope.Categories = response.data;
                if (response.data.length > 0)
                    $scope.activeCategoryId = response.data[0].Id
            }
        ).addPromiseWithTimeout(
            30000,
            CategoryHomePage_CRUD_API.query({}).$promise,
            function success(response) {
                $scope.CategoriesOnPage = response.data;

                eComCarf.checkWebStorage();
                var cart = eComCarf;
                $scope.totalItems = cart.getTotalItems();
            }
        ).addPromiseWithTimeout(
            30000,
            Slideshow_CRUD_API.query({}).$promise,
            function success(response) {
                $scope.slideshows = response.data;
            }
        );
        promiseWithFunction.runPromiseDefault($scope);

        $scope.setActiveCategory = function (categoryId) {
            $scope.activeCategoryId = categoryId;
        }
    }]);

eComControllers.controller('ProductCtrl', ['$scope', '$location', 'Product_CRUD_API', 'eComCarf', 'Category_CRUD_API', 'PromiseWithFunctionCheckStatus',
    function ($scope, $location, Product_CRUD_API, eComCarf, Category_CRUD_API, PromiseWithFunctionCheckStatus) {
        eComCarf.checkWebStorage();
        $scope.Cart = eComCarf;

        var productId = parseInt($location.search().ProductId);
        $scope.totalItems = 0;

        $scope.$on('changeQuantity', function () {
            if ($scope.Cart.getItemById(productId))
                $scope.product.Quantity = $scope.Cart.getItemById(productId).getQuantity();
            else
                $scope.product.Quantity = 1;
            $scope.totalItems = $scope.Cart.getTotalItems();
        });

        // Main promise.
        var promiseWithFunction = new PromiseWithFunctionCheckStatus();
        promiseWithFunction.addPromiseWithTimeout(
            30000,
            Category_CRUD_API.query({}).$promise,
            function success(response) {
                $scope.Categories = response.data;
            }
        );

        //Product and relater promise.
        var get_product_and_relater = function (product_id) {
            var productAndRelater = new PromiseWithFunctionCheckStatus();
            productAndRelater.addPromiseWithTimeout(
                30000,
                Product_CRUD_API.get({ id: product_id }).$promise,
                function success(response) {
                    var product = response.data;

                    if ($scope.Cart.getItemById(product.Id)) {
                        $scope.Cart.updateByProductObject(product);
                        $scope.product = $scope.Cart.getItemById(product.Id);
                        $scope.product.extendCategory(response.data.Category);
                        $scope.product.extendStock(response.data.Stock);
                        $scope.product.Description = response.data.Description;
                        $scope.product.DemoFile = function () { var arr = []; angular.forEach(response.data.DemoElectronicFiles, function (value, key) { arr.push({ FilePath: value.FilePath.replace('~', ''), Filename: value.Filename }); }); return arr; }();
                        $scope.product.OtherFiles = function () { var arr = []; angular.forEach(response.data.OtherFiles, function (value, key) { arr.push({ FilePath: value.FilePath.replace('~', ''), Filename: value.Filename }); }); return arr; }();
                    }
                    else {
                        $scope.product = $scope.Cart.initeComCarfItem(product);
                        $scope.product.extendCategory(response.data.Category);
                        $scope.product.extendStock(response.data.Stock);
                        $scope.product.Description = response.data.Description;
                        $scope.product.DemoFile = function () { var arr = []; angular.forEach(response.data.DemoElectronicFiles, function (value, key) { arr.push({ FilePath: value.FilePath.replace('~', ''), Filename: value.Filename }); }); return arr; }();
                        $scope.product.OtherFiles = function () { var arr = []; angular.forEach(response.data.OtherFiles, function (value, key) { arr.push({ FilePath: value.FilePath.replace('~', ''), Filename: value.Filename }); }); return arr; }();
                    }
                    productId = product_id;
                    $scope.$broadcast('changeQuantity', {});
                }
            ).addPromiseWithTimeout(
                30000,
                Product_CRUD_API.query({ Related: product_id }).$promise,
                function success(response) {
                    $scope.relatedItem_Products = response.data;
                }
            );
            return productAndRelater;
        }

        promiseWithFunction.addMultiPromise(get_product_and_relater(productId)).runPromiseDefault($scope);

        $scope.addToCart = function (productObject) {
            $scope.Cart.addItem(productObject);
            $scope.$broadcast('changeQuantity');
        };
        $scope.removeInCart = function () {
            $scope.Cart.removeItemById(productId);
            $scope.$broadcast('changeQuantity');
        };

        // change product.
        $scope.changeProduct = function (productid) {
            $scope.loadObject = {
                status: true,
                message: "Loading..."
            };
            var promise_product_and_relater = new PromiseWithFunctionCheckStatus();
            promise_product_and_relater.addMultiPromise(get_product_and_relater(productid)).runPromiseDefault($scope);
        };

        $scope.upQuantity = function () {
            $scope.product.Quantity = $scope.product.Quantity + 1;
        };

        $scope.downQuantity = function () {
            $scope.product.Quantity = $scope.product.Quantity == 0 ? 0 : $scope.product.Quantity - 1;
        };
    }]);

eComControllers.controller('CartCtrl', ['$scope', 'eComCarf', 'Product_CRUD_API', 'DeliverType_CRUD_API', 'PromiseWithFunctionCheckStatus',
    function ($scope, eComCarf, Product_CRUD_API, DeliverType_CRUD_API, PromiseWithFunctionCheckStatus) {
        eComCarf.checkWebStorage();
        $scope.Cart = eComCarf;

        var promiseWithFunction = new PromiseWithFunctionCheckStatus();
        promiseWithFunction.addPromiseWithTimeout(
            30000,
            DeliverType_CRUD_API.query({}).$promise,
            function success(response) {
                $scope.deliverTypes = {
                    availableOptions: response.data,
                    selectedOption: response.data[0]
                };
            }
        ).addPromiseWithTimeout(
            30000,
            Product_CRUD_API.query({}).$promise,
            function success(response) {
                $scope.Cart.updateByArrayProductObject(response.data);
                $scope.Cart.$save();

                // init deliverType in cart
                $scope.Cart.Cart.Deliver = $scope.Cart.Cart.DeliverType[0];
                angular.forEach($scope.deliverTypes.availableOptions, function (item) {
                    if (item.Id === $scope.Cart.Cart.DeliverType) {
                        $scope.deliverTypes.selectedOption = item;
                        $scope.Cart.Cart.Deliver = item;
                    }
                });
                console.log($scope.Cart.getPriceDeliver());
            }
        );

        promiseWithFunction.runPromiseDefault($scope);

        $scope.save = function () {
            $scope.Cart.Cart.DeliverType = $scope.deliverTypes.selectedOption.Id;
            $scope.Cart.$save();
        };

        $scope.removeInCart = function (productId) {
            $scope.Cart.removeItemById(productId);
        };
    }]);

eComControllers.controller('DeliverCtrl', ['$scope', 'eComCarf', 'Profile_CRUD_API', 'UpdataCart', '$window', 'PromiseWithFunctionCheckStatus', '$timeout', 'PaymentType_CRUD_API', 'Legal_CRUD_API', 'Order_CRUD_API',
    function ($scope, eComCarf, Profile_CRUD_API, UpdataCart, $window, PromiseWithFunctionCheckStatus, $timeout, PaymentType_CRUD_API, Legal_CRUD_API, Order_CRUD_API) {
        $scope.loadObject.message = "Loading...";
        $scope.loadObject.status = true;

        var getCartObject = function () {
            eComCarf.checkWebStorage();
            $scope.Cart = eComCarf;

            var cartObject = new Order_CRUD_API();
            cartObject.DeliverType = $scope.Cart.Cart.DeliverType;
            cartObject.PaymentType = $scope.Cart.Cart.PaymentType;
            cartObject.Items = $scope.Cart.Cart.Items;
            cartObject.Price = $scope.Cart.Cart.Price;
            cartObject.Current = $scope.Cart.Cart.Current;
            cartObject.SenderAddress = $scope.Cart.Cart.SenderAddress;
            cartObject.ReceiptAddress = $scope.Cart.Cart.ReceiptAddress;

            return cartObject;
        }

        var promiseWithFunction = new PromiseWithFunctionCheckStatus();
        promiseWithFunction.addPromiseWithTimeout(
            30000,
            getCartObject().$check({}),
            function success(response) {
                function getPromotionDiscount(promotionDetail) {
                    var price = 0;
                    promotionDetail.forEach(function (item) {
                        price += item.Price;
                    });
                    return price;
                }
                function getSummaryTotalPrice(data) {
                    return data.ProductTotalPrice + data.DeliverPrice - getPromotionDiscount(data.PromotionDetail);
                }
                $scope.orderDetail = {
                    deliverPrice: response.data.DeliverPrice,
                    price: response.data.ProductPrice,
                    vat: response.data.ProductVat,
                    totalPrice: getSummaryTotalPrice(response.data),
                    promotionList: response.data.PromotionDetail
                }
            }
        ).addPromiseWithTimeout(
            30000,
            Profile_CRUD_API.query({}).$promise,
            function success(response) {
                function getAddress(_response) {
                    var address = "";

                    function checkNullAndAppEnd(_data, prefix) {
                        if (prefix == void 0 || prefix == null)
                            prefix = "";
                        if (_data != "" && _data != void 0)
                            address += " " + prefix + _data;
                    }

                    // If country is thailand
                    if (_response.CountryName != null) {
                        if (_response.CountryName.toLowerCase() == "thailand") {
                            checkNullAndAppEnd(_response.Address);
                            checkNullAndAppEnd(_response.Road, "ถนน ");
                            var DistrictPrefix = "ต. ";
                            var AmphurPrefix = "อ. ";
                            if (_response.Province == 1) {
                                DistrictPrefix = "แขวง ";
                                AmphurPrefix = "";
                            }
                            checkNullAndAppEnd(_response.DistrictName, DistrictPrefix);
                            checkNullAndAppEnd(_response.AmphurName, AmphurPrefix);
                        }//If Other country
                        else {
                            checkNullAndAppEnd(_response.Address);
                            checkNullAndAppEnd(_response.Road);
                            checkNullAndAppEnd(_response.City);
                            checkNullAndAppEnd(_response.State);
                        }
                    }

                    return address.trim();
                }
                var deliverAddress = {
                    Fullname: response.data.Firstname + " " + response.data.Lastname,
                    Address: getAddress(response.data),
                    Province: response.data.ProvinceName,
                    Country: response.data.CountryName,
                    Postcode: response.data.Zipcode,
                    Phonenumber: response.data.Mobile,
                    Email: response.data.Email
                };

                eComCarf.checkWebStorage();
                $scope.Cart = eComCarf;

                if ($scope.Cart.Cart.Items.length == 0)
                    $window.location.href = '/Home/ProductView';

                UpdataCart.updateProduct($scope.Cart);
                UpdataCart.updateDeliverType($scope.Cart);

                $scope.Cart.Cart.setSenderAddress(deliverAddress);
                $scope.Cart.Cart.setReceiptAddress($scope.Cart.Cart.getSenderAddress());
            }
        ).addPromiseWithTimeout(
            30000,
            PaymentType_CRUD_API.query({}).$promise,
            function success(response) {
                $scope.paymentTypes = response.data;
            }
        ).addPromiseWithTimeout(
            30000,
            Legal_CRUD_API.query({}).$promise,
            function success(response) {
                $scope.legal = response.data;
            }
        );
        promiseWithFunction.PromiseOverride($scope).setReject(function (response) {
            if (response[0].status == 201) {
                console.log(response);
                $scope.loadObject.message = "Not login...";
                $timeout(function () {
                    $scope.loadObject.message = "load...";
                    $scope.loadObject.status = false;
                    $scope.NotLogin = true;
                }, 1000);
            } else {
                $window.location.href = '/Home';
            }
        }).execute();

        function validateEmail(email) {
            var re = /^[a-zA-Z]+[a-zA-Z0-9._-]+@[a-zA-Z]+\.[a-zA-Z.]{2,}$/;
            return re.test(email);
        };

        $scope.validSenderEmail = true;
        $scope.fnValidSenderEmail = function () {
            $scope.validSenderEmail = validateEmail($scope.Cart.Cart.SenderAddress.Email);
        };

        $scope.validReceiptEmail = true;
        $scope.fnValidReceiptEmail = function () {
            $scope.validReceiptEmail = validateEmail($scope.Cart.Cart.ReceiptAddress.Email);
        };

        function checkForm() {
            return true == validateEmail($scope.Cart.Cart.SenderAddress.Email) == validateEmail($scope.Cart.Cart.ReceiptAddress.Email);
        };

        $scope.save = function () {
            console.log($scope.Cart);
            $scope.Cart.$save();
            $window.location.href = '/Home/ProductShoppingPayment';
        };

        $scope.logInFn = function () {
            $scope.NotLogin = false;
            $scope.loadObject.message = "Loading...";
            $scope.loadObject.status = true;

            var profileLogin = new Profile_CRUD_API();
            profileLogin.Email = $scope.loginEmail;
            profileLogin.Password = $scope.loginPassword;
            var promiseWithFunction = new PromiseWithFunctionCheckStatus();
            promiseWithFunction.addPromiseWithTimeout(
                30000,
                profileLogin.$save({}),
                function success(response) {
                    $window.location.reload();
                }
            );
            promiseWithFunction.runPromiseDefault($scope);
            promiseWithFunction.PromiseOverride($scope).setReject(function (response) {
                $scope.loadObject.message = "Loading...";
                $scope.loginValidationMessage = response[0].message;
                $timeout(function () {
                    $scope.NotLogin = true;
                    $scope.loadObject.status = false;
                }, 1000);
            }).execute();
        };
    }]);

eComControllers.controller('PaymentCtrl', ['$scope', 'eComCarf', 'Order_CRUD_API', '$timeout', '$window', 'UpdataCart', 'PromiseWithFunctionCheckStatus', 'PaymentType_CRUD_API',
    function ($scope, eComCarf, Order_CRUD_API, $timeout, $window, UpdataCart, PromiseWithFunctionCheckStatus, PaymentType_CRUD_API) {
        eComCarf.checkWebStorage();
        $scope.Cart = eComCarf;

        if ($scope.Cart.Cart.Items.length == 0)
            $window.location.href = '/Home/ProductView';

        UpdataCart.updateProduct($scope.Cart);
        UpdataCart.updateDeliverType($scope.Cart);

        var getCartObject = function () {
            var cartObject = new Order_CRUD_API();
            cartObject.DeliverType = $scope.Cart.Cart.DeliverType;
            cartObject.PaymentType = $scope.Cart.Cart.PaymentType;
            cartObject.Items = $scope.Cart.Cart.Items;
            cartObject.Price = $scope.Cart.Cart.Price;
            cartObject.Current = $scope.Cart.Cart.Current;
            cartObject.SenderAddress = $scope.Cart.Cart.SenderAddress;
            cartObject.ReceiptAddress = $scope.Cart.Cart.ReceiptAddress;

            return cartObject;
        }

        var promiseWithFunction = new PromiseWithFunctionCheckStatus();
        promiseWithFunction.addPromiseWithTimeout(
            30000,
            getCartObject().$check({}),
            function success(response) {
                function getPromotionDiscount(promotionDetail) {
                    var price = 0;
                    promotionDetail.forEach(function (item) {
                        price += item.Price;
                    });
                    return price;
                }
                function getSummaryTotalPrice(data) {
                    return data.ProductTotalPrice + data.DeliverPrice - getPromotionDiscount(data.PromotionDetail);
                }
                $scope.orderDetail = {
                    deliverPrice: response.data.DeliverPrice,
                    price: response.data.ProductPrice,
                    vat: response.data.ProductVat,
                    totalPrice: getSummaryTotalPrice(response.data),
                    promotionList: response.data.PromotionDetail
                }
            }
        ).addPromiseWithTimeout(
            30000,
            PaymentType_CRUD_API.query({}).$promise,
            function success(response) {
                $scope.paymentName = "";
                angular.forEach(response.data, function (value, key) {
                    if (value.Id == $scope.Cart.Cart.PaymentType)
                        $scope.paymentName = value.Name;
                });
            }
        ).runPromiseDefault($scope);

        $scope.save = function () {
            $scope.loadObject.status = true;
            $scope.loadObject.status = "Loading...";
            console.log($scope.Cart);
            $scope.Cart.$save();

            var promiseWithFunction = new PromiseWithFunctionCheckStatus();
            promiseWithFunction.addPromiseWithTimeout(
                30000,
                getCartObject().$save({}),
                function success(response) {
                    if (response.data === void 0)
                        $window.location.href = '/Home/ProductView';
                    else {
                        var resData = response.data;
                        if (resData.PaymentType === 2) {
                            var parmeObject = {
                                amount: resData.Price.toFixed(2),
                                reference_number: resData.Id,
                                currency: resData.Currency.toLowerCase(),
                                transaction: "ecom"
                            }
                            var parmesList = [];
                            for (var key in parmeObject) {
                                if (parmeObject.hasOwnProperty(key)) {
                                    parmesList.push(key + "=" + parmeObject[key]);
                                }
                            }
                            $scope.Cart.$clear();
                            $window.location.href = '/Home/RedirectToCreditCard?' + parmesList.join("&");
                        }
                        else {
                            $scope.Cart.$clear();
                            $window.location.href = '/Home/ProductOrderHistory';
                        }
                    }
                }
            );
            promiseWithFunction.runPromiseDefault($scope);
        };
    }]);

eComControllers.controller('OrderHistoryCtrl', ['$scope', 'Order_CRUD_API', '$timeout', '$window', 'PromiseWithFunctionCheckStatus', 'Profile_CRUD_API', 'NgTableParams',
    function ($scope, Order_CRUD_API, $timeout, $window, PromiseWithFunctionCheckStatus, Profile_CRUD_API, NgTableParams) {
        var promiseWithFunction = new PromiseWithFunctionCheckStatus();
        promiseWithFunction.addPromiseWithTimeout(
            30000,
            Order_CRUD_API.query({ Payment: 0, Deliver: 0 }).$promise,
            function success(response) {
                $scope.orderHistory = response.data;
                $scope.tableParams = new NgTableParams({ count: 10 }, { counts: [], paginationMaxBlocks: 6, dataset: response.data });
            }
        );
        promiseWithFunction.PromiseOverride($scope).setReject(function (response) {
            if (response[0].status == 201) {
                console.log(response);
                $scope.loadObject.message = "Not login...";
                $timeout(function () {
                    $scope.loadObject.message = "load...";
                    $scope.loadObject.status = false;
                    $scope.NotLogin = true;
                }, 1000);
            } else {
                $window.location.href = '/Home';
            }
        }).execute();

        $scope.logInFn = function () {
            $scope.NotLogin = false;
            $scope.loadObject.message = "Loading...";
            $scope.loadObject.status = true;

            var profileLogin = new Profile_CRUD_API();
            profileLogin.Email = $scope.loginEmail;
            profileLogin.Password = $scope.loginPassword;
            var promiseWithFunction = new PromiseWithFunctionCheckStatus();
            promiseWithFunction.addPromiseWithTimeout(
                30000,
                profileLogin.$save({}),
                function success(response) {
                    $window.location.reload();
                }
            );
            promiseWithFunction.runPromiseDefault($scope);
            promiseWithFunction.PromiseOverride($scope).setReject(function (response) {
                $scope.loadObject.message = "Loading...";
                $scope.loginValidationMessage = response[0].message;
                $timeout(function () {
                    $scope.NotLogin = true;
                    $scope.loadObject.status = false;
                }, 1000);
            }).execute();
        };
    }]);

eComControllers.controller('OrderHistoryDetailCtrl', ['$scope', 'Order_CRUD_API', '$timeout', '$window', 'PromiseWithFunctionCheckStatus', 'Profile_CRUD_API', '$location', 'Product_CRUD_API',
    function ($scope, Order_CRUD_API, $timeout, $window, PromiseWithFunctionCheckStatus, Profile_CRUD_API, $location, Product_CRUD_API) {
        var orderId = parseInt($location.search().OrderId);
        if (orderId === void 0) { orderId = 0; }

        var promiseWithFunction = new PromiseWithFunctionCheckStatus();
        promiseWithFunction.addPromiseWithTimeout(
            30000,
            Order_CRUD_API.query({ id: orderId }).$promise,
            function success(response) {
                $scope.orderDetail = response.data;
                angular.forEach($scope.orderDetail.OrderItem, function (item) {
                    Product_CRUD_API.query({ id: item.EComProductId },
                        function (data) {
                            item.Product = data.data;
                        })
                });
            }
        );
        promiseWithFunction.PromiseOverride($scope).setReject(function (response) {
            if (response[0].status == 201) {
                console.log(response);
                $scope.loadObject.message = "Not login...";
                $timeout(function () {
                    $scope.loadObject.message = "load...";
                    $scope.loadObject.status = false;
                    $scope.NotLogin = true;
                }, 1000);
            } else {
                $window.location.href = '/Home';
            }
        }).execute();

        $scope.logInFn = function () {
            $scope.NotLogin = false;
            $scope.loadObject.message = "Loading...";
            $scope.loadObject.status = true;

            var profileLogin = new Profile_CRUD_API();
            profileLogin.Email = $scope.loginEmail;
            profileLogin.Password = $scope.loginPassword;
            var promiseWithFunction = new PromiseWithFunctionCheckStatus();
            promiseWithFunction.addPromiseWithTimeout(
                30000,
                profileLogin.$save({}),
                function success(response) {
                    $window.location.reload();
                }
            );
            promiseWithFunction.runPromiseDefault($scope);
            promiseWithFunction.PromiseOverride($scope).setReject(function (response) {
                $scope.loadObject.message = "Loading...";
                $scope.loginValidationMessage = response[0].message;
                $timeout(function () {
                    $scope.NotLogin = true;
                    $scope.loadObject.status = false;
                }, 1000);
            }).execute();
        };

        $scope.getProduct = function () {
            Product_CRUD_API.get({ id: product_id }).$promise
        };
    }]);

eComControllers.controller('ConfirmOrderCtrl', ['$scope', 'Order_CRUD_API', 'fileUpload', '$location', 'BankType_CRUD_API', '$window', 'PromiseWithFunctionCheckStatus', '$timeout', '$q',
    function ($scope, Order_CRUD_API, fileUpload, $location, BankType_CRUD_API, $window, PromiseWithFunctionCheckStatus, $timeout, $q) {
        var orderId = parseInt($location.search().OrderId);
        if (orderId === void 0) { orderId = 0; }

        var promiseWithFunction = new PromiseWithFunctionCheckStatus();
        promiseWithFunction.addPromiseWithTimeout(
            30000,
            BankType_CRUD_API.query({}).$promise,
            function success(response) {
                $scope.bankTypes = {
                    availableOptions: response.data,
                    selectedOption: response.data[0]
                }
            }
        ).addPromiseWithTimeout(
            30000,
            Order_CRUD_API.query({ id: orderId }).$promise,
            function success(response) {
                console.log(response);
                $scope.EComOrder = {
                    EComOrderId: orderId,
                    ConfirmPaymentBankTypeId: 1,
                    Fullname: response.data.ReceiptFullname,
                    Datetimes: {
                        Time: "",
                        Date: ""
                    },
                    Datetime: "",
                    Price: 0
                };
            }
        );
        promiseWithFunction.PromiseOverride($scope).setReject(function (response) {
            if (response[0].status == 201 || response[1].status == 201) {
                console.log(response);
                $scope.loadObject.message = "Not login...";
                $timeout(function () {
                    $scope.loadObject.message = "load...";
                    $scope.loadObject.status = false;
                    $scope.NotLogin = true;
                }, 1000);
            } else {
                $window.location.href = '/Home';
            }
        }).execute();

        $scope.logInFn = function () {
            $scope.NotLogin = false;
            $scope.loadObject.message = "Loading...";
            $scope.loadObject.status = true;

            var profileLogin = new Profile_CRUD_API();
            profileLogin.Email = $scope.loginEmail;
            profileLogin.Password = $scope.loginPassword;
            var promiseWithFunction = new PromiseWithFunctionCheckStatus();
            promiseWithFunction.addPromiseWithTimeout(
                30000,
                profileLogin.$save({}),
                function success(response) {
                    $window.location.reload();
                }
            );
            promiseWithFunction.runPromiseDefault($scope);
            promiseWithFunction.PromiseOverride($scope).setReject(function (response) {
                $scope.loadObject.message = "Loading...";
                $scope.loginValidationMessage = response[0].message;
                $timeout(function () {
                    $scope.NotLogin = true;
                    $scope.loadObject.status = false;
                }, 1000);
            }).execute();
        };

        // Setup validation rules defalut
        $scope.confirmValidationDateDatePickerStatus = true;
        $scope.confirmValidationDateTimePickerStatus = true;
        $scope.confirmValidationFileStatus = true;
        $scope.confirmValidationPriceStatus = true;

        $scope.sumbit = function () {
            //Check all rules
            $scope.confirmValidationDateDatePickerStatus = ($scope.EComOrder.Datetimes.Date != "" && $scope.EComOrder.Datetimes.Date != void 0);
            $scope.confirmValidationDateTimePickerStatus = ($scope.EComOrder.Datetimes.Time != "" && $scope.EComOrder.Datetimes.Time != void 0);
            $scope.confirmValidationFileStatus = ($scope.EComOrder.FilenamePayment != "" && $scope.EComOrder.FilenamePayment != void 0);
            $scope.confirmValidationPriceStatus = ($scope.EComOrder.Price != "" || $scope.EComOrder.Price != void 0);

            //All rules
            $scope.validationRule = [
                $scope.confirmValidationDateDatePickerStatus,
                $scope.confirmValidationDateTimePickerStatus,
                $scope.confirmValidationFileStatus,
                $scope.confirmValidationPriceStatus
            ];

            var validationRulesStatus = $scope.validationRule.every(function (check, index, array) {
                return check;
            });

            $q.all(validationRulesStatus).then(function (res) {
                console.log(res);
                if (validationRulesStatus == true) {
                    submitApi();
                }
            });

            function submitApi() {
                $scope.loadObject.message = "Loading...";
                $scope.loadObject.status = true;

                var uploadUrl = defineMainUrl + "/api/ConfirmOrder";

                $scope.EComOrder.Datetime = $scope.EComOrder.Datetimes.Date.toString() + " " + $scope.EComOrder.Datetimes.Time.toString();
                $scope.EComOrder.ConfirmPaymentBankTypeId = $scope.bankTypes.selectedOption.Id;
                fileUpload.uploadFileToUrl(uploadUrl, $scope.EComOrder)
                    .success(function (response) {
                        if (response.status == 1) {
                            $scope.loadObject.message = "Success";
                            $timeout($window.location.href = '/Home/ProductView', 1000);
                        } else {
                            $scope.confirmValidationMessage = response.message;
                            $scope.loadObject.status = false;
                        }
                    })
                    .error(function (response) {
                        $scope.confirmValidationMessage = response.message;
                        $scope.loadObject.status = false;
                    });
            };
        }
    }]);

eComControllers.controller('ProductTrackCtrl', ['$scope', 'Order_CRUD_API', '$timeout', '$window', 'PromiseWithFunctionCheckStatus', 'Profile_CRUD_API', 'NgTableParams', 'filterFilter',
    function ($scope, Order_CRUD_API, $timeout, $window, PromiseWithFunctionCheckStatus, Profile_CRUD_API, NgTableParams, filterFilter) {
        var promiseWithFunction = new PromiseWithFunctionCheckStatus();
        promiseWithFunction.addPromiseWithTimeout(
            30000,
            Order_CRUD_API.query({ Payment: 3 }).$promise,
            function success(response) {
                $scope.orderHistory = filterFilter(response.data, { isEletronic: false });
                $scope.tableParams = new NgTableParams({ count: 5 }, { counts: [], paginationMaxBlocks: 6, dataset: $scope.orderHistory });
            }
        );
        promiseWithFunction.PromiseOverride($scope).setReject(function (response) {
            if (response[0].status == 201) {
                console.log(response);
                $scope.loadObject.message = "Not login...";
                $timeout(function () {
                    $scope.loadObject.message = "load...";
                    $scope.loadObject.status = false;
                    $scope.NotLogin = true;
                }, 1000);
            } else {
                $window.location.href = '/Home';
            }
        }).execute();

        $scope.logInFn = function () {
            $scope.NotLogin = false;
            $scope.loadObject.message = "Loading...";
            $scope.loadObject.status = true;

            var profileLogin = new Profile_CRUD_API();
            profileLogin.Email = $scope.loginEmail;
            profileLogin.Password = $scope.loginPassword;
            var promiseWithFunction = new PromiseWithFunctionCheckStatus();
            promiseWithFunction.addPromiseWithTimeout(
                30000,
                profileLogin.$save({}),
                function success(response) {
                    $window.location.reload();
                }
            );
            promiseWithFunction.runPromiseDefault($scope);
            promiseWithFunction.PromiseOverride($scope).setReject(function (response) {
                $scope.loadObject.message = "Loading...";
                $scope.loginValidationMessage = response[0].message;
                $timeout(function () {
                    $scope.NotLogin = true;
                    $scope.loadObject.status = false;
                }, 1000);
            }).execute();
        };
    }]);

eComControllers.controller('ProductMoreCtrl', ['$scope', '$location', 'Product_CRUD_API', 'eComCarf', 'Category_CRUD_API',
    function ($scope, $location, Product_CRUD_API, eComCarf, Category_CRUD_API) {
        $scope.page_items_per_page = 6;
        $scope.page_maxSize = 3;
        $scope.page_totalItems = 0;
        $scope.page_currentPage = 1;
        $scope.page_datas = [];
        $scope.page_dataWithPage = [];
        $scope.page_setDate = function (datas) {
            $scope.page_totalItems = datas.length;
            $scope.page_datas = datas;
            page_getPage();
        };
        var page_getPage = function () {
            var current = $scope.page_currentPage;
            var items_per_page = $scope.page_items_per_page;
            var totalItems = $scope.page_totalItems;
            var getMaxOfdata = function (current) {
                return items_per_page * current > totalItems ? totalItems : items_per_page * current;
            }
            var datas = $scope.page_datas;
            $scope.page_dataWithPage = datas.slice(items_per_page * (current - 1), getMaxOfdata(current));
        };

        $scope.pageChanged = function () {
            console.log("Page changed to: " + $scope.page_currentPage)
            page_getPage();
        };

        // check in cart
        eComCarf.checkWebStorage();
        $scope.Cart = eComCarf;
        $scope.totalItems = $scope.Cart.getTotalItems();

        // get form url
        var categoryId = parseInt($location.search().CategoryId);
        var search = $location.search().Search;

        // list on product
        $scope.productLists = {};

        // function product by search
        var productBySearch = function (search) {
            $scope.loadObject.status = true;
            Product_CRUD_API.query({ Search: search },
                function success(response) {
                    $scope.productLists = response.data;
                    $scope.page_setDate(response.data);
                    $scope.loadObject.status = false;
                },
                function error(errorResponse) {
                    console.error("Error:" + angular.toJson(errorResponse));
                    $scope.loadObject.message = "Error";
                    $timeout(function () {
                        $scope.loadObject.status = false;
                    }, 1000);
                });
        };

        // function product by category
        var productByCategory = function (categoryId) {
            $scope.loadObject.status = true;
            Product_CRUD_API.query({ Category: categoryId },
                function success(response) {
                    $scope.productLists = response.data;
                    $scope.page_setDate(response.data);
                    $scope.loadObject.status = false;
                },
                function error(errorResponse) {
                    console.error("Error:" + angular.toJson(errorResponse));
                    $scope.loadObject.message = "Error";
                    $timeout(function () {
                        $scope.loadObject.status = false;
                    }, 1000);
                });
        };

        // scope search on view and controller
        $scope.search = function (searchText) {
            if (typeof searchText === "string") {
                $scope.titleBar = searchText;
                productBySearch(searchText);
            }
        };

        // scope change category on view and controller
        $scope.changeCategory = function (categoryId) {
            productByCategory(categoryId);
            categoryChangeTitle(categoryId);
        }

        // function change titile by category id
        var categoryChangeTitle = function (categoryId) {
            $scope.titleBar = "";
            angular.forEach($scope.Categories, function (item) {
                if (item.Id === categoryId) {
                    $scope.titleBar = item.Name;
                }
            });
        }

        // Categories bese on api
        Category_CRUD_API.query({},
            function success(response) {
                $scope.Categories = response.data;
                categoryChangeTitle(categoryId);
            },
            function error(errorResponse) {
                console.error("Error:" + angular.toJson(errorResponse));
            });

        // product list by url search or category id
        if ((typeof categoryId === "number") && !(isNaN(categoryId))) {
            $scope.changeCategory(categoryId);
        } else if ((typeof search === "string") && search != "") {
            $scope.search(search);
        }
    }]);

eComControllers.controller('ProductElectronicCtrl', ['$scope', 'PromiseWithFunctionCheckStatus', 'EletronicProduct_CRUD_API',
    function ($scope, PromiseWithFunctionCheckStatus, EletronicProduct_CRUD_API) {
        var promiseWithFunction = new PromiseWithFunctionCheckStatus();
        promiseWithFunction.addPromiseWithTimeout(
            30000,
            EletronicProduct_CRUD_API.query({}).$promise,
            function success(response) {
                $scope.products = response.data;
            }
        ).runPromiseDefault($scope);
    }]);