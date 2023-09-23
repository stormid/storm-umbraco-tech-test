'use strict'
function messagesResource($q, $http, umbRequestHelper) {
    var apiUrl = '/Umbraco/backoffice/Api/ContactUsApi';
    return {
        getMessages: function () {
            return umbRequestHelper.resourcePromise(
                $http.get(apiUrl + '/GetAll'),
                'Error loading contact us messages.');
        }

    };
}
angular.module('umbraco.resources').factory('messagesResource', messagesResource);