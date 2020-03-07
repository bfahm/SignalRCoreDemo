import 'package:flutter/material.dart';
import 'package:signalr_client/signalr_client.dart';

const kChatServerUrl = "http://192.168.1.3:5566/Chat";

bool connectionIsOpen = false;
HubConnection _hubConnection;


Future<void> openChatConnection() async {

  if (_hubConnection == null) {
    _hubConnection = HubConnectionBuilder().withUrl(kChatServerUrl).build();
    _hubConnection.onclose((error) => connectionIsOpen = false);
    _hubConnection.on("OnMessage", _handleIncommingChatMessage);
    sendChatMessage();
  }

  if (_hubConnection.state != HubConnectionState.Connected) {
    await _hubConnection.start();
    connectionIsOpen = true;
  }
}

Future<void> sendChatMessage() async {
  await openChatConnection();
  _hubConnection.invoke("Send", args: <Object>["testUser", "testMessage"] );
}

void _handleIncommingChatMessage(List<Object> args){
  print("got some messages");
  connectionIsOpen = true;
}

void main() {
  runApp(new MaterialApp(
    home: new MyApp(),
  ));
}

class MyApp extends StatefulWidget {
  @override
  _State createState() => new _State();
}

class _State extends State<MyApp> {
  @override
  Widget build(BuildContext context) {
    openChatConnection();

    return new Scaffold(
      appBar: new AppBar(
        title: new Text('Name here'),
      ),
      body: new Container(
        padding: new EdgeInsets.all(32.0),
        child: new Column(
          children: <Widget>[new Text('Add Widgets Here')],
        ),
      ),

    );
  }
}
