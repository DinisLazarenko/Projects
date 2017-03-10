app.controller("ownersCtrl", function ($scope, $http, $resource, baseUrl) {

    $scope.currentPage = 0;
    $scope.ownersResource = $resource(baseUrl + "owners");
    $scope.owners = [];
    $scope.pagedItems = [];
    $scope.limitValue = 5;
    $scope.limitRange = [5, 10, 15];

    $scope.getNumber = function (num) {
        return new Array(num);
    }

    function refresh() {
        $scope.owners = $scope.ownersResource.query();
        $scope.groupToPages();
    }

    $scope.changePage = function (pageNumber) {
        $scope.currentPage = pageNumber;
    }

    $scope.prevPage = function () {
        if ($scope.currentPage != 0) {
            $scope.currentPage--;
        }
    }

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.numberOfPages){
            $scope.currentPage++;
        }
    }

    $scope.groupToPages = function () {
        $scope.pagedItems = [];
        for (var i = 0; i < $scope.owners.length; i++) {
            if (i % $scope.limitValue === 0) {
                $scope.pagedItems[Math.floor(i / $scope.limitValue)] = [$scope.owners[i]];
            }
            else {
                $scope.pagedItems[Math.floor(i / $scope.limitValue)].push($scope.owners[i]);
            }
        }
    }

    $scope.range = function (start, end) {
        var ret = [];
        if (!end) {
            end = start;
            start = 0;
        }
        for (var i = start; i < end; i++) {
            ret.push(i);
        }
        return ret;
    }

    $scope.setPage = function () {
        $scope.currentPage = this.pageNumber;
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