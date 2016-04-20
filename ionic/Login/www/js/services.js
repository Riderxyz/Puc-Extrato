angular.module('starter.services', [])

.factory('Chats', function() {
  // Might use a resource here that returns a JSON array

  // Some fake testing data
  var chats = [{
    id: 0,
    name: 'Ben Sparrow',
    lastText: 'You on your way?',
    face: 'img/ben.png'
  }, {
    id: 1,
    name: 'Max Lynx',
    lastText: 'Hey, it\'s me',
    face: 'img/max.png'
  }, {
    id: 2,
    name: 'Adam Bradleyson',
    lastText: 'I should buy a boat',
    face: 'img/adam.jpg'
  }, {
    id: 3,
    name: 'Perry Governor',
    lastText: 'Look at my mukluks!',
    face: 'img/perry.png'
  }, {
    id: 4,
    name: 'Mike Harrington',
    lastText: 'This is wicked good ice cream.',
    face: 'img/mike.png'
  }];

  return {
    all: function() {
      return chats;
    },
    remove: function(chat) {
      chats.splice(chats.indexOf(chat), 1);
    },
    get: function(chatId) {
      for (var i = 0; i < chats.length; i++) {
        if (chats[i].id === parseInt(chatId)) {
          return chats[i];
        }
      }
      return null;
    }
  };
})

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

  Usuario.dataLancamento = function(datalan) {
    if (typeof datalan === "undefined") {
      return Usuario._datalan
    } else {
      Usuario._datalan = datalan
      return Usuario._datalan
    }
  }

  Usuario.descricaoLancamento = function(descricao) {
    if (descricao === undefined) {
      return Usuario._descricaoLancamento
    } else {
      Usuario._descricaoLancamento = descricao
      return Usuario._descricaoLancamento
    }
  }

  Usuario.receitaLancamento = function(receita) {
    if (receita === undefined) {
      return Usuario._receitaLancamento
    } else {
      Usuario._receitaLancamento = receita
    }
  }

  Usuario.despesaLancamento = function(despesa) {
    if (despesa === undefined) {
      return Usuario._despesaLancamento
    } else {
      Usuario._despesaLancamento = despesa
      return despesa
    }
  }

  Usuario.saldoLancamento = function(saldo) {
    if (saldo === undefined) {
      return Usuario._saldoLancamento
    } else {
      Usuario._saldoLancamento = saldo
      return saldo
    }
  }

}])

.service('UrlServicoSrv', [function servico(emProducao) {
  var servico = this;
  var prod = "sim"
  servico.getCodigo = function(emProducao) {
    if (prod == 'sim') {
      return 'http://139.82.24.10/MobServ'
    } else {
      return 'http://localhost:19017'
    }
  }

}])

;
