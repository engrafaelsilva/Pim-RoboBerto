import 'dart:convert';
import 'package:flutter/services.dart' show rootBundle;

class AppConfig {
  static Map<String, dynamic>? _config;

  static Future<void> loadConfig() async {
    final String configString = await rootBundle.loadString('assets/cfg/config.json');
    _config = json.decode(configString);
  }

  static String? get serverIp {
    return _config?['api_server_ip'];
  }
}