import 'dart:convert'; // Para JSON
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http; // Para requisições HTTP

class LoginViewModel extends ChangeNotifier {
  bool _estaCarregando = false;

  bool get estaCarregando => _estaCarregando;

  String _mensagemErro = "";

  String get mensagemErro => _mensagemErro;

  Future<bool> fazerLogin(String email, String senha) async {
    _estaCarregando = true;
    _mensagemErro = "";
    notifyListeners();

    try {
      final url = Uri.parse('http://10.0.2.2:5129/Autenticacao/login');
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
        String token = data['token'];

        _estaCarregando = false;
        notifyListeners();
        return true;
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
}