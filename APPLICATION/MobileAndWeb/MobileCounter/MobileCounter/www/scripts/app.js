var app = angular.module("MobileCounter", ["ngRoute",  "ngMaterial"]);
var splash = angular.element(document.querySelector('.loader-wrapper'));
 
app.run(['$window', function ($window) {
    $window.onload = function () {
        splash = angular.element(document.querySelector('.loader-wrapper'));
        splash.css("display", "none");
    };
}]);
app.config(function ($routeProvider, $mdThemingProvider, $mdGestureProvider) {
    $routeProvider.otherwise({
        redirectTo: '/home'
    });
    $routeProvider.when("/home", {
        controller: "HomeCtrl",
        templateUrl: "views/home.html"
    });
    $mdThemingProvider.theme('default').primaryPalette('blue')
        .warnPalette('blue-grey')
        .accentPalette('light-blue');
    $mdGestureProvider.skipClickHijack();
});
