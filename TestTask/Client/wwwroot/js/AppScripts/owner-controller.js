app.controller("ownersCtrl", function ($scope, $http, $resource, baseUrl) {

    $scope.ownersResource = $resource(baseUrl + $scope.currentView);

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