angular.module('todo', [])
    .service('Connector', [
        '$http',
        function ($http) {
            return {
                CheckId: function (id) {
                    return $http.get('/Home/CheckId/' + id);
                },
                CreateUser: function () {
                    return $http.get('/Home/CreateUser/');
                },
                Logout: function (id) {
                    return $http.get('/Home/Logout/' + id);
                },
                getAll: function () {
                    return $http.get('/api/Task');
                },
                Save: function (data) {
                    return $http.post('/api/Task', data);
                },
                Find: function (id) {
                    return $http.get('/api/Task/' + id);
                },
                Delete: function (id) {
                    return $http.delete('/api/Task/' + id);
                },
                Change: function (id, data) {
                    return $http.put('/api/Task/' + id, data);
                }
            }
        }
    ])
    .controller('ctrl', [
    '$scope',
    '$window',
    'Connector',
    function ($scope, $window, Connector) {
        $scope.Logout = function () {
            Connector.Logout($scope.userId).then(function () {
                $window.location.href = '/Home/Index'
            })
        };
        $scope.AddTask = function () {
            Connector.Save({
                Id: 0,
                UserId: $scope.userId,
                IsCompleted: false,
                Name: $scope.Text,
                Author: $scope.author
            }).then(function () { $scope.update() });
        };
        $scope.update = function () {
            Connector.getAll().then(function (responce) {
                $scope.tasks = [];

                responce.data.forEach(function (task, i, array) {
                    $scope.tasks.push({
                        taskId: task.ToDoId,
                        taskText: task.Name,
                        taskCompleted: task.IsCompleted
                    });
                });
            })
        };
        $scope.Delete = function (id) {
            Connector.Delete(id).then(function () {
                $scope.update()
            });
        };
        $scope.update();
    }])
.controller('ctrl2', [
    '$scope',
    '$window',
    'Connector',
    function ($scope, $window, Connector) {
        $scope.CheckId = function (id) {
            Connector.CheckId(id).then(function (response) {
                $window.location.href = '/Home/ToDoList/' + response.data.id
            },
                function () { $scope.LoginId = '' });
        };

        $scope.CreateUser = function () {
            Connector.CreateUser().then(function (response) {
                $window.location.href = '/Home/ToDoList/' + response.data.id;
            },
                function (response) { $scope.LoginId = '' });
        }
    }])