import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;
import 'package:uuid/uuid.dart';

class AuthViewModel extends ChangeNotifier {
  final _storage = const FlutterSecureStorage();

  bool _estaCarregando = false;

  bool get estaCarregando => _estaCarregando;

  String _mensagemErro = "";

  String get mensagemErro => _mensagemErro;

  var ipApi = 'robobertoapi-e6cgbaawhxagdwg2.brazilsouth-01.azurewebsites.net';

  Future<Map<String, String>?> fazerLogin(String email, String senha) async {
    _estaCarregando = true;
    _mensagemErro = "";
    notifyListeners();

    if (email.isEmpty || senha.isEmpty) {
      _mensagemErro = 'Insira o email e a senha';
      _estaCarregando = false;
      notifyListeners();
      return null;
    }

    try {
      final url = Uri.parse('https://' + ipApi + '/Autenticacao/login');
      final body = jsonEncode({'email': email, 'senha': senha});

      final response = await http.post(
        url,
        headers: {'Content-Type': 'application/json'},
        body: body,
      ).timeout(const Duration(seconds: 10));

      if (response.statusCode == 200) {
        final data = jsonDecode(response.body);
        final String? token = data['token'];

        if (token != null && token.isNotEmpty) {
          await _storage.write(key: 'jwt_token', value: token);

          final Map<String, String>? dadosUsuario =
          await _buscarDadosUsuario(token);

          if (dadosUsuario != null) {
            _estaCarregando = false;
            notifyListeners();
            return dadosUsuario;
          } else {
            _mensagemErro =
            'Login com sucesso, mas falha ao buscar dados do utilizador.';
          }
        } else {
          _mensagemErro = 'Resposta de login inválida. Token não encontrado.';
        }
      } else {
        try {
          final errorData = jsonDecode(response.body);
          _mensagemErro = errorData['message'] ?? 'Email ou senha invalidos';
        } catch (e) {
          _mensagemErro = 'Email ou senha invalidos';
        }
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
    return null;
  }

  Future<Map<String, String>?> _buscarDadosUsuario(String token) async {
    try {
      final url = Uri.parse('https://$ipApi/Usuario/eu');
      final headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      };

      final response = await http
          .get(url, headers: headers)
          .timeout(const Duration(seconds: 10));

      if (response.statusCode == 200) {
        final data = jsonDecode(response.body);
        final String? nome = data['nome'];
        final String? email = data['email'];

        if (nome != null && email != null) {
          return {'nome': nome, 'email': email};
        }
      }
      return null;
    } catch (e) {
      return null;
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

      final url = Uri.parse('https://' + ipApi + '/Usuario');
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
          print("Log de Erro: ${response.body}");

          if (errorData['error'] != null) {
            _mensagemErro = errorData['error'];
          }
          else if (errorData['errors'] != null) {
            Map<String, dynamic> errorsMap = errorData['errors'];

            if (errorsMap.isNotEmpty) {
              var listaDeErros = errorsMap.values.first;

              if (listaDeErros is List && listaDeErros.isNotEmpty) {
                _mensagemErro = listaDeErros[0];
              } else {
                _mensagemErro = 'Verifique os dados informados.';
              }
            } else {
              _mensagemErro = errorData['title'] ?? 'Erro de validação nos dados.';
            }
          }
          else {
            _mensagemErro = errorData['title'] ?? 'Erro ao criar a conta';
          }
        } catch (e) {
          _mensagemErro = 'Erro ao processar resposta do servidor.';
        }

        _estaCarregando = false;
        notifyListeners();
        return false;
      }
    } on TimeoutException catch (_) {
      _mensagemErro = 'O servidor demorou muito para responder. Tente novamente.';
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

  Future<void> FazerLogout() async {
    await _storage.deleteAll();
  }
}