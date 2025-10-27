import 'dart:convert';

class UsuarioModel {
  /// Mapeia para: usu_codigo (INT, Chave Primária)
  final int? codigo;

  /// Mapeia para: usu_nome (VARCHAR)
  final String nome;

  /// Mapeia para: usu_email (VARCHAR, Único)
  final String email;

  /// Mapeia para: usu_senha_hash (VARCHAR)
  final String? senhaHash;

  /// Mapeia para: usu_telefone (VARCHAR, Nulável)
  final String? telefone;

  /// Mapeia para: usu_datacriacao (DATETIME/TIMESTAMP)
  final DateTime? dataCriacao;

  /// Mapeia para: dep_codigo (INT, Chave Estrangeira)
  final int? depCodigo;

  UsuarioModel({
    this.codigo,
    required this.nome,
    required this.email,
    this.senhaHash,
    this.telefone,
    this.dataCriacao,
    this.depCodigo,
  });

  factory UsuarioModel.fromJson(Map<String, dynamic> json) {
    return UsuarioModel(
      codigo: json['usu_codigo'] as int?,
      nome: json['usu_nome'] as String,
      email: json['usu_email'] as String,
      senhaHash: json['usu_senha_hash'] as String?,
      telefone: json['usu_telefone'] as String?,
      // Trata a data, que pode vir como String (ISO 8601) ou Timestamp
      dataCriacao: json['usu_datacriacao'] == null
          ? null
          : DateTime.parse(json['usu_datacriacao'] as String),
      depCodigo: json['dep_codigo'] as int?,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'usu_codigo': codigo,
      'usu_nome': nome,
      'usu_email': email,
      'usu_senha_hash': senhaHash,
      'usu_telefone': telefone,
      'usu_datacriacao': dataCriacao?.toIso8601String(),
      'dep_codigo': depCodigo,
    };
  }

  UsuarioModel copyWith({
    int? codigo,
    String? nome,
    String? email,
    String? senhaHash,
    String? telefone,
    DateTime? dataCriacao,
    int? depCodigo,
  }) {
    return UsuarioModel(
      codigo: codigo ?? this.codigo,
      nome: nome ?? this.nome,
      email: email ?? this.email,
      senhaHash: senhaHash ?? this.senhaHash,
      telefone: telefone ?? this.telefone,
      dataCriacao: dataCriacao ?? this.dataCriacao,
      depCodigo: depCodigo ?? this.depCodigo,
    );
  }

  @override
  String toString() {
    return 'UsuarioModel(codigo: $codigo, nome: $nome, email: $email)';
  }

  @override
  bool operator ==(Object other) {
    if (identical(this, other)) return true;
    return other is UsuarioModel &&
        other.codigo == codigo &&
        other.nome == nome &&
        other.email == email;
  }

  @override
  int get hashCode => Object.hash(codigo, nome, email);
}