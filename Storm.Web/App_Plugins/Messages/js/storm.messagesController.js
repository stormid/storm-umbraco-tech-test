function messagesController($scope, messagesResource, $http, $q, $location, appState, treeService, notificationsService, userService, historyService, updateChecker, navigationService, eventsService, tmhDynamicLocale, localStorageService, editorService, overlayService, assetsService) {
    $scope.messages = [];
    $scope.model = {};
    $scope.options = {};
    $scope.options.includeProperties = [
        //{ alias: "firstName", header: "FirstName" },
        //{ alias: "lastName", header: "LastName"},
        { alias: "emailAddress", header: "Email Address"},
        { alias: "message", header: "Message Content"}
    ];
    $scope.model.title = "Contact Us Message Requests";
    $scope.sort = sort;
    $scope.isSortDirection = isSortDirection;
    $scope.selectItem = selectItem;
    $scope.clickItem = clickItem;

    function init() {
        loadMessages();
        $scope.loading = false;
    }

    function loadMessages() {
        $scope.loading = true;
        messagesResource.getMessages()
            .then(function (response) {
                if (response !== null && response !== undefined) {
                    $scope.messages = response.map(function (item, index) {
                        return {
                            icon: "icon-document",
                            //firstName: item.FirstName,
                            //lastName: item.LastName,
                            name: item.FirstName + ' ' + item.LastName,
                            emailAddress: item.EmailAddress,
                            message: item.Message
                        }
                    });
                }
                else {
                    $scope.model.value = null;
                    $scope.prouducts = null;
                }
                $scope.loading = false;
            });
    }

    function isSortDirection(col, direction) {

    }

    function sort(field, allow, isSystem) {

    }

    function selectItem(selectedItem, $index, $event) {
        alert("select node");
    }
    function clickItem(item) {
        alert(item.emailAddress);
    }

    init();
};
angular.module("umbraco").controller("storm.messagesController", messagesController);