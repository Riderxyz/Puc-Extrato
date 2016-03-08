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
})

.controller('ResumoCtrl', ['$scope','$state', 'UsuarioSrv', 'UrlServicoSrv', function($scope, $state,  UsuarioSrv, UrlServicoSrv) {
  $scope.descricao = UsuarioSrv.descricaoLancamento();
  $scope.data = UsuarioSrv.dataLancamento();
  $scope.receita = UsuarioSrv.receitaLancamento();
  $scope.despesa = UsuarioSrv.despesaLancamento();
  $scope.saldo = UsuarioSrv.saldoLancamento();
  $scope.voltar = function(){alert(1);
    $ionicHistory.goBack(-2);
  }
}])

.controller('ExtratoCtrl', ['$scope', '$state','$http', '$location',  'UsuarioSrv', 'UrlServicoSrv', function($scope,$state, $http, $location,  UsuarioSrv, UrlServicoSrv) {
  var d = new Date();
  var dt = new Date(d.getFullYear(), d.getMonth(), 0);
  $scope.NomeProjeto = UsuarioSrv.getNomeProjeto();
  $scope.df = dt.getFullYear() + "-" + dt.getMonth() + "-" + dt.getDate();
  $scope.di = "2015" + "-" + dt.getMonth() + "-1";
  $http.get(UrlServicoSrv.getCodigo('sim') + "/api/extratos?projeto=" + UsuarioSrv.getProjeto() + "&di=" + $scope.di + "&df=" + $scope.df).then(function(data) {
    $scope.extratos = JSON.parse(data.data);
  })

  $scope.envioEmail = function() {
    alert("Processo de envio de email em desenvolvimento!")
  }

  $scope.verLancamento = function(descricao, data, receita, despesa, saldo) {

    UsuarioSrv.descricaoLancamento(descricao);
    UsuarioSrv.dataLancamento(data);
    UsuarioSrv.receitaLancamento(receita);
    UsuarioSrv.despesaLancamento(despesa);
    UsuarioSrv.saldoLancamento(saldo);
    $state.go('resumolancamento');
  }
}])

.controller('LoginCtrl', ['$scope', '$state','$http', '$location', 'UsuarioSrv', 'UrlServicoSrv', function($scope, $state, $http, $location, UsuarioSrv, UrlServicoSrv) {
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
        $state.go("tab");  
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

;