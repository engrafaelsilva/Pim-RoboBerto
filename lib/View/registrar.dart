import 'package:flutter/material.dart';

class registrar extends StatefulWidget {
  const registrar({Key? key}) : super(key: key);

  @override
  State<registrar> createState() => _registrarState();
}

class _registrarState extends State<registrar> {
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 24.0),
      child: Card(
        elevation: 5.0,
        shadowColor: Colors.black.withOpacity(0.1),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(20.0),
        ),
        child: Padding(
          // Padding interno do cartão
          padding: const EdgeInsets.all(24.0),
          child: Column(
            // Alinha os textos da esquerda para a esquerda
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Crie sua conta', // Texto alterado
                style: TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                ),
              ),
              SizedBox(height: 8),
              Text(
                'Preencha os campos para se registrar', // Texto alterado
                style: TextStyle(
                  fontSize: 14,
                  color: Colors.grey[600],
                ),
              ),
              SizedBox(height: 24),

              // CAMPO DE EMAIL
              Text(
                'Email',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.bold,
                  color: Colors.black54,
                ),
              ),
              SizedBox(height: 8),
              TextFormField(
                decoration: InputDecoration(
                  hintText: 'seu@email.com',
                  fillColor: Colors.grey[200],
                  filled: true,
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(12.0),
                    borderSide: BorderSide.none, // Sem borda
                  ),
                ),
                keyboardType: TextInputType.emailAddress,
              ),
              SizedBox(height: 16),

              // CAMPO DE SENHA
              Text(
                'Senha',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.bold,
                  color: Colors.black54,
                ),
              ),
              SizedBox(height: 8),
              TextFormField(
                obscureText: true, // Para esconder a senha
                decoration: InputDecoration(
                  hintText: '********',
                  fillColor: Colors.grey[200],
                  filled: true,
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(12.0),
                    borderSide: BorderSide.none,
                  ),
                ),
              ),
              SizedBox(height: 16), // Espaço adicionado

              // NOVO CAMPO: CONFIRMAR SENHA
              Text(
                'Confirmar Senha',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.bold,
                  color: Colors.black54,
                ),
              ),
              SizedBox(height: 8),
              TextFormField(
                obscureText: true, // Para esconder a senha
                decoration: InputDecoration(
                  hintText: '********',
                  fillColor: Colors.grey[200],
                  filled: true,
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(12.0),
                    borderSide: BorderSide.none,
                  ),
                ),
              ),
              SizedBox(height: 24),

              // BOTÃO REGISTRAR
              SizedBox(
                width: double.infinity, // Faz o botão ter largura total
                child: ElevatedButton(
                  onPressed: () {
                    // TODO: Adicionar sua lógica de registro
                  },
                  child: Padding(
                    padding: const EdgeInsets.symmetric(vertical: 16.0),
                    child: Text(
                      'Registrar', // Texto alterado
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.bold,
                        color: Colors.white,
                      ),
                    ),
                  ),
                  style: ElevatedButton.styleFrom(
                    primary: Colors.black,
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(12.0),
                    ),
                  ),
                ),
              ),
              // Dica removida
            ],
          ),
        ),
      ),
    );
  }
}
