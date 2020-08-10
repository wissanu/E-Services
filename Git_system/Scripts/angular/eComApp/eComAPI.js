'use strict';

/* eComAPI */

var eComAPI = angular.module('eComAPI', ['ngResource']);

eComAPI.factory('Product_CRUD_API', ['$resource',
    function ($resource) {
        return $resource('/api/Product/:id', { id: '@id' }, {
            query: { method: 'GET' }
        });
    }]);

eComAPI.factory('Category_CRUD_API', ['$resource',
    function ($resource) {
        return $resource('/api/Category/:id', { id: '@id' }, {
            query: { method: 'GET' }
        });
    }]);

eComAPI.factory('CategoryHomePage_CRUD_API', ['$resource',
    function ($resource) {
        return $resource('/api/CategoryHomePage/', {}, {
            query: { method: 'GET' }
        });
    }]);

eComAPI.factory('Order_CRUD_API', ['$resource',
    function ($resource) {
        return $resource('/api/Order/:id', { id: '@id' }, {
            query: { method: 'GET' },
            check: { method: 'POST', params: { check: true } }
        });
    }]);

eComAPI.factory('Profile_CRUD_API', ['$resource',
    function ($resource) {
        return $resource('/api/Profile/', {}, {
            query: { method: 'GET' },
            login: { method: 'POST' }
        });
    }]);

eComAPI.factory('DeliverType_CRUD_API', ['$resource',
    function ($resource) {
        return $resource('/api/DeliverType/', {}, {
            query: { method: 'GET' }
        });
    }]);

eComAPI.factory('BankType_CRUD_API', ['$resource',
    function ($resource) {
        return $resource('/api/BankType/', {}, {
            query: { method: 'GET' }
        });
    }]);

eComAPI.factory('Slideshow_CRUD_API', ['$resource',
    function ($resource) {
        return $resource('/api/Slideshow/', {}, {
            query: { method: 'GET' }
        });
    }]);

eComAPI.factory('PaymentType_CRUD_API', ['$resource',
    function ($resource) {
        return $resource('/api/PaymentType/', {}, {
            query: { method: 'GET' }
        });
    }]);

eComAPI.factory('Legal_CRUD_API', ['$resource',
    function ($resource) {
        return $resource('/api/Legal/', {}, {
            query: { method: 'GET' }
        });
    }]);

eComAPI.factory('EletronicProduct_CRUD_API', ['$resource',
    function ($resource) {
        return $resource('/api/ElectronicFilesHistory/', {}, {
            query: { method: 'GET' }
        });
    }]);