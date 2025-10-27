import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'ViewModel/LoginViewModel.dart';
import 'View/home.dart';

void main() {
  runApp(ChangeNotifierProvider(
    create: (context) => LoginViewModel(),
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
