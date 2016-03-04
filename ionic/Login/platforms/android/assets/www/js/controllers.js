angular.module('starter.controllers', [])
.controller('DashCtrl', function($scope) {})

.controller('ProjetoCtrl', function($scope) {

  $scope.currentDate = new Date();
  $scope.currentDate2 = new Date();
  $scope.minDate = new Date(2015, 6, 1);
  $scope.maxDate = new Date(2016, 6, 31);


  $scope.items = [{
    title: "Item 1"
  }, {
    title: "Item 2"
  }, {
    title: "Item 3"
  }, {
    title: "Item 4"
  }, {
    title: "Item 5"
  }, ]

  $scope.editItem = function(item) {
    item.title = "Edited Item"
  }

  $scope.datePickerCallback = function(val) {
    if (!val) {
      console.log('Date not selected');
    } else {
      alert(val);
    }
  };
})

.controller('ChatsCtrl', function($scope, Chats) {
  // With the new view caching in Ionic, Controllers are only called
  // when they are recreated or on app start, instead of every page change.
  // To listen for when this page is active (for example, to refresh data),
  // listen for the $ionicView.enter event:
  //
  //$scope.$on('$ionicView.enter', function(e) {
  //});

  $scope.chats = Chats.all();
  $scope.remove = function(chat) {
    Chats.remove(chat);
  };
})

.controller('ChatDetailCtrl', function($scope, $stateParams, Chats) {
  $scope.chat = Chats.get($stateParams.chatId);
})

.controller('AccountCtrl', function($scope) {
  $scope.settings = {
    enableFriends: true
  };
});