'use strict';

/* Services */

var eComServices = angular.module('eComServices', ['ngResource']);

eComServices.service('eComCarf', ['eComCarfDetail', 'eComCarfItem', 'webStorage', '$cookies',
    function (eComCarfDetail, eComCarfItem, webStorage, $cookies) {
        function getCookieFUTH() {
            var ck = $cookies.get('.FOENAUTH');
            if (ck == void 0)
                ck = "";
            return ck;
        }

        function setCartWebStorage(thisCart) {
            var ck = getCookieFUTH();
            var cartAndHash = {
                Hash: ck.hashCode(),
                Cart: thisCart
            }
            webStorage.set('Cart', cartAndHash);
            return thisCart;
        }
        function getCartWebStorage() {
            var cartAndHash = webStorage.get('Cart');
            if (cartAndHash != false) {
                var ck = getCookieFUTH();
                if (angular.isObject(cartAndHash)) {
                    if (cartAndHash.Hash == ck.hashCode() || cartAndHash.Hash == 0) {
                        if (angular.isObject(cartAndHash.Cart))
                            return cartAndHash.Cart;
                    }
                }
            }
            return false;
        }

        this.checkWebStorage = function () {
            if (angular.isObject(getCartWebStorage())) {
                this.$restore(getCartWebStorage());
            } else {
                this.init();
            }
            this.$save();
        };

        this.init = function () {
            this.Cart = new eComCarfDetail();
        };

        this.$restore = function (stroageCart) {
            var _self = this.init();
            var _selfCart = this.Cart;
            _selfCart.DeliverType = parseInt(stroageCart.DeliverType);
            _selfCart.PaymentType = parseInt(stroageCart.PaymentType);
            _selfCart.setSenderAddress(stroageCart.SenderAddress);
            _selfCart.setReceiptAddress(stroageCart.ReceiptAddress);
            angular.forEach(stroageCart.Items, function (item) {
                _selfCart.addItem(item.Id, item.Name, item.ImageFiles, item.Quantity, item.PricePerUnit, item.Detail, item.Currency, item.PriceDeliver, item.ElectronicFileStatus, item.DeliverStatus);
            });
        };

        this.$save = function () {
            var setWeb = setCartWebStorage(this.Cart);
            return setWeb;
        };

        this.$clear = function () {
            return webStorage.remove('Cart');
        };

        this.getItemById = function (id) {
            var items = this.Cart.getItems();
            var build = false;
            angular.forEach(items, function (item) {
                if (item.getId() === id) {
                    build = item;
                }
            });
            return build;
        };

        this.addItemById = function (id, quantity) {
            var inCart = this.getItemById(id);

            if (typeof inCart === 'object') {
                // Update quantity of an item if it's already in the cart
                inCart.setQuantity(quantity);
            } else {
                this.Cart.addItem(id, quantity);
            }
            this.$save();
        };

        this.addItem = function (productObject) {
            var inCart = this.getItemById(productObject.Id);
            if (typeof inCart === 'object') {
                // Update quantity of an item if it's already in the cart
                inCart.setQuantity(productObject.Quantity);
            } else {
                this.Cart.addItem(productObject.Id, productObject.Name, productObject.ImageFiles, productObject.Quantity, productObject.PricePerUnit, productObject.Detail, productObject.Currency, productObject.PriceDeliver, productObject.ElectronicFileStatus, productObject.DeliverStatus);
            }
            this.$save();
        };

        this.removeItemById = function (id) {
            var cart = this.Cart;
            angular.forEach(cart.Items, function (item, index) {
                if (item.getId() === id) {
                    cart.Items.splice(index, 1);
                }
            });
            this.$save();
        };

        this.getPriceTotal = function () {
            return this.Cart.getPriceTotal();
        };

        this.getCurrency = function () {
            return this.Cart.Items.length >= 1 ? this.Cart.Items[0].Currency : 'THB';
        };

        this.getPriceDeliver = function () {
            return this.Cart.getPriceDeliver();
        };

        this.getTotalItems = function () {
            var count = 0;
            var items = this.Cart.Items;
            angular.forEach(items, function (item) {
                count += item.getQuantity();
            });
            return count;
        };

        this.getTotalUniqueItems = function () {
            return this.Cart.Items.length;
        };

        this.updateByProductObject = function (productObject) {
            var inCart = this.getItemById(productObject.Id);
            if (typeof inCart === 'object') {
                // Update quantity of an item if it's already in the cart
                inCart.setName(productObject.Name);
                inCart.setDetail(productObject.Detail);
                inCart.setImageFiles(productObject.ImageFiles);
                inCart.setPricePerUnit(productObject.Price);
                inCart.setCurrency(productObject.Currency);
                inCart.setPriceDeliver(productObject.PriceDeliver);
                inCart.setElectronicFileStatus(productObject.ElectronicFileStatus);
                inCart.setDeliverStatus(productObject.DeliverStatus);
            }
        };

        this.updateByArrayProductObject = function (arrayProductObject) {
            var _self = this;
            angular.forEach(arrayProductObject, function (productObject, keyORindex) {
                _self.updateByProductObject(productObject);
            });
        };

        this.initeComCarfItem = function (productObject) {
            var CartItem = new eComCarfItem(productObject.Id, productObject.Name, productObject.ImageFiles, 0, productObject.Price, productObject.Detail, productObject.Currency, productObject.PriceDeliver, productObject.ElectronicFileStatus, productObject.DeliverStatus);
            return CartItem;
        };

        this.checkEletronicOrder = function () {
            var items = this.Cart.getItems();
            var eletronicOrder = items.every(function (item, index, array) {
                return item.DeliverStatus == false && item.ElectronicFileStatus == true;
            });
            return eletronicOrder;
        };
    }]);

