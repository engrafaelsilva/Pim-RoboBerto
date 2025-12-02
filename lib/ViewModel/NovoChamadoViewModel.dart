import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

class CategoriaModel {
  final String id;
  final String nome;

  CategoriaModel({required this.id, required this.nome});

  // Converte o JSON da API para o objeto Dart
  factory CategoriaModel.fromJson(Map<String, dynamic> json) {
    return CategoriaModel(
      id: json['id'] ?? '',
      nome: json['nome'] ?? '',
    );
  }
}

class NovoChamadoViewModel extends ChangeNotifier {
  final _storage = const FlutterSecureStorage();
  final String ipApi = "robobertoapi-e6cgbaawhxagdwg2.brazilsouth-01.azurewebsites.net";

  // --- Estados de Carregamento ---
  bool _estaCarregando = false;
  bool get estaCarregando => _estaCarregando;

  bool _carregandoCategorias = false; // Novo estado específico para o dropdown
  bool get carregandoCategorias => _carregandoCategorias;

  String _mensagemErro = "";
  String get mensagemErro => _mensagemErro;

  // --- Lista de Categorias ---
  List<CategoriaModel> _listaCategorias = [];
  List<CategoriaModel> get listaCategorias => _listaCategorias;

  void limparMensagens() {
    _mensagemErro = "";
    notifyListeners();
  }

  // --- BUSCAR CATEGORIAS (GET) ---
  Future<void> buscarCategorias() async {
    _carregandoCategorias = true;
    notifyListeners();

    final token = await _storage.read(key: 'jwt_token');

    // Tratamento básico se não tiver token, mas idealmente você redireciona para login
    Map<String, String> headers = {
      'Content-Type': 'application/json',
    };
    if (token != null) {
      headers['Authorization'] = 'Bearer $token';
    }

    try {
      final url = Uri.parse('https://$ipApi/Categoria?tamanho=999');
      final response = await http.get(url, headers: headers).timeout(const Duration(seconds: 10));

      if (response.statusCode == 200) {
        final List<dynamic> dadosJson = jsonDecode(response.body);

        // Converte a lista de JSON para lista de Objetos CategoriaModel
        _listaCategorias = dadosJson.map((json) => CategoriaModel.fromJson(json)).toList();
      } else {
        _mensagemErro = "Erro ao buscar categorias: ${response.statusCode}";
      }
    } catch (e) {
      print("Erro ao buscar categorias: $e");
      // Não vamos travar a tela com erro aqui, apenas a lista ficará vazia
    }

    _carregandoCategorias = false;
    notifyListeners();
  }

  // --- CRIAR CHAMADO (POST) ---
  Future<bool> criarNovoChamado({
    required String categoriaNome, // Vamos passar o Nome para manter compatibilidade com sua API de post
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
      "categoria": {"nome": categoriaNome}, // Envia o nome selecionado
      "prioridade": prioridade,
      "titulo": titulo,
      "descricao": descricao
    };

    try {
      final url = Uri.parse('https://$ipApi/Chamados');
      final response = await http
          .post(url, headers: headers, body: jsonEncode(body))
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