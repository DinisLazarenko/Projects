app.controller("petsCtrl", function ($scope, $http, $resource, baseUrl) {
   
    $scope.petsResource = $resource(baseUrl + $scope.owner.name);

    function refresh() {
        $scope.pets = $scope.petsResource.query();
    }

    $scope.back = function () {
        $scope.setCurrentView("owners");
    }

    refresh();
})