eComServices.factory('eComCarfDetail', ['eComCarfItem', 'eComCartAddressDetail',
    function (eComCarfItem, eComCartAddressDetail) {
        var CartDetail = function () {
            this.DeliverType = 1;
            this.Deliver = {};
            this.PaymentType = 1;
            this.Items = [];
            this.Price = 0.0;
            this.Current = "THB";
            this.SenderAddress = new eComCartAddressDetail("", "", "", "", "", "", "");
            this.ReceiptAddress = new eComCartAddressDetail("", "", "", "", "", "", "");
        };

        CartDetail.prototype.setSenderAddress = function (object) {
            this.SenderAddress = new eComCartAddressDetail(object.Fullname, object.Address, object.Province, object.Country, object.Postcode, object.Phonenumber, object.Email);
        };
        CartDetail.prototype.getSenderAddress = function (object) {
            return this.SenderAddress;
        };

        CartDetail.prototype.setReceiptAddress = function (object) {
            this.ReceiptAddress = new eComCartAddressDetail(object.Fullname, object.Address, object.Province, object.Country, object.Postcode, object.Phonenumber, object.Email);
        };

        CartDetail.prototype.getItemById = function (itemId) {
            var items = this.Items;
            var build = false;
            angular.forEach(items, function (Item) {
                if (Item.getId() === itemId) {
                    build = Item;
                }
            });
            return build;
        };

        CartDetail.prototype.getItems = function () {
            return this.Items;
        };

        CartDetail.prototype.addItem = function (A1, A2, A3, A4, A5, A6, A7, A8, A9, A10) {
            if (arguments.length == 2) {
                // Length of argument = 2, A1 is Id, A2 is Quantity
                this.addItem(A1, "", "", A2, 0.0, "", "");
            } else if (arguments.length == 10) {
                var inCart = this.getItemById(A1);
                if (typeof inCart === 'object') {
                    // Update quantity of an item if it's already in the cart
                    inCart.setQuantity(A4);
                } else {
                    // Length of argument = 7, A1 is Id, A2 is Name, A3 is ImageFiles, A4 is Quantity, A5 is PricePerUnit, A6 is Detail, A7 is Currency
                    var newItem = new eComCarfItem(A1, A2, A3, A4, A5, A6, A7, A8, A9, A10);
                    this.Items.push(newItem);
                }
            }
        };

        CartDetail.prototype.getPriceTotal = function () {
            var price = 0.0;
            angular.forEach(this.Items, function (Item) {
                price = price + Item.getPriceTotal();
            });
            return price;
        };

        CartDetail.prototype.getPriceDeliver = function () {
            var price = 0.0;
            angular.forEach(this.Items, function (Item) {
                price = price + Item.getPriceDeliverTotal();
            });
            return price;
        }

        return CartDetail;
    }]);

