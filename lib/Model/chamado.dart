import 'dart:convert';
import 'dart:math';

List<Chamado> chamadoFromJson(String str) =>
    List<Chamado>.from(json.decode(str).map((x) => Chamado.fromJson(x)));

String chamadoToJson(List<Chamado> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

DateTime? _parseDate(dynamic dateValue) {
  if (dateValue == null || dateValue.toString().isEmpty) {
    return null;
  }
  try {
    return DateTime.parse(dateValue.toString());
  } catch (e) {
    return null;
  }
}

DateTime _parseDateRequired(dynamic dateValue) {
  if (dateValue == null || dateValue.toString().isEmpty) {
    return DateTime(1970, 1, 1);
  }
  try {
    return DateTime.parse(dateValue.toString());
  } catch (e) {
    return DateTime(1970, 1, 1);
  }
}

class Chamado {
  final String id;
  final Autor autor;
  final Autor? tecnico;
  final Categoria categoria;
  final int status;
  final int prioridade;
  final String titulo;
  final String sugestaoGemini;
  final String descricao;
  final DateTime dataAbertura;
  final DateTime? dataFechamento;
  final DateTime slaVenceEm;
  final bool? sugestaoResolveu;
  final List<Mensagem> mensagens;

  Chamado({
    required this.id,
    required this.autor,
    this.tecnico,
    required this.categoria,
    required this.status,
    required this.prioridade,
    required this.titulo,
    required this.sugestaoGemini,
    required this.descricao,
    required this.dataAbertura,
    this.dataFechamento,
    required this.slaVenceEm,
    this.sugestaoResolveu,
    required this.mensagens,
  });

  factory Chamado.fromJson(Map<String, dynamic> json) {
    return Chamado(
      id: json["id"]?.toString() ?? '',
      autor: Autor.fromJson(json["autor"] ?? {}),
      tecnico: json["tecnico"] != null ? Autor.fromJson(json["tecnico"]) : null,
      categoria: Categoria.fromJson(json["categoria"] ?? {}),
      status: (json["status"] as num?)?.toInt() ?? 0,
      prioridade: (json["prioridade"] as num?)?.toInt() ?? 0,
      titulo: json["titulo"]?.toString() ?? 'Título Indisponível',
      sugestaoGemini: json["sugestaoGemini"]?.toString() ?? '',
      descricao: json["descricao"]?.toString() ?? '',
      dataAbertura: _parseDateRequired(json["dataAbertura"]),
      dataFechamento: _parseDate(json["dataFechamento"]),
      slaVenceEm: _parseDateRequired(json["slaVenceEm"]),
      sugestaoResolveu: json["sugestaoResolveu"] as bool?,
      mensagens: json["mensagens"] != null
          ? List<Mensagem>.from(
          json["mensagens"].map((x) => Mensagem.fromJson(x)))
          : [],
    );
  }

  Map<String, dynamic> toJson() => {
    "id": id,
    "autor": autor.toJson(),
    "tecnico": tecnico?.toJson(),
    "categoria": categoria.toJson(),
    "status": status,
    "prioridade": prioridade,
    "titulo": titulo,
    "sugestaoGemini": sugestaoGemini,
    "descricao": descricao,
    "dataAbertura": dataAbertura.toIso8601String(),
    "dataFechamento": dataFechamento?.toIso8601String(),
    "slaVenceEm": slaVenceEm.toIso8601String(),
    "sugestaoResolveu": sugestaoResolveu,
    "mensagens": List<dynamic>.from(mensagens.map((x) => x.toJson())),
  };
}

class Autor {
  final String id;
  final String? nome;

  Autor({required this.id, this.nome});

  Autor copyWith({
    String? id,
    String? nome,
  }) {
    return Autor(
      id: id ?? this.id,
      nome: nome ?? this.nome,
    );
  }

  factory Autor.fromJson(Map<String, dynamic> json) {
    return Autor(
      id: json["id"]?.toString() ?? '',
      nome: json["nome"]?.toString(),
    );
  }

  Map<String, dynamic> toJson() => {
    "id": id,
    "nome": nome,
  };
}

class Categoria {
  final String? nome;

  Categoria({this.nome});

  factory Categoria.fromJson(Map<String, dynamic> json) {
    return Categoria(
      nome: json["nome"]?.toString(),
    );
  }

  Map<String, dynamic> toJson() => {
    "nome": nome,
  };
}

class Mensagem {
  final String id;
  final Autor autor;
  final DateTime dataHoraMensagem;
  final String texto;

  Mensagem({
    required this.id,
    required this.autor,
    required this.dataHoraMensagem,
    required this.texto,
  });

  Mensagem copyWith({
    String? id,
    Autor? autor,
    DateTime? dataHoraMensagem,
    String? texto,
  }) {
    return Mensagem(
      id: id ?? this.id,
      autor: autor ?? this.autor,
      dataHoraMensagem: dataHoraMensagem ?? this.dataHoraMensagem,
      texto: texto ?? this.texto,
    );
  }

  factory Mensagem.fromJson(Map<String, dynamic> json) {
    return Mensagem(
      id: json["id"]?.toString() ?? '',
      autor: Autor.fromJson(json["autor"] ?? {}),
      dataHoraMensagem: _parseDateRequired(json["dataHoraMensagem"]),
      texto: json["texto"]?.toString() ?? '',
    );
  }

  Map<String, dynamic> toJson() => {
    "id": id,
    "autor": autor.toJson(),
    "dataHoraMensagem": dataHoraMensagem.toIso8601String(),
    "texto": texto,
  };
}