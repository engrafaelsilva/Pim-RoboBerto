import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;
import 'package:roboberto_ia/Model/chamado.dart';

class ChamadoDetalheViewModel extends ChangeNotifier {
  final _storage = const FlutterSecureStorage();
  final String ipApi = "192.168.0.28";

  bool _estaCarregando = false;
  bool get estaCarregando => _estaCarregando;

  String _mensagemErro = "";
  String get mensagemErro => _mensagemErro;

  void limparMensagens() {
    _mensagemErro = "";
  }

  Future<bool> responderSugestao(String chamadoId, bool resolveu) async {
    _estaCarregando = true;
    _mensagemErro = "";
    notifyListeners();

    final token = await _storage.read(key: 'jwt_token');
    if (token == null || token.isEmpty) {
      _mensagemErro = "Utilizador não autenticado.";
      _estaCarregando = false;
      notifyListeners();
      return false;
    }

    final Map<String, String> headers = {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer $token',
    };

    final body = jsonEncode(resolveu);
    final url = Uri.parse(
        'http://$ipApi:5129/Chamados/$chamadoId/alterar-resolveu-sugestao');

    try {
      final response = await http
          .patch(url, headers: headers, body: body)
          .timeout(const Duration(seconds: 10));

      if (response.statusCode == 200 || response.statusCode == 204) {
        _estaCarregando = false;
        notifyListeners();
        return true;
      } else if (response.statusCode == 401) {
        _mensagemErro = "Sessão expirada.";
      } else {
        _mensagemErro = "Erro ${response.statusCode} ao responder.";
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
    return false;
  }

  Future<bool> enviarMensagem(String chamadoId, String texto) async {
    _mensagemErro = "";
    notifyListeners();

    final token = await _storage.read(key: 'jwt_token');
    if (token == null || token.isEmpty) {
      _mensagemErro = "Utilizador não autenticado.";
      notifyListeners();
      return false;
    }

    final Map<String, String> headers = {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer $token',
    };

    final body = jsonEncode({"texto": texto});
    final url = Uri.parse('http://$ipApi:5129/Chamados/$chamadoId/comentar');
    try {
      final response = await http
          .patch(url, headers: headers, body: body)
          .timeout(const Duration(seconds: 10));

      if (response.statusCode == 201 || response.statusCode == 200 || response.statusCode == 204) {
        return true;
      } else if (response.statusCode == 401) {
        _mensagemErro = "Sessão expirada.";
      } else {
        _mensagemErro = "Erro ${response.statusCode} ao enviar mensagem.";
      }
    } on TimeoutException catch (_) {
      _mensagemErro = 'O servidor demorou muito para responder.';
    } on SocketException catch (_) {
      _mensagemErro = 'Sem conexão com a internet.';
    } catch (e) {
      _mensagemErro = 'Ocorreu um erro inesperado: $e';
    }

    notifyListeners();
    return false;
  }
}