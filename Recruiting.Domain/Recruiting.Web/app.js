angular.module('recruitingApp', ['ngRoute'])
    .config(['$routeProvider',
      function ($routeProvider) {
          $routeProvider.
            when('/screenings', {
                templateUrl: 'partials/screenings.html',
                controller: 'screeningsController'
            }).
            when('/screenings/:id', {
                templateUrl: 'partials/screening.html',
                controller: 'screeningController'
            }).
            otherwise({
                redirectTo: '/screenings'
            });
      }])
    .controller('screeningsController', [
        '$scope', '$http', '$location', function ($scope, $http, $location) {
            $scope.screening = {};
            $scope.screenings = [];


            $http.get('http://localhost:64677/api/screenings/')
                .then(function (response) {
                    $scope.screenings = response.data;
                });

            $scope.createScreening = function () {
                $http.post('http://localhost:64677/api/screenings/', $scope.screening)
                    .then(function (response) {
                        var data = response.data;

                        $location.path('/screenings/' + data.Id);
                    });
            };
        }
    ])
    .controller('screeningController', [
        '$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {
            $scope.screening = {};

            $http.get('http://localhost:64677/api/screenings/' + $routeParams.id)
                .then(function (response) {
                    $scope.screening = response.data;
                });
        }
    ]);
