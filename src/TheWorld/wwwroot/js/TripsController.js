// tripsController.js
(function () {

    "use strict";

    // Getting the existing module
    angular.module("app-trips")
    .controller("tripsController", tripsController);

    function tripsController($http) {
        
        var vm = this;

        vm.trips = [];
        vm.errorMessage = "";
        vm.isBusy = true;


        $http.get("/api/trips")
         .then(function (response) {
             //success
             angular.copy(response.data, vm.trips);
         },
         function (error) {
             //failure
             vm.errorMessage = "Failed to load data " + error;
         })
        .finally(function () {
            //last
            vm.isBusy = false;

        });
        
        vm.newTrip = {};

        vm.addTrip = function () {
            vm.trips.push({ name: vm.newTrip.name, created: new Date() });
            vm.newTrip = ({});
        };
    }

})();