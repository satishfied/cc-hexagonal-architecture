angular.module('recruitingApp', [])
    .controller('screeningsController', [
        '$scope', '$http', function ($scope, $http) {
            $scope.screening = {};
            $scope.id = 0;

            $scope.createScreening = function () {
                $http.post('http://localhost:64677/api/screenings/', $scope.screening)
                    .then(function (response) {
                        var id = response.data;

                        $scope.id = id;
                    });
            };
        }
    ]);
