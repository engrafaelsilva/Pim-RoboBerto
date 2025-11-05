import 'package:flutter/material.dart';

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
      case 'em andamento':
        return Colors.orange;
      case 'fechado':
        return Colors.grey;
      default:
        return Colors.blue;
    }
  }

  Color _getBackgroundColor(String status) {
    switch (status.toLowerCase()) {
      case 'aberto':
        return Colors.green.withOpacity(0.1);
      case 'em andamento':
        return Colors.orange.withOpacity(0.1);
      case 'fechado':
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

              // Chip de Status
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

              // Ícone de Seta
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
