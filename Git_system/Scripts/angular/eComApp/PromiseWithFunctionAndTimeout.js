'use strict';

/* Services */

var promiseWithFunctionAndTimeout = angular.module('promiseWithFunctionAndTimeout', []);

promiseWithFunctionAndTimeout.factory('PromiseWithFunction',
    function ($q) {
        var PromiseWithFunction = function () {
            this.promiseWithFunctionAll = [];
        };

        PromiseWithFunction.prototype.addPromise = function (promise, success, error) {
            var promiseWithFunction = {};
            promiseWithFunction.promise = angular.isUndefined(promise) || !angular.isObject(promise) ? function () { var deferred = $q.defer(); return deferred.promise; } : promise;
            promiseWithFunction.success = angular.isUndefined(success) || !angular.isFunction(success) ? function () { } : success;
            promiseWithFunction.error = angular.isUndefined(error) || !angular.isFunction(error) ? function () { } : error;
            this.promiseWithFunctionAll.push(promiseWithFunction);
            return this;
        };
        PromiseWithFunction.prototype.addPromiseWithTimeout = function (timeout, promise, success, error) {
            var timeoutCheck = false;
            var defer = $q.defer();
            defer.promise = promise;
            promise.then(function (data) {
                if (!timeoutCheck) {
                    defer.resolve(data);
                } else {
                    defer.reject('Time out');
                }
            }).finally(
              function () { clearTimeout(check_timeout); }
            );
            // Set time out.
            var check_timeout = setTimeout(
              function () {
                  timeoutCheck = true;
                  defer.reject('Time out');
              }, timeout);
            var promiseWithTimeout = defer.promise;
            return this.addPromise(promiseWithTimeout, success, error);
        };
        PromiseWithFunction.prototype.getAllPromise = function getArrayPromise() {
            var promiseAll = [];
            promiseAll = this.promiseWithFunctionAll.map(
              function (promiseWithFunction) {
                  return promiseWithFunction.promise
              }
            );
            return promiseAll;
        };
        PromiseWithFunction.prototype.checkPromiseAll = function checkPromiseAll() {
            return $q.all(this.getAllPromise());
        };
        PromiseWithFunction.prototype.getAtIndex = function getAtIndex(index) {
            return this.promiseWithFunctionAll[index];
        };
        PromiseWithFunction.prototype.addMultiPromise = function (promises) {
            var _this = this;
            angular.forEach(promises.promiseWithFunctionAll, function (promise, key) {
                _this.promiseWithFunctionAll.push(promise);
            });
            return this;
        };

        return PromiseWithFunction;
    });

promiseWithFunctionAndTimeout.factory('PromiseWithFunctionCheckStatus',
    function ($q, PromiseWithFunction, $timeout) {
        var promiseWithFunctionCheckStatus = angular.extend(PromiseWithFunction);
        promiseWithFunctionCheckStatus.prototype.checkPromiseAll = function () {
            return $q.all(this.getAllPromise()).then(
                function resolve(datas) {
                    var count_status = datas.map(
                        function (data) {
                            return !angular.isNumber(data.status) ? 1 : 1 == data.status ? 1 : 0;
                        }).reduce(function (a, b) { return a + b; });

                    if (datas.length != count_status) {
                        return $q.reject(datas);
                    } else {
                        return $q.resolve(datas);
                    }
                },
                function reject(datas) {
                    return $q.reject(datas);
                }
            );
        };
        promiseWithFunctionCheckStatus.prototype.runPromiseDefault = function runDefault(_scope) {
            var _this = this;
            this.checkPromiseAll().then(
                function resolve(datas) {
                    _scope.loadObject.message = "Prepare...";

                    angular.forEach(datas, function (data, key) {
                        _this.getAtIndex(key).success(data);
                    });

                    $timeout(function () { _scope.loadObject.status = false }, 25);
                },
                function reject(data) {
                    _scope.loadObject.message = "Error:" + angular.toJson(data);
                    console.error({ Error: data });
                    $timeout(function () {
                        _scope.loadObject.message = "Close...";
                        _scope.loadObject.status = false;
                    }, 500);
                }
            );
        };

        var PromiseOverride = function ($_scope) {
            var PromiseOverride = function (_this, _scope) {
                this.__this = _this;
                this._resolve = function (datas) {
                    _scope.loadObject.message = "Prepare...";

                    angular.forEach(datas, function (data, key) {
                        _this.getAtIndex(key).success(data);
                    });

                    $timeout(function () { _scope.loadObject.status = false }, 25);
                };
                this._reject = function (datas) {
                    _scope.loadObject.message = "Error:" + angular.toJson(datas);
                    console.error({ Error: datas });
                    $timeout(function () {
                        _scope.loadObject.message = "Close...";
                        _scope.loadObject.status = false;
                    }, 500);
                }
            }
            PromiseOverride.prototype.execute = function () {
                return this.__this.checkPromiseAll().then(this._resolve, this._reject);
            };
            PromiseOverride.prototype.setResolve = function (fnResolve) {
                this._resolve = fnResolve;
                return this;
            }
            PromiseOverride.prototype.setReject = function (fnReject) {
                this._reject = fnReject;
                return this;
            }

            return new PromiseOverride(this, $_scope);
        };

        promiseWithFunctionCheckStatus.prototype.PromiseOverride = PromiseOverride;

        return promiseWithFunctionCheckStatus;
    });