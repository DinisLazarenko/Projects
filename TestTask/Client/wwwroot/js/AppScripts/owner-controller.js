app.controller("ownersCtrl", function ($scope, $http, $resource, baseUrl) {

    $scope.pagedItems = [];
    $scope.currentPage = 0;
    $scope.ownersResource = $resource(baseUrl + "owners");
    $scope.limitValue = 5;
    $scope.limitRange = [5, 10, 15];

    function refresh() {
        $scope.owners = $scope.ownersResource.query();
    }

    //create new owner
    $scope.create = function (owner) {
        new $scope.ownersResource(owner).$save().then(function (ownerAdded) {
            $scope.owners.push(ownerAdded);
        });
    }

    //show selected owner pets 
    $scope.showPets = function (owner) {
        $scope.setOwner(owner);
        $scope.setCurrentView("pets");
    }

    refresh();
})