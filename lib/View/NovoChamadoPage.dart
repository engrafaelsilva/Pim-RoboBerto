import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:roboberto_ia/ViewModel/NovoChamadoViewModel.dart';

class NovoChamadoPage extends StatefulWidget {
  const NovoChamadoPage({Key? key}) : super(key: key);

  @override
  State<NovoChamadoPage> createState() => _NovoChamadoPageState();
}

class _NovoChamadoPageState extends State<NovoChamadoPage> {
  // Controladores para os campos de texto
  final _tituloController = TextEditingController();
  final _descricaoController = TextEditingController();

  // Chave para o formulário (usada para validação)
  final _formKey = GlobalKey<FormState>();

  // Listas para os Dropdowns
  final List<String> _categorias = ["Suporte", "Infraestrutura", "Financeiro"];
  final List<String> _prioridadesNomes = ["Baixa", "Média", "Alta"];

  // Variáveis para guardar a seleção
  String? _categoriaSelecionada = "Suporte";
  String? _prioridadeSelecionada = "Baixa";

  // Mapeamento de Prioridade (Nome para Int)
  int _getPrioridadeInt(String? nomePrioridade) {
    switch (nomePrioridade) {
      case "Média":
        return 2;
      case "Alta":
        return 3;
      case "Baixa":
      default:
        return 1;
    }
  }

  @override
  void dispose() {
    _tituloController.dispose();
    _descricaoController.dispose();
    super.dispose();
  }

  // Função de submissão do formulário
  Future<void> _submitForm() async {
    // Limpa mensagens de erro antigas
    context.read<NovoChamadoViewModel>().limparMensagens();

    // Valida o formulário
    if (!(_formKey.currentState?.validate() ?? false)) {
      return; // Se o formulário não for válido, não faz nada
    }

    final viewModel = context.read<NovoChamadoViewModel>();

    // Chama o ViewModel para criar o chamado
    bool sucesso = await viewModel.criarNovoChamado(
      categoria: _categoriaSelecionada!,
      prioridade: _getPrioridadeInt(_prioridadeSelecionada),
      titulo: _tituloController.text,
      descricao: _descricaoController.text,
    );

    if (!mounted) return; // Verifica se o widget ainda está na tela

    if (sucesso) {
      // Se deu certo: Mostra SnackBar de sucesso e volta para a tela anterior
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Chamado criado com sucesso!"),
          backgroundColor: Colors.green,
        ),
      );
      // O 'true' avisa a tela anterior que um chamado foi criado
      Navigator.pop(context, true);
    } else {
      // Se deu errado: Mostra SnackBar de erro
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(viewModel.mensagemErro),
          backgroundColor: Colors.red,
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    // Ouve o estado de 'estaCarregando'
    final bool estaCarregando =
        context.watch<NovoChamadoViewModel>().estaCarregando;

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
              // --- CAMPO CATEGORIA ---
              DropdownButtonFormField<String>(
                value: _categoriaSelecionada,
                items: _categorias.map((String categoria) {
                  return DropdownMenuItem<String>(
                    value: categoria,
                    child: Text(categoria),
                  );
                }).toList(),
                onChanged: (String? novoValor) {
                  setState(() {
                    _categoriaSelecionada = novoValor;
                  });
                },
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
                validator: (valor) => (valor?.isEmpty ?? true)
                    ? "Por favor, insira um título"
                    : null,
              ),
              const SizedBox(height: 20),

              // --- CAMPO DESCRIÇÃO ---
              TextFormField(
                controller: _descricaoController,
                decoration: _inputDecoration("Descrição Detalhada"),
                maxLines: 6, // Permite múltiplas linhas
                validator: (valor) => (valor?.isEmpty ?? true)
                    ? "Por favor, insira uma descrição"
                    : null,
              ),
              const SizedBox(height: 30),

              // --- BOTÃO ENVIAR ---
              ElevatedButton(
                style: ElevatedButton.styleFrom(
                  primary: Colors.black,
                  padding: const EdgeInsets.symmetric(vertical: 16),
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(12),
                  ),
                ),
                // Desabilita o botão se estiver carregando
                onPressed: estaCarregando ? null : _submitForm,
                child: estaCarregando
                    ? const SizedBox(
                  height: 24,
                  width: 24,
                  child: CircularProgressIndicator(
                    color: Colors.white,
                    strokeWidth: 3,
                  ),
                )
                    : const Text(
                  "Enviar Chamado",
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.bold,
                    color: Colors.white,
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  // Função auxiliar para decorar os campos de texto
  InputDecoration _inputDecoration(String label) {
    return InputDecoration(
      labelText: label,
      fillColor: Colors.white,
      filled: true,
      border: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12.0),
        borderSide: BorderSide(color: Colors.grey.shade300),
      ),
      enabledBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12.0),
        borderSide: BorderSide(color: Colors.grey.shade300),
      ),
      focusedBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(12.0),
        borderSide: const BorderSide(color: Colors.blueAccent, width: 2.0),
      ),
    );
  }
}