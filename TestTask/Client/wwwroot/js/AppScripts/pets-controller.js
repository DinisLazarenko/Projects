app.controller("petsCtrl", function ($scope, $http, $resource, baseUrl) {
   
    //if (angular.isString($scope.owner.name)) {
    //    ownerName = angular.lowercase($scope.owner.name);
    //}
    //else {
    //    console.log("Owner name is not a string");
    //}

    
    $scope.petsResource = $resource(baseUrl + $scope.owner.name);

    function refresh() {
        $scope.pets = $scope.petsResource.query();
    }

    function Back() {
        $scope.setCurrentView("owners");
    }

    refresh();
})