import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:jwt_decoder/jwt_decoder.dart';

const storage =  FlutterSecureStorage();

void saveToken(String token) async {
  await storage.write(key: 'jwt', value: token);
}

Future<String?> getToken() async {
  return await storage.read(key: 'jwt');
}

void storeSesionData(String email, String password) async {
  await storage.write(key: 'email', value: email);
  await storage.write(key: 'password', value: password);
  await storage.write(key: 'loginDate', value: DateTime.now().toString());
}

Future<SesionData> getSesionData() async {
  SesionData sesionData = SesionData();
  sesionData.email = await storage.read(key: 'email');
  sesionData.password = await storage.read(key: 'password');
  var logindate =await storage.read(key: 'loginDate') ?? '';
  sesionData.loginDate = DateTime.tryParse(logindate);
  return sesionData;
}

void deleteSesionData() async {
  await storage.delete(key: 'email');
  await storage.delete(key: 'password');
  await storage.delete(key: 'loginDate');
  await storage.delete(key: 'jwt');
}

void deleteToken() async {
  await storage.delete(key: 'jwt');
}
Future<Map<String, dynamic>> getClaims() async {
  var token = await storage.read(key: "jwt");
  if (token == null) {
    return {};
  }
  return JwtDecoder.decode(await storage.read(key: 'jwt') ?? '');
}

Future<bool> isExpired() async{
  var token = await storage.read(key: "jwt");
  if (token == null) {
    return true;
  }
  return JwtDecoder.isExpired(token);
}

class SesionData {
  String? email;
  String? password;
  DateTime? loginDate;
  SesionData();
}

