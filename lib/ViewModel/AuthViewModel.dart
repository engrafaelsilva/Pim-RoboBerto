import 'dart:async';
import 'dart:convert'; // Para JSON
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;
import 'package:uuid/uuid.dart'; // Para requisições HTTP

class AuthViewModel extends ChangeNotifier {
  final _storage = const FlutterSecureStorage();

  bool _estaCarregando = false;

  bool get estaCarregando => _estaCarregando;

  String _mensagemErro = "";

  String get mensagemErro => _mensagemErro;

  Future<bool> fazerLogin(String email, String senha) async {
    _estaCarregando = true;
    _mensagemErro = "";
    notifyListeners();

    if(email.isEmpty || email.length == 0 || senha.isEmpty || senha.length == 0){
      _mensagemErro = 'Insira o email e a senha';
      _estaCarregando = false;
      notifyListeners();
      return false;
    }


    try {
      final url = Uri.parse('http://10.117.174.6:5129/Autenticacao/login');
      final body = jsonEncode({
        'email': email,
        'senha': senha
      });

      final response = await http.post(
        url, headers: {'Content-Type': 'application/json'}, body: body,
      ).timeout(const Duration(seconds: 10));

      if (response.statusCode == 200) {
        //salva token
        final data = jsonDecode(response.body);
        final String token = data['token'];

        if(token != null && token.isNotEmpty){
          await _storage.write(key: 'jwt_token', value: token);

          _estaCarregando = false;
          notifyListeners();
          return true; // Sucesso
        } else {
          _mensagemErro = 'Resposta de login inválida. Token não encontrado.';
          _estaCarregando = false;
          notifyListeners();
          return false;
        }

      } else {
        try {
          final errorData = jsonDecode(response.body);
          _mensagemErro = errorData['message'] ?? 'Email ou senha invalidos';
        } catch (e) {
          _mensagemErro = 'Email ou senha invalidos';
        }
        _estaCarregando = false;
        notifyListeners();
        return false;
      }
    } catch (e){
      _mensagemErro = 'Erro de conexão. Tente novamente';
      _estaCarregando = false;
      notifyListeners();
      return false;
    }
  }

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
      final url = Uri.parse('http://10.117.174.6:5129/Usuario');
      final body = jsonEncode({
        'id': novaUuid,
        'nome': usuario,
        'email': email,
        'telefone': telefone,
        'senhaHash': senha
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

  void FazerLogout(BuildContext context) async {
    _storage.deleteAll();

    Navigator.pop(context);
  }
}