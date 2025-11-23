import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

class NovoChamadoViewModel extends ChangeNotifier {
  final _storage = const FlutterSecureStorage();

  final String ipApi = "robobertoapi-e6cgbaawhxagdwg2.brazilsouth-01.azurewebsites.net";

  bool _estaCarregando = false;
  bool get estaCarregando => _estaCarregando;

  String _mensagemErro = "";
  String get mensagemErro => _mensagemErro;

  void limparMensagens() {
    _mensagemErro = "";
  }

  Future<bool> criarNovoChamado({
    required String categoria,
    required int prioridade,
    required String titulo,
    required String descricao,
  }) async {
    _estaCarregando = true;
    _mensagemErro = "";
    notifyListeners();

    if (titulo.isEmpty || descricao.isEmpty) {
      _mensagemErro = "Título e Descrição são obrigatórios.";
      _estaCarregando = false;
      notifyListeners();
      return false;
    }

    final token = await _storage.read(key: 'jwt_token');
    if (token == null || token.isEmpty) {
      _mensagemErro = "Utilizador não autenticado. Faça login novamente.";
      _estaCarregando = false;
      notifyListeners();
      return false;
    }

    final Map<String, String> headers = {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer $token',
    };

    final Map<String, dynamic> body = {
      "categoria": {"nome": categoria},
      "prioridade": prioridade,
      "titulo": titulo,
      "descricao": descricao
    };

    try {
      final url = Uri.parse('https://$ipApi/Chamados');
      final response = await http
          .post(
        url,
        headers: headers,
        body: jsonEncode(body),
      )
          .timeout(const Duration(seconds: 10));

      if (response.statusCode == 201 || response.statusCode == 200) {
        _estaCarregando = false;
        notifyListeners();
        return true;
      } else if (response.statusCode == 401) {
        _mensagemErro = "Sessão expirada. Faça login novamente.";
      } else {
        _mensagemErro = "Erro ao criar chamado: ${response.statusCode}";
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
}