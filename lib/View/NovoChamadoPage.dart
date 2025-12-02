import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:roboberto_ia/ViewModel/NovoChamadoViewModel.dart'; // Ajuste o import conforme sua pasta

class NovoChamadoPage extends StatefulWidget {
  const NovoChamadoPage({Key? key}) : super(key: key);

  @override
  State<NovoChamadoPage> createState() => _NovoChamadoPageState();
}

class _NovoChamadoPageState extends State<NovoChamadoPage> {
  final _tituloController = TextEditingController();
  final _descricaoController = TextEditingController();
  final _formKey = GlobalKey<FormState>();

  // A lista _categorias fixa foi removida daqui.

  final List<String> _prioridadesNomes = ["Baixa", "Média", "Alta"];

  // Agora a categoria começa nula, pois esperamos a API carregar
  String? _categoriaSelecionada;
  String? _prioridadeSelecionada = "Baixa";

  @override
  void initState() {
    super.initState();
    // Assim que a tela inicia, pedimos para buscar as categorias
    // O 'listen: false' é importante aqui dentro do initState
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<NovoChamadoViewModel>().buscarCategorias();
    });
  }

  int _getPrioridadeInt(String? nomePrioridade) {
    switch (nomePrioridade) {
      case "Média": return 2;
      case "Alta": return 3;
      case "Baixa": default: return 1;
    }
  }

  @override
  void dispose() {
    _tituloController.dispose();
    _descricaoController.dispose();
    super.dispose();
  }

  Future<void> _submitForm() async {
    context.read<NovoChamadoViewModel>().limparMensagens();

    if (!(_formKey.currentState?.validate() ?? false)) {
      return;
    }

    // Validação extra: O usuário selecionou uma categoria?
    if (_categoriaSelecionada == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Por favor, selecione uma categoria."), backgroundColor: Colors.orange),
      );
      return;
    }

    final viewModel = context.read<NovoChamadoViewModel>();

    bool sucesso = await viewModel.criarNovoChamado(
      categoriaNome: _categoriaSelecionada!, // Enviamos o nome (String)
      prioridade: _getPrioridadeInt(_prioridadeSelecionada),
      titulo: _tituloController.text,
      descricao: _descricaoController.text,
    );

    if (!mounted) return;

    if (sucesso) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("Chamado criado com sucesso!"), backgroundColor: Colors.green),
      );
      Navigator.pop(context, true);
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text(viewModel.mensagemErro), backgroundColor: Colors.red),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    // Usamos o Consumer para reconstruir a tela quando as categorias chegarem
    return Consumer<NovoChamadoViewModel>(
      builder: (context, viewModel, child) {
        return Scaffold(
          backgroundColor: const Color(0xFFF0F4FF),
          appBar: AppBar(
            title: const Text("Abrir Novo Chamado"),
            backgroundColor: Colors.blueAccent,
          ),
          body: Form(
            key: _formKey,
            child: SingleChildScrollView(
              padding: const EdgeInsets.all(20.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  // --- CAMPO CATEGORIA (DINÂMICO) ---
                  viewModel.carregandoCategorias
                      ? const Center(child: LinearProgressIndicator()) // Mostra loading enquanto busca
                      : DropdownButtonFormField<String>(
                    value: _categoriaSelecionada,
                    // Se a lista estiver vazia, mostramos um aviso no dropdown ou desabilitamos
                    hint: const Text("Selecione a Categoria"),
                    items: viewModel.listaCategorias.map((categoria) {
                      return DropdownMenuItem<String>(
                        value: categoria.nome, // O valor será o nome (ex: "Infraestrutura")
                        child: Text(categoria.nome),
                      );
                    }).toList(),
                    onChanged: (String? novoValor) {
                      setState(() {
                        _categoriaSelecionada = novoValor;
                      });
                    },
                    validator: (value) => value == null ? 'Campo obrigatório' : null,
                    decoration: _inputDecoration("Categoria"),
                  ),

                  const SizedBox(height: 20),

                  // --- CAMPO PRIORIDADE ---
                  DropdownButtonFormField<String>(
                    value: _prioridadeSelecionada,
                    items: _prioridadesNomes.map((String prioridade) {
                      return DropdownMenuItem<String>(
                        value: prioridade,
                        child: Text(prioridade),
                      );
                    }).toList(),
                    onChanged: (String? novoValor) {
                      setState(() {
                        _prioridadeSelecionada = novoValor;
                      });
                    },
                    decoration: _inputDecoration("Prioridade"),
                  ),
                  const SizedBox(height: 20),

                  // --- CAMPO TÍTULO ---
                  TextFormField(
                    controller: _tituloController,
                    decoration: _inputDecoration("Título do Chamado"),
                    validator: (valor) => (valor?.isEmpty ?? true) ? "Por favor, insira um título" : null,
                  ),
                  const SizedBox(height: 20),

                  // --- CAMPO DESCRIÇÃO ---
                  TextFormField(
                    controller: _descricaoController,
                    decoration: _inputDecoration("Descrição Detalhada"),
                    maxLines: 6,
                    validator: (valor) => (valor?.isEmpty ?? true) ? "Por favor, insira uma descrição" : null,
                  ),
                  const SizedBox(height: 30),

                  // --- BOTÃO ENVIAR ---
                  ElevatedButton(
                    style: ElevatedButton.styleFrom(
                      primary: Colors.black, // Nota: 'primary' está deprecated em Flutter recentes, use backgroundColor se der erro
                      padding: const EdgeInsets.symmetric(vertical: 16),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(12),
                      ),
                    ),
                    onPressed: viewModel.estaCarregando ? null : _submitForm,
                    child: viewModel.estaCarregando
                        ? const SizedBox(
                      height: 24,
                      width: 24,
                      child: CircularProgressIndicator(color: Colors.white, strokeWidth: 3),
                    )
                        : const Text(
                      "Enviar Chamado",
                      style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold, color: Colors.white),
                    ),
                  ),
                ],
              ),
            ),
          ),
        );
      },
    );
  }

  InputDecoration _inputDecoration(String label) {
    return InputDecoration(
      labelText: label,
      fillColor: Colors.white,
      filled: true,
      border: OutlineInputBorder(borderRadius: BorderRadius.circular(12.0), borderSide: BorderSide(color: Colors.grey.shade300)),
      enabledBorder: OutlineInputBorder(borderRadius: BorderRadius.circular(12.0), borderSide: BorderSide(color: Colors.grey.shade300)),
      focusedBorder: OutlineInputBorder(borderRadius: BorderRadius.circular(12.0), borderSide: const BorderSide(color: Colors.blueAccent, width: 2.0)),
    );
  }
}