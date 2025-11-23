import 'dart:async';
import 'dart:convert'; // Para JSON
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http; // Para requisições HTTP
import '../Model/chamado.dart';
import 'package:intl/intl.dart';

class UserHomePageViewModel extends ChangeNotifier {
  final _storage = const FlutterSecureStorage();

  bool _estaCarregando = false;

  bool get estaCarregando => _estaCarregando;

  String _mensagemErro = "";

  String get mensagemErro => _mensagemErro;

  List<Chamado> _chamados = [];
  List<Chamado> get chamados => _chamados;

  var ipApi = "robobertoapi-e6cgbaawhxagdwg2.brazilsouth-01.azurewebsites.net";

  void FazerLogout(BuildContext context) async {
    _storage.deleteAll();

    Navigator.pop(context);
  }

  Future<void> buscarMeusChamados() async {
    _estaCarregando = true;
    _mensagemErro = "";
    notifyListeners();

    final token = await _storage.read(key: 'jwt_token');

    if (token == null || token.isEmpty) {
      _mensagemErro = 'Utilizador não autenticado.';
      _estaCarregando = false;
      notifyListeners();
      return;
    }

    final Map<String, String> headers = {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer $token',
    };
    
    try {
      final url = Uri.parse('https://$ipApi/Chamados/meus?paginaAtual=0&tamanho=999');
      final response = await http
          .get(
        url,
        headers: headers,
      )
          .timeout(const Duration(seconds: 10));

      if (response.statusCode == 200) {
        _chamados = chamadoFromJson(response.body);
      } else if (response.statusCode == 401) {
        _mensagemErro = 'Sessão expirada. Faça login novamente.';
      } else {
        _mensagemErro = 'Erro ao buscar chamados: ${response.statusCode}';
      }
    } on TimeoutException catch (_) {
      _mensagemErro = 'O servidor demorou muito para responder.';
    } on SocketException catch (_) {
      _mensagemErro = 'Sem conexão com a internet.';
    } catch (e) {
      _mensagemErro = 'Ocorreu um erro inesperado: $e';
    }

    _estaCarregando = false;
    notifyListeners();
  }

  void limparMensagens() {
    _mensagemErro = "";
  }
}
