app.controller("HomeCtrl", function ($scope, $mdSidenav, $mdDialog, $http) {
    splash.css("display", "none");  // hides the loader
    $scope.servername = window.localStorage.getItem("ServerIPAddress") === null ? "http://" : window.localStorage.getItem("ServerIPAddress"); // gets service server IP or Address
    //initial values for counter
    $scope.CounterValue = 0;
    $scope.TotalIncreasmentCalls = 0;
    $scope.MemoryAddress = "0x"; 
    // if Server address previously saved then we can get last counter.
    
    $scope.IncreaseCounter = function () {
        if ($scope.servername !== "http://") {
            splash.css("display", "block");
            //Request parameters(headers) that is used for $http
            var Request = {
                method: 'POST',
                url: $scope.servername + "/api/SetCounter",
                headers: {
                    'Content-Type': 'application/json'
                }
            };
            $http(Request).then(
                function (response) { // If Response is 200 (OK) then we can handle return message
                    splash.css("display", "none");
                    window.localStorage.setItem("ServerIPAddress", $scope.servername);
                    //we can ask user to check last counter value.
                    var confirm = $mdDialog.confirm()
                        .title('You increased counter value.')
                        .textContent('Would you like to check last counter value?')
                        .ariaLabel('Counter Increased')
                        .ok('Yes Please.')
                        .cancel('No I Will Do It Later');

                    $mdDialog.show(confirm).then(function () {
                        $scope.GetCounter();
                    }, function () {

                    });
                }, function (response) {
                    //Showing error for response other then 200 
                    splash.css("display", "none");
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(true)
                            .title('Error')
                            .textContent(response)
                            .ariaLabel('Error')
                            .ok('Close')
                    );
                });
        }
    };
    //function to reset counter
    $scope.ResetCounter = function () {
        if ($scope.servername !== "http://") {
            var confirm = $mdDialog.confirm()
                .title('Counter Will Reset')
                .textContent('Would you like to reset counter value and put it on initial value of 0?')
                .ariaLabel('Counter will reset')
                .ok('Yes')
                .cancel('No');
            $mdDialog.show(confirm).then(function () {
               
                splash.css("display", "block");
                var Request = {
                    method: 'POST',
                    url: $scope.servername + "/api/ResetCounter",
                    headers: {
                        'Content-Type': 'application/json'
                    }
                };
                $http(Request).then(
                    function (response) { // If Response is 200 (OK) then we can handle return message
                        splash.css("display", "none");
                        window.localStorage.setItem("ServerIPAddress", $scope.servername);
                        $scope.CounterValue = 0;
                        $scope.TotalIncreasmentCalls = 0;
                        $scope.MemoryAddress = "0x";
                    }, function (response) {
                        //Showing error for response other then 200 
                        splash.css("display", "none");
                        $mdDialog.show(
                            $mdDialog.alert()
                                .clickOutsideToClose(true)
                                .title('Error')
                                .textContent(response)
                                .ariaLabel('Error')
                                .ok('Close')
                        );
                    });
            }, function () {

            });
        }
    };
    
    $scope.GetCounter = function () {
        if ($scope.servername !== "http://") {
            splash.css("display", "block");
            //Request parameters(headers) that is used for $http
            var Request = {
                method: 'GET',
                url: $scope.servername + "/api/getcounter",
                headers: {
                    'Content-Type': 'application/json'
                }
            };
            $http(Request).then(
                function (response) { // If Response is 200 (OK) then we can handle return message
                    //setting up values from response
                    window.localStorage.setItem("ServerIPAddress", $scope.servername);
                    $scope.CounterValue = response.data.counterValue;
                    $scope.TotalIncreasmentCalls = response.data.increasmentCalls;
                    $scope.MemoryAddress = response.data.memoryAddress;
                    splash.css("display", "none");
                }, function (response) {
                    //Showing error for response other then 200 
                    splash.css("display", "none");
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(true)
                            .title('Error')
                            .textContent(response)
                            .ariaLabel('Error')
                            .ok('Close')
                    );
                });
        }
    };
    
});
 