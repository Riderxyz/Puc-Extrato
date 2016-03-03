// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.services' is found in services.js
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'ionic-datepicker', 'starter.controllers', 'starter.services'])

.run(function($ionicPlatform) {
  $ionicPlatform.ready(function($scope) {
    if (window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
      cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
      cordova.plugins.Keyboard.disableScroll(true);

    }
    if (window.StatusBar) {
      // org.apache.cordova.statusbar required
      StatusBar.styleDefault();
    }
  });
})

.controller('LoginCtrl', ['$scope','$http','$location', function($scope, $http, $location) 
 {
    $scope.data = {};
    $scope.data.username = '2387';
    $scope.data.password = '123456';
    $scope.data.errormessage = 'Informe usuario e senha';
    $scope.login = function()
      {
        $http.get('http://localhost:19017/api/usuarios?_usuario='+$scope.data.username+'&_senha='+$scope.data.password).then(function(data)
        { 
          //console.log(JSON.parse(data.nome);
          $scope.coordenador = JSON.parse(data.data[0]).tab1[0];
          if ($scope.coordenador.coordenador == $scope.data.username){
            $scope.errormessage = ' ';
            $location.path( "/tab" ); 
          }
          else
          {
            $scope.data.errormessage = 'Usuario ou senha inválidos';            
          }
        })   
      }; 
    }])



.config(function($stateProvider, $urlRouterProvider) {

  // Ionic uses AngularUI Router which uses the concept of states
  // Learn more here: https://github.com/angular-ui/ui-router
  // Set up the various states which the app can be in.
  // Each state's controller can be found in controllers.js
  $stateProvider

  // setup an abstract state for the tabs directive
    .state('tab', {
    url: '/tab',
    templateUrl: 'templates/tabs.html'
  })

  // Each tab has its own nav history stack:
 .state('login', {
      url: '/login',
      templateUrl: 'templates/login.html',
      controller:'LoginCtrl'
  })
  .state('tab.dash', {
    url: '/dash',
    views: {
      'tab-dash': {
        templateUrl: 'templates/tab-dash.html',
        controller: 'DashCtrl'
      }
    }
  })

  .state('tab.projetos', {
    url: '/projetos',
    views: {
      'tab-projetos': {
        templateUrl: 'templates/tab-projetos.html',
        controller: 'ProjetoCtrl'
      }
    }
  })


  .state('tab.chats', {
      url: '/chats',
      views: {
        'tab-chats': {
          templateUrl: 'templates/tab-chats.html',
          controller: 'ChatsCtrl'
        }
      }
    })
    .state('tab.chat-detail', {
      url: '/chats/:chatId',
      views: {
        'tab-chats': {
          templateUrl: 'templates/chat-detail.html',
          controller: 'ChatDetailCtrl'
        }
      }
    })

  .state('tab.account', {
    url: '/account',
    views: {
      'tab-account': {
        templateUrl: 'templates/tab-account.html',
        controller: 'AccountCtrl'
      }
    }
  });

  // if none of the above states are matched, use this as the fallback
  $urlRouterProvider.otherwise('login'); 

});