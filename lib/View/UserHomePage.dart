import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:roboberto_ia/View/NovoChamadoPage.dart';
import 'package:roboberto_ia/View/chamado_detalhe_page.dart';
import 'package:roboberto_ia/View/home.dart';
import 'package:roboberto_ia/ViewModel/UserHomePageViewModel.dart';
import 'package:roboberto_ia/ViewModel/AuthViewModel.dart';

class UserHomePage extends StatefulWidget {
  final String userName;
  final String userEmail;

  const UserHomePage({
    Key? key,
    required this.userName,
    required this.userEmail,
  }) : super(key: key);

  @override
  State<UserHomePage> createState() => _UserHomePageState();
}

class _UserHomePageState extends State<UserHomePage> {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<UserHomePageViewModel>().buscarMeusChamados();
    });
  }

  Future<void> _handleRefresh() async {
    context.read<UserHomePageViewModel>().limparMensagens();
    await context.read<UserHomePageViewModel>().buscarMeusChamados();
  }

  @override
  Widget build(BuildContext context) {
    final viewModelAuth = context.read<AuthViewModel>();
    final viewModelUserHomePage =
    context.watch<UserHomePageViewModel>();

    return Scaffold(
      backgroundColor: Color(0xFFF0F4FF),
      appBar: _buildCustomAppBar(context, viewModelAuth),
      body:
      RefreshIndicator(
        onRefresh: _handleRefresh,
        child: SingleChildScrollView(
          physics: const AlwaysScrollableScrollPhysics(),
          child: Padding(
            padding: const EdgeInsets.all(16.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                _buildWelcomeCard(context),
                SizedBox(height: 24),
                Text(
                  'Meus Chamados',
                  style: TextStyle(
                    fontSize: 20,
                    fontWeight: FontWeight.bold,
                    color: Colors.black87,
                  ),
                ),
                SizedBox(height: 16),
                _buildChamadosContent(viewModelUserHomePage),
              ],
            ),
          ),
        ),
      ),
    );
  }

  PreferredSizeWidget _buildCustomAppBar(
      BuildContext context, AuthViewModel viewModelAuth) {
    return PreferredSize(
      preferredSize: Size.fromHeight(100.0),
      child: AppBar(
        backgroundColor: Colors.white,
        elevation: 0,
        automaticallyImplyLeading: false,
        flexibleSpace: SafeArea(
          child: Padding(
            padding:
            const EdgeInsets.symmetric(horizontal: 16.0, vertical: 8.0),
            child: Row(
              children: [
                CircleAvatar(
                  radius: 28,
                  backgroundColor: Colors.blueAccent.withOpacity(0.1),
                  child: Text(
                    widget.userName.isNotEmpty
                        ? widget.userName[0].toUpperCase()
                        : 'U',
                    style: TextStyle(
                      fontSize: 24,
                      fontWeight: FontWeight.bold,
                      color: Colors.blueAccent,
                    ),
                  ),
                ),
                SizedBox(width: 12),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Text(
                      widget.userName,
                      style: TextStyle(
                        fontSize: 18,
                        fontWeight: FontWeight.bold,
                        color: Colors.black87,
                      ),
                    ),
                    SizedBox(height: 4),
                    Text(
                      widget.userEmail,
                      style: TextStyle(
                        fontSize: 14,
                        color: Colors.grey[600],
                      ),
                    ),
                  ],
                ),
                Spacer(),
                IconButton(
                  icon: Icon(Icons.logout, color: Colors.grey[600], size: 28),
                  onPressed: () async {
                    await viewModelAuth.FazerLogout();

                    if (mounted) {
                      Navigator.of(context).pushAndRemoveUntil(
                        MaterialPageRoute(
                          builder: (context) => const homePage(),
                        ),
                            (Route<dynamic> route) => false,
                      );
                    }
                  },
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildChamadosContent(UserHomePageViewModel viewModel) {
    if (viewModel.estaCarregando) {
      return const Center(
        child: Padding(
          padding: EdgeInsets.symmetric(vertical: 40.0),
          child: CircularProgressIndicator(),
        ),
      );
    }

    if (viewModel.mensagemErro.isNotEmpty) {
      WidgetsBinding.instance.addPostFrameCallback((_) {
        ScaffoldMessenger.of(context).removeCurrentSnackBar();
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text(viewModel.mensagemErro),
            backgroundColor: Colors.red,
          ),
        );
      });
      return _buildNoTicketsCard(context);
    }

    if (viewModel.chamados.isEmpty) {
      return _buildNoTicketsCard(context);
    }

    return ListView.builder(
      itemCount: viewModel.chamados.length,
      shrinkWrap: true,
      physics: const NeverScrollableScrollPhysics(),
      itemBuilder: (context, index) {
        final chamado = viewModel.chamados[index];

        String statusFormatado;
        switch (chamado.status) {
          case 1:
            statusFormatado = 'Aberto';
            break;
          case 2:
            statusFormatado = 'Sugestão gerada';
            break;
          case 3:
            statusFormatado = 'Pendente Tecnico';
            break;
          case 4:
            statusFormatado = 'Em andamento';
            break;
          case 5:
            statusFormatado = 'Fechado';
            break;
          case 6:
            statusFormatado = 'Cancelado';
            break;
          default:
            statusFormatado = 'Desconhecido';
        }

        final String dataFormatada =
        DateFormat('dd/MM/yyyy HH:mm').format(chamado.dataAbertura);

        return ChamadoCard(
          titulo: chamado.titulo,
          data: dataFormatada,
          status: statusFormatado,
          onTap: () async {
            final bool? precisaAtualizar = await Navigator.push(
              context,
              MaterialPageRoute(
                builder: (context) => ChamadoDetalhePage(
                    chamado: chamado, nomeUsuarioAtual: widget.userName),
              ),
            );

            if (precisaAtualizar == true && mounted) {
              _handleRefresh();
            }
          },
        );
      },
    );
  }

  Widget _buildWelcomeCard(BuildContext context) {
    return Card(
      elevation: 3.0,
      shadowColor: Colors.black.withOpacity(0.1),
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
      child: Container(
        width: double.infinity,
        padding: const EdgeInsets.all(20.0),
        decoration: BoxDecoration(
          color: Colors.blueAccent,
          borderRadius: BorderRadius.circular(16),
        ),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              'Bem-vindo(a) de volta,',
              style: TextStyle(
                fontSize: 18,
                color: Colors.white.withOpacity(0.9),
              ),
            ),
            SizedBox(height: 4),
            Text(
              widget.userName,
              style: TextStyle(
                fontSize: 26,
                fontWeight: FontWeight.bold,
                color: Colors.white,
              ),
            ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: () async {
                final bool? chamadoFoiCriado = await Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const NovoChamadoPage(),
                  ),
                );

                if (chamadoFoiCriado == true && mounted) {
                  _handleRefresh();
                }
              },
              child: Text(
                'Novo Chamado',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.bold,
                  color: Colors.blueAccent,
                ),
              ),
              style: ElevatedButton.styleFrom(
                primary: Colors.white,
                padding: EdgeInsets.symmetric(horizontal: 24, vertical: 14),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildNoTicketsCard(BuildContext context) {
    return Card(
      elevation: 2.0,
      shadowColor: Colors.black.withOpacity(0.05),
      color: Colors.white,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 40),
        child: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Icon(
                Icons.inbox_outlined,
                size: 60,
                color: Colors.grey[400],
              ),
              SizedBox(height: 16),
              Text(
                'Nenhum chamado aberto',
                style: TextStyle(
                  fontSize: 18,
                  fontWeight: FontWeight.bold,
                  color: Colors.grey[600],
                ),
              ),
              SizedBox(height: 8),
              Text(
                'Quando você criar um chamado, ele aparecerá aqui.',
                textAlign: TextAlign.center,
                style: TextStyle(
                  fontSize: 14,
                  color: Colors.grey[500],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

class ChamadoCard extends StatelessWidget {
  final String titulo;
  final String data;
  final String status;
  final VoidCallback onTap;

  const ChamadoCard({
    Key? key,
    required this.titulo,
    required this.data,
    required this.status,
    required this.onTap,
  }) : super(key: key);

  Color _getStatusColor(String status) {
    switch (status.toLowerCase()) {
      case 'aberto':
        return Colors.green;
      case 'sugestão gerada':
        return Colors.cyan;
      case 'pendente tecnico':
      case 'em andamento':
        return Colors.orange;
      case 'fechado':
      case 'cancelado':
        return Colors.grey;
      default:
        return Colors.blue;
    }
  }

  Color _getBackgroundColor(String status) {
    switch (status.toLowerCase()) {
      case 'aberto':
        return Colors.green.withOpacity(0.1);
      case 'sugestão gerada':
        return Colors.cyan.withOpacity(0.1);
      case 'pendente tecnico':
      case 'em andamento':
        return Colors.orange.withOpacity(0.1);
      case 'fechado':
      case 'cancelado':
        return Colors.grey.withOpacity(0.1);
      default:
        return Colors.blue.withOpacity(0.1);
    }
  }

  @override
  Widget build(BuildContext context) {
    final Color statusColor = _getStatusColor(status);
    final Color statusBackgroundColor = _getBackgroundColor(status);

    return Card(
      elevation: 2.0,
      shadowColor: Colors.black.withOpacity(0.05),
      color: Colors.white,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
      margin: const EdgeInsets.only(bottom: 12.0),
      child: InkWell(
        onTap: onTap,
        borderRadius: BorderRadius.circular(16),
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 16.0, vertical: 20.0),
          child: Row(
            children: [
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      titulo,
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.bold,
                        color: Colors.black87,
                      ),
                      maxLines: 1,
                      overflow: TextOverflow.ellipsis,
                    ),
                    SizedBox(height: 6),
                    Text(
                      'Criado em: $data',
                      style: TextStyle(
                        fontSize: 14,
                        color: Colors.grey[600],
                      ),
                    ),
                  ],
                ),
              ),
              SizedBox(width: 12),
              Chip(
                label: Text(
                  status,
                  style: TextStyle(
                    fontSize: 12,
                    fontWeight: FontWeight.bold,
                    color: statusColor,
                  ),
                ),
                backgroundColor: statusBackgroundColor,
                padding: EdgeInsets.symmetric(horizontal: 8.0, vertical: 4.0),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(20),
                  side: BorderSide(color: Colors.transparent),
                ),
              ),
              SizedBox(width: 8),
              Icon(
                Icons.chevron_right,
                color: Colors.grey[400],
                size: 28,
              ),
            ],
          ),
        ),
      ),
    );
  }
}