angular.module("umbraco").controller("ColoredTag.controller", function ($scope) {

    if ($scope.model.value === "") {
        $scope.model.value = {};
        $scope.model.value.tags = [];
    }

    $scope.newTag = {
        'color': "default",
        'title': ""
    };
    
    $scope.availableColors = [
        { name: "grey", value: "default" },
        { name: "blue", value: "primary" },
        { name: "sky", value: "info" },
        { name: "green", value: "success"},
        { name: "yellow", value: "warning" },
        { name: "red", value: "danger" }
        ];
    $scope.showEmptyWarning = false;


    $scope.pushTag = function () {
        if ($scope.newTag.title === "") {
            $scope.showEmptyWarning = true;
            return;
        } else {
            $scope.showEmptyWarning = false;
        }

        var pushedTag = {
            'title': $scope.newTag.title,
            'color': $scope.newTag.color
        };
        for (var i in $scope.model.value.tags) {
            if ($scope.model.value.tags[i].title === pushedTag.title) {
                $scope.model.value.tags[i].title = pushedTag.title;
                $scope.model.value.tags[i].color = pushedTag.color;
                $scope.newTag.color = "default";
                $scope.newTag.title = "";
                return;
            }
        }

        $scope.model.value.tags.push(pushedTag);
        $scope.newTag.color = "default";
        $scope.newTag.title = "";
    };

    $scope.deleteTag = function (tagTitle) {
        for (var i in $scope.model.value.tags) {
            if ($scope.model.value.tags[i].title === tagTitle) {
                $scope.model.value.tags.splice(i, 1);
                return;
            }
        }
    };
});