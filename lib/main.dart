import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:roboberto_ia/ViewModel/UserHomePageViewModel.dart';
import 'package:roboberto_ia/ViewModel/NovoChamadoViewModel.dart';
import 'package:roboberto_ia/ViewModel/ChamadoDetalheViewModel.dart';
import 'ViewModel/AuthViewModel.dart';
import 'View/home.dart';

void main() {
  runApp(MultiProvider(
    providers: [
      ChangeNotifierProvider(create: (context) => AuthViewModel()),
      ChangeNotifierProvider(create: (context) => UserHomePageViewModel()),
      ChangeNotifierProvider(create: (context) => NovoChamadoViewModel()),
      ChangeNotifierProvider(create: (context) => ChamadoDetalheViewModel()),
    ],
    child: MyApp(),
  ));
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: homePage(),
    );
  }
}
