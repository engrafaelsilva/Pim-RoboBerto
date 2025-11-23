import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:roboberto_ia/Model/chamado.dart';
import 'package:roboberto_ia/ViewModel/ChamadoDetalheViewModel.dart';
import 'package:uuid/uuid.dart';

class ChamadoDetalhePage extends StatefulWidget {
  final Chamado chamado;
  final String nomeUsuarioAtual;

  const ChamadoDetalhePage({
    Key? key,
    required this.chamado,
    required this.nomeUsuarioAtual,
  }) : super(key: key);

  @override
  State<ChamadoDetalhePage> createState() => _ChamadoDetalhePageState();
}

class _ChamadoDetalhePageState extends State<ChamadoDetalhePage> {
  final _chatController = TextEditingController();
  late List<Mensagem> _mensagensAtuais;
  bool _enviandoMensagem = false;

  final _uuid = Uuid();

  bool _chatFoiAtualizado = false;

  @override
  void initState() {
    super.initState();
    _mensagensAtuais = List.from(widget.chamado.mensagens);
  }

  @override
  void dispose() {
    _chatController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final bool estaCarregando =
        context.watch<ChamadoDetalheViewModel>().estaCarregando;

    return WillPopScope(
      onWillPop: () async {
        Navigator.pop(context, _chatFoiAtualizado);
        return false;
      },
      child: Scaffold(
        backgroundColor: const Color(0xFFF0F4FF),
        appBar: AppBar(
          title: Text(widget.chamado.titulo, maxLines: 1),
          backgroundColor: Colors.blueAccent,
        ),
        body: widget.chamado.status == 2
            ? _buildModoSugestao(context, estaCarregando)
            : _buildModoChat(context),
      ),
    );
  }

