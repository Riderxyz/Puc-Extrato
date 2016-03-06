// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.services' is found in services.js
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'ionic-datepicker', 'starter.controllers', 'starter.services'])

.service('UsuarioSrv', [function Usuario(codigo) {
  var Usuario = this;

  Usuario.setCodigo = function(codigo) {
    Usuario.codigo = codigo
  }

  Usuario.setProjeto = function(projeto) {
    Usuario.projeto = projeto
  }

  Usuario.getCodigo = function() {
    return Usuario.codigo
  }

  Usuario.getProjeto = function() {
    return Usuario.projeto
  }

  Usuario.getNomeProjeto = function() {
    return Usuario.Nomeprojeto
  }

  Usuario.setNomeProjeto = function(Nomeprojeto) {
    Usuario.Nomeprojeto = Nomeprojeto
  }

  usuario.dataLancamento = function(data){
    if (typeof data === "undefined"){
      return usuario.data
    }
    else
    {
      usuario.data = data
    }
  }

  usuario.descricaoLancamento = function(descricao){
    if (typeof descricao === "undefined"){
      return usuario.descricaoLancamento
    }
    else
    {
      usuario.descricaoLancamento = descricao
    }
  }

  usuario.receitaLancamento = function(receita){
    if (typeof receita === "undefined"){
      return usuario.receitaLancamento
    }
    else
    {
      usuario.receitaLancamento = receita
    }
  }

  usuario.despesaLancamento = function(despesa){
    if (typeof despesa === "undefined"){
      return usuario.despesaLancamento
    }
    else
    {
      usuario.despesaLancamento = despesa
    }
  }

  usuario.saldoLancamento = function(saldo){
    if (typeof saldo === "undefined"){
      return usuario.saldoLancamento
    }
    else
    {
      usuario.saldoLancamento = saldo
    }
  }

}])

.service('UrlServicoSrv', [function servico(emProducao) {
  var servico = this;
  var prod = "nao"
  servico.getCodigo = function(emProducao) {
    if (prod == 'sim') {
      return 'http://139.82.24.10/MobServ'
    } else {
      return 'http://localhost:19017'
    }
  }

}])

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

.controller('LoginCtrl', ['$scope', '$http', '$location', 'UsuarioSrv', 'UrlServicoSrv', function($scope, $http, $location, UsuarioSrv, UrlServicoSrv) {
  $scope.data = {};
  $scope.data.username = '2387';
  $scope.data.password = '123456';
  //$rootScope.usuario = 'usuario dentro do controler';
  $scope.data.errormessage = 'Informe usuario e senha';
  $scope.login = function() {
    $http.get(UrlServicoSrv.getCodigo('sim') + '/api/usuarios?_usuario=' + $scope.data.username + '&_senha=' + $scope.data.password).then(function(data) {
      $scope.coordenador = JSON.parse(data.data[0]).tab1[0];
      UsuarioSrv.setCodigo($scope.coordenador.coordenador);
      if ($scope.coordenador.coordenador == $scope.data.username) {
        $scope.errormessage = ' ';
        $location.path("/tab");
      } else {
        $scope.data.errormessage = 'Usuario ou senha inválidos';
      }
    })
  }
}])

.controller('ProjetoCtrl', ['$scope', '$state', '$http', '$location', 'UsuarioSrv', 'UrlServicoSrv', function($scope, $state, $http, $location, UsuarioSrv, UrlServicoSrv) {
  var d = new Date();
  $scope.parmdata = d.getFullYear() + "-" + d.getMonth() + "-" + d.getDate()
  $http.get(UrlServicoSrv.getCodigo('sim') + "/api/projetos?coordenador=" + UsuarioSrv.getCodigo() + "&data=" + $scope.parmdata).then(
    function(data) {
      $scope.projetos = JSON.parse(data.data);
    }
  )

  $scope.gotoExtrato = function(projeto, nomeprojeto) {
    UsuarioSrv.setProjeto(projeto);
    UsuarioSrv.setNomeProjeto(nomeprojeto);
    $state.go("tab.extrato");
  }

}]) 

.controller('ExtratoCtrl', ['$scope', '$http', '$location', 'UsuarioSrv', 'UrlServicoSrv', function($scope, $http, $location, UsuarioSrv, UrlServicoSrv) {
  var d = new Date();
  var dt = new Date(d.getFullYear(), d.getMonth(), 0);
  $scope.NomeProjeto = UsuarioSrv.getNomeProjeto();
  $scope.df = dt.getFullYear() + "-" + dt.getMonth() + "-" + dt.getDate();
  $scope.di = "2015" + "-" + dt.getMonth() + "-1";

  $http.get(UrlServicoSrv.getCodigo('sim') + "/api/extratos?projeto=" + UsuarioSrv.getProjeto() + "&di=" + $scope.di + "&df=" + $scope.df).then(function(data) {
    $scope.extratos = JSON.parse(data.data);

  })

  $scope.envioEmail = function(){
    alert("Processo de envio de email em desenvolvimento!")
  }

  $scope.verLancamento = function(descricao, data){
      $scope.descricao(descricao);
  $scope.data(data);
  $scope.receita(receita);
  $scope.despesa(despesa);
  $scope.saldo(saldo);
    //ngDialog.open({ template: '<h3>{{descricao}}</h3>"', plain: true, className: 'ngdialog-theme-default' });
  }

}])

.controller('ResumoCtrl', ['$scope', '$location', 'UsuarioSrv', 'UrlServicoSrv', function ($scope, $location, UsuarioSrv, UrlServicoSrv) {
  $scope.descricao = UsuarioSrv.descricaoLancamento();
  $scope.data = UsuarioSrv.dataLancamento();
  $scope.receita = UsuarioSrv.receitaLancamento();
  $scope.despesa = UsuarioSrv.despesaLancamento();
  $scope.saldo = UsuarioSrv.saldoLancamento();
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
      cache: false,
      templateUrl: 'templates/login.html',
      controller: 'LoginCtrl'
    })

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

  .state('ResumoLancamento', {
      url: '/resumoLancamento',
      cache: false,
      templateUrl: 'templates/resumo-lancamento.html',
      controller: 'ResumoCtrl'
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