app.controller("baseCtrl", function ($scope) {
    $scope.currentView = "owners";
    $scope.owner = {};

    $scope.setCurrentView = function (currentView) {
        $scope.currentView = currentView;
    }

    $scope.setOwner = function (owner) {
        $scope.owner = owner;
    }
})