  Widget _buildModoSugestao(BuildContext context, bool estaCarregando) {
    return Padding(
      padding: const EdgeInsets.all(20.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            "Sugestão da IA",
            style: TextStyle(fontSize: 22, fontWeight: FontWeight.bold),
          ),
          const SizedBox(height: 16),
          Container(
            padding: const EdgeInsets.all(16),
            decoration: BoxDecoration(
              color: Colors.white,
              borderRadius: BorderRadius.circular(12),
              border: Border.all(color: Colors.grey.shade300),
            ),
            child: Text(
              widget.chamado.sugestaoGemini,
              style: const TextStyle(fontSize: 16, height: 1.5),
            ),
          ),
          const SizedBox(height: 30),
          const Text(
            "Isso resolveu o seu problema?",
            style: TextStyle(fontSize: 18, fontWeight: FontWeight.w600),
          ),
          const SizedBox(height: 16),
          if (estaCarregando)
            const Center(child: CircularProgressIndicator())
          else
            Row(
              children: [
                Expanded(
                  child: ElevatedButton(
                    onPressed: () => _handleResponderSugestao(true),
                    child: const Text("Sim, resolveu!"),
                    style: ElevatedButton.styleFrom(
                      primary: Colors.green,
                      padding: const EdgeInsets.symmetric(vertical: 14),
                    ),
                  ),
                ),
                const SizedBox(width: 16),
                Expanded(
                  child: ElevatedButton(
                    onPressed: () => _handleResponderSugestao(false),
                    child: const Text("Não, preciso de ajuda"),
                    style: ElevatedButton.styleFrom(
                      primary: Colors.red,
                      padding: const EdgeInsets.symmetric(vertical: 14),
                    ),
                  ),
                ),
              ],
            ),
        ],
      ),
    );
  }

  Future<void> _handleResponderSugestao(bool resolveu) async {
    final viewModel = context.read<ChamadoDetalheViewModel>();
    viewModel.limparMensagens();

    final bool sucesso =
    await viewModel.responderSugestao(widget.chamado.id, resolveu);

    if (!mounted) return;

    if (sucesso) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Obrigado pelo seu feedback!"),
          backgroundColor: Colors.green,
        ),
      );
      Navigator.pop(context, true);
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(viewModel.mensagemErro),
          backgroundColor: Colors.red,
        ),
      );
    }
  }

  Widget _buildModoChat(BuildContext context) {
    return Column(
      children: [
        Expanded(
          child: ListView.builder(
            padding: const EdgeInsets.all(16.0),
            reverse: true,
            itemCount: _mensagensAtuais.length,
            itemBuilder: (context, index) {
              final mensagem = _mensagensAtuais.reversed.toList()[index];

              print('====================================');
              print('A PROCESSAR MENSAGEM: ${mensagem.texto}');
              print('JSON do Autor (da API): ${mensagem.autor.toJson()}');
              print('Nome do Autor (lido): ${mensagem.autor.nome}');
              print('ID do Autor (lido): ${mensagem.autor.id}');
              print('ID do Autor (do Chamado): ${widget.chamado.autor.id}');

              final bool eMinhaMensagem =
              (mensagem.autor.id == widget.chamado.autor.id);
              String nomeAutor = mensagem.autor.nome ?? "Técnico";

              if (eMinhaMensagem) {
                nomeAutor = widget.nomeUsuarioAtual;
              }

              return _buildBolhaChat(mensagem, eMinhaMensagem, nomeAutor);
            },
          ),
        ),
        _buildChatInput(),
      ],
    );
  }

  Widget _buildBolhaChat(
      Mensagem mensagem, bool eMinhaMensagem, String nomeAutor) {
    return Align(
      alignment: eMinhaMensagem ? Alignment.centerRight : Alignment.centerLeft,
      child: Card(
        elevation: 1.0,
        color: eMinhaMensagem ? const Color(0xFFD9E9FF) : Colors.white,
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
        margin: const EdgeInsets.symmetric(vertical: 4.0),
        child: Padding(
          padding: const EdgeInsets.all(12.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                nomeAutor,
                style: const TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 13,
                    color: Colors.black54),
              ),
              const SizedBox(height: 4),
              Text(
                mensagem.texto,
                style: const TextStyle(fontSize: 16, color: Colors.black87),
              ),
              const SizedBox(height: 6),
              Text(
                DateFormat('dd/MM HH:mm').format(mensagem.dataHoraMensagem),
                style: const TextStyle(fontSize: 12, color: Colors.black45),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildChatInput() {
    return Container(
      padding: const EdgeInsets.all(12.0),
      decoration: BoxDecoration(
        color: Colors.white,
        boxShadow: [
          BoxShadow(
            color: Colors.grey.withOpacity(0.2),
            spreadRadius: 1,
            blurRadius: 5,
          )
        ],
      ),
      child: SafeArea(
        child: Row(
          children: [
            Expanded(
              child: TextFormField(
                controller: _chatController,
                decoration: InputDecoration(
                  hintText: "Digite sua mensagem...",
                  fillColor: Colors.grey[100],
                  filled: true,
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(30.0),
                    borderSide: BorderSide.none,
                  ),
                ),
                onFieldSubmitted: (value) => _handleEnviarMensagem(),
              ),
            ),
            const SizedBox(width: 8),
            _enviandoMensagem
                ? const Padding(
              padding: EdgeInsets.all(8.0),
              child: CircularProgressIndicator(),
            )
                : IconButton(
              icon: const Icon(Icons.send, color: Colors.blueAccent),
              iconSize: 30,
              onPressed: _handleEnviarMensagem,
            ),
          ],
        ),
      ),
    );
  }

  Future<void> _handleEnviarMensagem() async {
    final viewModel = context.read<ChamadoDetalheViewModel>();
    viewModel.limparMensagens();

    final texto = _chatController.text;
    if (texto.isEmpty) return;

    setState(() {
      _enviandoMensagem = true;
    });

    final novaMensagem = Mensagem(
      id: _uuid.v4(),
      texto: texto,
      dataHoraMensagem: DateTime.now(),
      autor: Autor(
        id: widget.chamado.autor.id,
        nome: widget.nomeUsuarioAtual,
      ),
    );

    setState(() {
      _mensagensAtuais.add(novaMensagem);
      _chatController.clear();
    });

    final bool sucesso =
    await viewModel.enviarMensagem(widget.chamado.id, texto);

    setState(() {
      _enviandoMensagem = false;
    });

    if (sucesso && mounted) {
      _chatFoiAtualizado = true;
    } else if (!sucesso && mounted) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text("Falha ao enviar: ${viewModel.mensagemErro}"),
          backgroundColor: Colors.red,
        ),
      );
      setState(() {
        _mensagensAtuais.removeWhere((msg) => msg.id == novaMensagem.id);
      });
    }
  }
}