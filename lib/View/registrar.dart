import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:roboberto_ia/ViewModel/AuthViewModel.dart';

class RegistrarForm extends StatefulWidget {
  const RegistrarForm({Key? key}) : super(key: key);

  @override
  State<RegistrarForm> createState() => _RegistrarFormState();
}

class _RegistrarFormState extends State<RegistrarForm> {
  final _usuarioController = TextEditingController();
  final _emailController = TextEditingController();
  final _telefoneController = TextEditingController();
  final _senhaController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    final viewModel = context.watch<AuthViewModel>();

    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 24.0),
      child: Card(
        elevation: 5.0,
        shadowColor: Colors.black.withOpacity(0.1),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(20.0),
        ),
        child: Padding(
          padding: const EdgeInsets.all(24.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Crie sua conta',
                style: TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                ),
              ),
              SizedBox(height: 8),
              Text(
                'Preencha os campos para se registrar',
                style: TextStyle(
                  fontSize: 14,
                  color: Colors.grey[600],
                ),
              ),
              SizedBox(height: 24),
              Text(
                'Usuário',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.bold,
                  color: Colors.black54,
                ),
              ),
              SizedBox(height: 8),
              TextFormField(
                controller: _usuarioController,
                decoration: InputDecoration(
                  hintText: 'Nome completo',
                  fillColor: Colors.grey[200],
                  filled: true,
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(12.0),
                    borderSide: BorderSide.none,
                  ),
                ),
                keyboardType: TextInputType.text,
              ),
              SizedBox(height: 16),
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
                controller: _emailController,
                decoration: InputDecoration(
                  hintText: 'seu@email.com',
                  fillColor: Colors.grey[200],
                  filled: true,
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(12.0),
                    borderSide: BorderSide.none,
                  ),
                ),
                keyboardType: TextInputType.emailAddress,
              ),
              SizedBox(height: 16),
              Text(
                'Telefone',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.bold,
                  color: Colors.black54,
                ),
              ),
              SizedBox(height: 8),
              TextFormField(
                controller: _telefoneController,
                decoration: InputDecoration(
                  hintText: '(11) 99999-9999',
                  fillColor: Colors.grey[200],
                  filled: true,
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(12.0),
                    borderSide: BorderSide.none,
                  ),
                ),
                keyboardType: TextInputType.phone,
              ),
              SizedBox(height: 16),
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
                controller: _senhaController,
                obscureText: true,
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
              SizedBox(
                width: double.infinity,
                child: ElevatedButton(
                      onPressed: viewModel.estaCarregando
                      ? null
                          : () async {
                      bool sucesso = await viewModel.fazerRegistro(
                          _usuarioController.text,
                          _emailController.text,
                          _telefoneController.text,
                          _senhaController.text
                      );


                      if(!sucesso) {
                        if (viewModel.mensagemErro != null) {
                          final snackBar = SnackBar(
                            content: Text(viewModel.mensagemErro),
                            duration: Duration(seconds: 3),

                          );

                          ScaffoldMessenger.of(context).showSnackBar(snackBar);
                        }
                      } else {
                          _usuarioController.text = '';
                          _emailController.text = '';
                          _telefoneController.text = '';
                          _senhaController.text = '';

                          final snackBar = SnackBar(
                            content: Text("Conta criada com sucesso!"),
                            duration: Duration(seconds: 3),

                          );

                          ScaffoldMessenger.of(context).showSnackBar(snackBar);
                        }
                    },
                  child: Padding(
                    padding: const EdgeInsets.symmetric(vertical: 16.0),
                    child: viewModel.estaCarregando ? CircularProgressIndicator(
                      color: Colors.white,
                    ): Text(
                      'Registrar',
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
            ],
          ),
        ),
      ),
    );
  }
}
