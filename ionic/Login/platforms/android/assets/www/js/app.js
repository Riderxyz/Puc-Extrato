// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.services' is found in services.js
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'ionic-datepicker', 'starter.controllers', 'starter.services', 'ui.router'])

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
    cache: false,
    templateUrl: 'templates/login.html',
    controller: 'LoginCtrl'
  })

  .state('resumolancamento', {
    url: '/resumolancamento',
    cache: false,
    templateUrl: 'templates/tab-resumolancamento.html',
     controller: 'ResumoCtrl'
    
    })
/*
    .state('tab.resumolancamento', {
    url: '/resumolancamento',
    cache: false,
    views: {
      'tab-resumolancamento': {
        templateUrl: 'templates/tab-resumolancamento.html',
        controller: 'ResumoCtrl'
      }
    }
  })
*/
  .state('tab.extrato', {
    url: '/extrato',
    cache: false,
    views: {
      'tab-extrato': {
        templateUrl: 'templates/tab-extrato.html',
        controller: 'ExtratoCtrl'
      }
    }
  })

  .state('tab.projetos', {
    url: '/projetos',
    cache: false,
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
        templateUrl: 'templates/tab-resumolancamento.html',
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
  })

  $urlRouterProvider.otherwise('/login');

});