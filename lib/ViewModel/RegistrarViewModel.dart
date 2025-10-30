import 'dart:async'; // Para TimeoutException
import 'dart:convert'; // Para JSON
import 'dart:io'; // Para SocketException
import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;
import 'package:uuid/uuid.dart'; // Para requisições HTTP

class RegistrarViewModel extends ChangeNotifier {
  final _storage = const FlutterSecureStorage();

  bool _estaCarregando = false;

  bool get estaCarregando => _estaCarregando;

  String _mensagemErro = "";

  String get mensagemErro => _mensagemErro;

  Future<bool> fazerRegistro(
      String usuario, String email, String telefone, String senha) async {
    _estaCarregando = true;
    _mensagemErro = "";
    notifyListeners();

    if (usuario.isEmpty || telefone.isEmpty || email.isEmpty || senha.isEmpty) {
      _mensagemErro = 'Preencha todos os campos';
      _estaCarregando = false;
      notifyListeners();
      return false;
    }

    try {
      var uuid = Uuid();
      String novaUuid = uuid.v4();

      DateTime agora = DateTime.now();
      String dataFormatada = agora.toIso8601String();
      final url = Uri.parse('http://10.0.2.2:5129/Usuario');
      final body = jsonEncode({
        'id': novaUuid,
        'nome': usuario,
        'email': email,
        'telefone': telefone,
        'senhaHash': senha,
        'dataCriacao': dataFormatada,
        'departamento': {'nome': 'PADRAO'},
        'roles': {
          'id': '3fa85f64-5717-4562-b3fc-2c963f66afa6',
          'nome': 'COLABORADOR'
        }
      });

      final response = await http.post(
        url,
        headers: {'Content-Type': 'application/json'},
        body: body,
      ).timeout(const Duration(seconds: 10));

      if (response.statusCode == 200 || response.statusCode == 201) {
        _estaCarregando = false;
        notifyListeners();
        return true;
      } else {
        try {
          final errorData = jsonDecode(response.body);
          _mensagemErro = errorData['message'] ?? 'Erro ao criar a conta';
        } catch (e) {
          _mensagemErro = 'Erro ao criar a conta';
        }
        _estaCarregando = false;
        notifyListeners();
        return false;
      }
    }
    on TimeoutException catch (_) {
      _mensagemErro =
      'O servidor demorou muito para responder. Tente novamente.';
      _estaCarregando = false;
      notifyListeners();
      return false;
    } on SocketException catch (_) {
      _mensagemErro = 'Sem conexão com a internet. Verifique sua rede.';
      _estaCarregando = false;
      notifyListeners();
      return false;
    } catch (e) {
      _mensagemErro = 'Ocorreu um erro inesperado: $e';
      _estaCarregando = false;
      notifyListeners();
      return false;
    }
  }

// ... (coloque aqui seus outros métodos, como fazerLogin, etc.) ...
}