eComServices.service('eComCarfItem',
    function () {
        var Item = function (Id, Name, ImageFiles, Quantity, PricePerUnit, Detail, Currency, PriceDeliver, ElectronicFileStatus, DeliverStatus) {
            this.setId(Id);
            this.setName(Name);
            this.setDetail(Detail);
            this.setImageFiles(ImageFiles);
            this.setQuantity(Quantity);
            this.setPricePerUnit(PricePerUnit);
            this.setCurrency(Currency);
            this.setPriceDeliver(PriceDeliver);
            this.setElectronicFileStatus(ElectronicFileStatus);
            this.setDeliverStatus(DeliverStatus);
        };

        Item.prototype.setId = function (id) {
            if (id) this.Id = parseInt(id);
            else {
                console.error('An ID must be provided');
            }
        };
        Item.prototype.getId = function () {
            return this.Id;
        };

        Item.prototype.setName = function (name) {
            this.Name = name;
        };
        Item.prototype.getName = function () {
            return this.Name;
        };

        Item.prototype.setImageFiles = function (imageFiles) {
            var _ImageFiles = [];
            angular.forEach(imageFiles, function (imagefile, key) {
                _ImageFiles.push({ FilePath: imagefile.FilePath.replace('~', '') });
            });
            this.ImageFiles = _ImageFiles;
        };
        Item.prototype.getImageFiles = function () {
            return this.ImageFiles;
        };

        Item.prototype.setQuantity = function (quantity) {
            this.Quantity = parseInt(quantity);
        };
        Item.prototype.getQuantity = function () {
            return this.Quantity;
        };

        Item.prototype.setPricePerUnit = function (pricePerUnit) {
            this.PricePerUnit = parseFloat(pricePerUnit);
        };
        Item.prototype.getPricePerUnit = function () {
            return this.PricePerUnit;
        };

        Item.prototype.getPriceTotal = function () {
            return this.PricePerUnit * this.Quantity;
        };

        Item.prototype.setDetail = function (detail) {
            this.Detail = detail;
        };
        Item.prototype.getDetail = function () {
            return this.Detail;
        };

        Item.prototype.setCurrency = function (currency) {
            this.Currency = currency;
        };
        Item.prototype.getCurrency = function () {
            return this.Currency;
        };

        Item.prototype.setPriceDeliver = function (priceDeliver) {
            this.PriceDeliver = priceDeliver;
        };
        Item.prototype.getPriceDeliver = function () {
            return this.PriceDeliver;
        };
        Item.prototype.getPriceDeliverTotal = function () {
            return this.PriceDeliver * this.Quantity;
        };

        Item.prototype.setDeliverStatus = function (status) {
            this.DeliverStatus = status;
        };
        Item.prototype.setElectronicFileStatus = function (status) {
            this.ElectronicFileStatus = status;
        }

        Item.prototype.getDeliverStatus = function () {
            return this.DeliverStatus;
        };
        Item.prototype.getElectronicFileStatus = function () {
            return this.ElectronicFileStatus;
        }

        Item.prototype.extendCategory = function (category) {
            this["Category"] = category;
        };
        Item.prototype.extendStock = function (stock) {
            var Stock = function () { this._data = stock }
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
            this["Stock"] = new Stock;
        };

        return Item;
    });

eComServices.factory('eComCartAddressDetail',
    function () {
        var AddressDetail = function (fullname, address, province, country, postcode, phonenumber, email) {
            this.Fullname = fullname;
            this.Address = address;
            this.Province = province;
            this.Country = country;
            this.Postcode = postcode;
            this.Phonenumber = phonenumber;
            this.Email = email;
        };
        return AddressDetail;
    });

eComServices.service('fileUpload', ['$http',
    function ($http) {
        this.uploadFileToUrl = function (uploadUrl, data) {
            var fd = new FormData();
            for (var key in data)
                fd.append(key, data[key]);
            return $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            });
        };
    }]);