angular.module("umbraco").controller("ColoredTag.controller", function ($scope) {

    console.log($scope.model.value);
    if ($scope.model.value === "") {
        $scope.model.value = {};
        console.log($scope.model);
        $scope.model.value.tags = [];
    }
    $scope.availableColors = [
        {
            name: "grey",
            value: "default"
        },
        {
            name: "blue",
            value: "primary"
        },
        {
            name: "sky",
            value: "info",
        },
        {   name: "green",
            value: "success"
        },
        {
            name: "yellow",
            value: "warning"
        },
        {
            name: "red",
            value: "danger"
        }];
    $scope.showEmptyWarning = false;

    $scope.newTag = {
        'color': "default",
        'title': ""
    };

    $scope.pushTag = function () {
        if ($scope.newTag.title === "") {
            $scope.showEmptyWarning = true;
            return;
        }

        var pushedTag = {
            'title': $scope.newTag.title,
            'color': $scope.newTag.color
        };
        $scope.model.value.tags.push(pushedTag);
        console.log($scope.model);
        $scope.newTag.color = "default";
        $scope.newTag.title = "";
    };
});